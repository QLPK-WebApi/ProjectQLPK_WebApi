namespace BlazorServer.Auth;

/// <summary>
///     Store the JWT in memory per circuit(BlazorServer scoped).
///     use it in DelegatingHandler to attach Authorization header
///     without calling JS interop on every request.
/// </summary> ( Giữ Jwt trong bộ nhớ. Dùng DelegatingHandler đính kèm header mà không cần gọi JS trên mỗi request)
public class TokenProvider
{ 
    public string? Token { get; set; }
}