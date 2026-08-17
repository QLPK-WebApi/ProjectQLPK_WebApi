 using App_QLPK.Application.DTO.Auth;
using App_QLPK.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using WebApi.Models;

namespace App_QLPK.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    /// <summary>
    ///     Đăng nhập, trả về Jwt Token.
    /// </summary>
            
    [HttpPost("login")]
    [AllowAnonymous]  // cho phép user trước khi đăng nhập, được phép gọi Api này --> nếu đăng nhập đúng username/password(đã đăng ký) -> cấp token JWT -> tránh lỗi 401 [Authorize]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var response = await _auth.LoginAsync(request, ct);
        return Ok(new { message = "Đăng nhập thành công", data = response });
    }

}

