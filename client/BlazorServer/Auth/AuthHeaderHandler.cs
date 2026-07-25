
namespace BlazorServer.Auth;
/// <summary>
///     Attach "Authorization: Bearer " every request calling API.(đính kèm "Authorization: Bearer cho mỗi lần request gọi API)
/// </summary> 


// Intercept all request before sending , avoid flop code when calling API (chặn request trước khi gửi đi, tránh lặp code lại khi gọi API : api/appointments, users, patients...)
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;
    public AuthHeaderHandler(TokenProvider tokenProvider)
        => _tokenProvider = tokenProvider;


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        if(!string.IsNullOrWhiteSpace(_tokenProvider.Token))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenProvider.Token);
        return await base.SendAsync(request,ct);
    }
}