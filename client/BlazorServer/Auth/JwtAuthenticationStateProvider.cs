using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorServer.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorServer.Auth;

/// <summary>
/// Manage authentication state based on Jwt stored in LocalStorage (Quản lý trạng thái đăng nhập dựa trên JWT lưu ở LocalStorage).
/// </summary>
public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string TokenKey = "qlpk_token";
    private static readonly AuthenticationState Anonymous =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    private readonly ProtectedLocalStorage _storage;
    private readonly TokenProvider _tokenProvider;

    public JwtAuthenticationStateProvider(ProtectedLocalStorage storage, TokenProvider tokenProvider)
    {
        _storage = storage;
        _tokenProvider = tokenProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _storage.GetAsync<string>(TokenKey); // get token stored LocalStorage ( lấy token )
            var token = result.Success ? result.Value : null;
            if (string.IsNullOrEmpty(token) || IsExpired(token)) 
                return Anonymous;   // if token null or IsExpired -> state /login ( null hoặc hết hạn thì trả về trạng thái chưa đăng nhập )

            _tokenProvider.Token = token;
            return new AuthenticationState(new ClaimsPrincipal(BuildIdentity(token)));
        }

        catch { return Anonymous; }
    }

    public async Task LoginAsync(string token)
    {
        await _storage.SetAsync(TokenKey, token);
        _tokenProvider.Token = token;
        var user = new ClaimsPrincipal(BuildIdentity(token));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task LogoutAsync()
    {
        await _storage.DeleteAsync(TokenKey);
        _tokenProvider.Token = null;
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }

    private static ClaimsIdentity BuildIdentity(string token) // tạo danh tính từ token
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return new ClaimsIdentity(jwt.Claims, "jwt", JwtRegisteredClaimNames.UniqueName, ClaimTypes.Role);
    }

    private static bool IsExpired(string token) // xử lý token hết hạn
    {
        try
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.ValidTo < DateTime.UtcNow;
        }
        catch { return true; }
    }
}

