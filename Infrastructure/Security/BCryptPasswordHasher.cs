using App_QLPK.Application.Interfaces.Services;

namespace App_QLPK.Infrastructure.Security;

// public class BCryptPasswordHasher : IPasswordHasher
// {
//     public string Hash(string password)
//     {
//         return BCrypt.Net.BCrypt.HashPassword(password);
//     }

//     public bool Verify(string password, string hash)
//     {
//         // Bảo vệ trước các hash cũ không hợp lệ(dữ liệu seed thô)
//         try { return BCrypt.Net.BCrypt.Verify(password, hash); }
//         catch { return false; }
//     }


// }


public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}