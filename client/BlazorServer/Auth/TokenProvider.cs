namespace BlazorServer.Auth;

/// <summary>
///     Store the JWT in memory per circuit(BlazorServer scoped).
///     use it in DelegatingHadler to attach Authorization header
///     without calling JS interop on every request.
/// </summary> 
public class TokenProvider
{ 
    public string? Token { get; set; }
}