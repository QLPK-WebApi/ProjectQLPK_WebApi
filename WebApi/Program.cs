using System.Security.Cryptography;
using System.Text;
using App_QLPK.Application.Interfaces;
using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Application.Services;
using App_QLPK.Infrastructure.Options;
using App_QLPK.Infrastructure.Persistence;
using App_QLPK.Infrastructure.Repositories;
using App_QLPK.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);   

// ------ Đăng ký vào DI cho Config Jwt ------
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.SectionName));

// --------- Config Jwt Authentication ---------
var jwt = builder.Configuration.GetSection(JwtOption.SectionName).Get
<JwtOption>()!;   // dấu " ! " : đảm bảo giá trị này không null 


// =====  Cấu hình hệ thống JWT và Quy định kiểm tra Token có hợp lệ không ===== //
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,              // Token cấp bởi ai?
            ValidateAudience = true,            // Token cấp cho ai?
            ValidateLifetime = true,            // Token còn hạn không?
            ValidateIssuerSigningKey = true,    // có bị giả mạo không?
            ClockSkew = TimeSpan.Zero,          // đảm bảo token hết hạn chính xác

            ValidIssuer = jwt.Issuer,        // so sánh với issuer trong token
            ValidAudience = jwt.Audience,   //  so sánh với audience trong token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))    // dùng Secret Key để Verify chữ ký token
        };

    });
builder.Services.AddAuthorization();

// --- CORS cho frontend Blazor Server ---
const string CorsPolicy = "BlazorClient";
builder.Services.AddCors(options =>
    options.AddPolicy(CorsPolicy, p => p
        .WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
        .AllowAnyHeader()
        .AllowAnyMethod()));

//  ------ Swagger có nút Authorize (Bearer) ------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QLPK API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT (không cần tiền tố 'Bearer')."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
            Array.Empty<string>()
        }
    });
});




// DI Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService,DoctorService>();

builder.Services.AddScoped<ISpecialtyRepository,SpecialtyRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


// DI Jwt
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


builder.Services.AddOpenApi();

//DI đăng ký DbContext
builder.Services.AddDbContext<QlpkDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));

});

// builder.Services.AddDbContext<QlbhContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
// });

// Add Controllers
builder.Services.AddControllers();


var app = builder.Build();

// --- Tự động apply migration mỗi khi app khởi động ---
using (var migrationScope = app.Services.CreateScope())
{
    var context = migrationScope.ServiceProvider.GetRequiredService<QlpkDbContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    // Mở phiên làm việc riêng để Seed tài khoản admin, password(BCrypt)
    using var scope = app.Services.CreateScope();
    var sp = scope.ServiceProvider; // kho chứa service
    try
    {
        var context = sp.GetRequiredService<QlpkDbContext>();
        var hasher = sp.GetRequiredService<IPasswordHasher>();
        await DbSeeder.SeedAsync(context, hasher);

    }
    // bỏ qua nếu lỗi để app vẫn chạy
    catch (Exception ex) { app.Logger.LogWarning(ex, "Bỏ qua seed admin (DB chưa sẵn sàng?)."); }



}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


