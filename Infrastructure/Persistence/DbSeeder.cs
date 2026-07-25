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

    public static async Task SeedAsync(QlpkDbContext context,IPasswordHasher hasher, CancellationToken ct = default)
    {
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Code == "Admin", ct);

        if (adminRole == null) // chưa có -> tạo mới
        {
            adminRole = new Role
            {
                Name = "Quản trị viên",
                Code = "Admin",
                Description = "Toàn quyền",
                CreatedAt = DateTime.UtcNow
            };
            context.Roles.Add(adminRole);
            await context.SaveChangesAsync(ct);
        };


        var hasAdmin = await context.Users.AnyAsync(u => u.Username == AdminUsername, ct);

        if (!hasAdmin)
        {
            context.Users.Add(new User
            {
                Username = AdminUsername,
                FullName = "Quản trị hệ thống",
                RoleId = adminRole.Id,
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