
using BlazorServer.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorServer.Pages;
using BlazorServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- Xác thực phía client (JWT lưu ở ProtectedLocalStorage) ---
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();


// --- HttpClient gọi Web API, tự đính kèm Bearer token ---
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5220/";
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddHttpClient<AuthApiClient>(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthHeaderHandler>();


builder.Services.AddHttpClient<AppointmentApiClient>(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    // Xác thực/uỷ quyền do AuthorizeRouteView xử lý phía client (token JWT ở ProtectedLocalStorage,
    // server không đọc được lúc SSR). Cho endpoint ẩn danh để middleware authorization không gọi ChallengeAsync (gây lỗi thiếu IAuthenticationService).
    .AllowAnonymous();

app.Run();
