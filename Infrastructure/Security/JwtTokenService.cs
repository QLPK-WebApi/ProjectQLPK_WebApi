
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Domain.Entities;
using App_QLPK.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App_QLPK.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption _option;
    
    public JwtTokenService(IOptions<JwtOption> option)
    {
        _option = option.Value;
    }

    public (string Token, DateTime ExpiresAt) GenerateToken(User user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_option.ExpireMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // gán user.Id vào token với key = "sub" -> biết user nào gọi Api
            new(JwtRegisteredClaimNames.UniqueName, user.Username), // gán username vào token với key = "unique_name" (account login)
            new(ClaimTypes.Name, user.FullName), // gán fullname vào token với key = "name" (tên hiển thị)
            new(ClaimTypes.Role, user.Role?.Code ?? "User"), // gán role của user vào token với key = "role" (chặn Api theo quyền)
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // phân biệt token : mỗi lần login -> tạo 1 token khác nhau ( id của token )
        };


    // ### Tạo chìa khóa bí mật + ký token + tạo Jwt chứa thông tin user và thời hạn ### //
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Key)); // tạo Secret Key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // tạo chữ ký

        var token = new JwtSecurityToken( // tạo Jwt token
            issuer: _option.Issuer,
            audience: _option.Audience,
            claims: claims,
            expires : expiresAt,
            signingCredentials: creds
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
    
}