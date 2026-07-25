


namespace App_QLPK.Infrastructure.Options;

/// <summary>
///     Cấu hình JWT, lấy từ section "Jwt" trong appsettings.json (WebApi)
/// </summary> 
public class JwtOption
{
    public const string SectionName = "Jwt";

    
    public string Key { get; set; } = null!; 
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpireMinutes { get; set; } = 120;

}