


using App_QLPK.Application.Common;
using App_QLPK.Application.DTO.Auth;
using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Application.Interfaces.Services;

namespace App_QLPK.Application.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _hasher;
    private readonly IUserRepository _userRepo;
    private readonly IJwtTokenService _jwt;

    public AuthService(IUserRepository userRepo, IPasswordHasher hasher, IJwtTokenService jwt)
    {
        _userRepo = userRepo;
        _hasher = hasher;
        _jwt = jwt;
    }



public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userRepo.GetByUsernameAsync(request.Username, ct);

        if(user == null || user.IsDeleted || !user.IsActive)
        {
            throw new AppException("Tài khoản không tồn tại hoặc đã bị khóa.");
        }

        if(!_hasher.Verify(request.Password, user.PasswordHash))
        {
            throw new AppException("Sai tên đăng nhập hoặc mật khẩu.");
        }


        var (token, expiresAt) = _jwt.GenerateToken(user);
        
        user.LastLoginAt = DateTime.UtcNow;
        await _userRepo.UpdateAsync(user, ct); // lưu vào DB
        
        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserInfo
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role?.Code ?? "", /* - nếu user.Role?.Code không null thì lấy giá trị Code
                                                - nếu user.Role.Code = null thì lấy giá trị "" (rỗng) */
            } 
        };
    }
}