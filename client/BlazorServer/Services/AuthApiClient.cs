using BlazorServer.Models;


namespace BlazorServer.Services;

public class AuthApiClient
{
    private readonly HttpClient _http;
    public AuthApiClient(HttpClient http) => _http = http;

    /// <summary>Gọi POST /api/auth/login. Trả về LoginResponse hoặc throw với message từ API.</summary>
    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", request, ct);
        if (!res.IsSuccessStatusCode)
            throw new ApiException(await ReadMessageAsync(res, ct));

        var body = await res.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>(cancellationToken: ct);
        return body!.Data! ;
    }

    internal static async Task<string> ReadMessageAsync(HttpResponseMessage res, CancellationToken ct) // đọc và thông báo lỗi(message) từ HTTP response khi gọi API
    {
        try
        {
            var err = await res.Content.ReadFromJsonAsync<ApiResponse<object>>(cancellationToken: ct);
            if (!string.IsNullOrWhiteSpace(err?.Message)) return err.Message;   // nếu hợp lệ -> trả về message từ server
        }
        catch { /* ignore */ }
        return $"Lỗi {(int)res.StatusCode}";
    }
}

/// <summary>Lỗi trả về từ API, hiển thị message thân thiện lên UI.</summary>
public class ApiException : Exception
{
    public ApiException(string message) : base(message) { }
}