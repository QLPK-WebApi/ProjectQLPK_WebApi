using App_QLPK.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using App_QLPK.Domain.Entities;


namespace App_QLPK.Infrastructure.Persistence;

/// <summary>
///     Tạo 1 tài khoản mặc định admin để đảm bảo có sẵn vai trò Admin (mật khẩu BCrypt) để đăng nhập thử, chỉ Insert khi chưa tồn tại . Gọi lúc khởi động ở môi trường Development
/// </summary> 

public static class DbSeeder
{
    public const string AdminUsername = "admin";
    public const string AdminPassword = "Admin@123";

    public static async Task SeedAsync(QlpkDbContext context, IPasswordHasher hasher, CancellationToken ct = default)
    {
        var roles = new List<Role>
        {
            new Role
            {
                Name = "Quản trị viên",
                Code = "Admin",
                Description = "Toàn quyền",
                CreatedAt = DateTime.UtcNow
            },

            new Role
            {
                Name = "Bác sĩ",
                Code = "Doctor",
                Description = "Khám và điều trị bệnh",
                CreatedAt = DateTime.UtcNow
            },

            new Role
            {
                Name ="Lễ tân",
                Code = "Receptionist",
                Description = "Tiếp nhận bệnh nhân, đặt lịch",
                CreatedAt = DateTime.UtcNow
            },

            new Role
            {
                Name = "Bệnh nhân",
                Code = "Patient",
                Description = "Người sử dụng dịch vụ khám",
                CreatedAt = DateTime.UtcNow
            },
        };


        foreach (var role in roles)
        {
            var existed = await context.Roles.AnyAsync(r => r.Code == role.Code, ct);

            if(!existed)
            {
                context.Roles.Add(role);
            }
        }

        await context.SaveChangesAsync(ct);


        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Code == "Admin", ct);

        var hasAdmin = await context.Users.AnyAsync(u => u.Username == AdminUsername, ct);
        if(!hasAdmin)
        {
            context.Add(new User
            {
                Username = AdminUsername,
                FullName = "Quản trị hệ thống",
                RoleId = adminRole!.Id,
                Email = "admin@qlpk.local",
                PasswordHash = hasher.Hash(AdminPassword),
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            });

            await context.SaveChangesAsync(ct);
        }
    }

}

    

