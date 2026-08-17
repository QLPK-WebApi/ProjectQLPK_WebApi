
using BlazorServer.Models;

namespace BlazorServer.Services;

public class AppointmentApiClient
{
    private readonly HttpClient _http;

    public AppointmentApiClient(HttpClient http)
        => _http = http;


    public async Task<PagedResult<AppointmentModel>> GetPagedAsync(int PageIndex, int PageSize, string? keyword, CancellationToken ct = default)
    {
        var url = $"api/appointments?pageIndex={PageIndex}&pageSize={PageSize}";

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            url += $"&keyword={Uri.EscapeDataString(keyword)}";
        }

        var res = await _http.GetAsync(url, ct); // sending to backend
        if (!res.IsSuccessStatusCode)
        {
            throw new ApiException(await AuthApiClient.ReadMessageAsync(res, ct));
        }

        var body = await res.Content.ReadFromJsonAsync<ApiResponse<PagedResult<AppointmentModel>>>(cancellationToken: ct);
            return body!.Data ?? new PagedResult<AppointmentModel>();
    }


    public async Task<AppointmentModel> CreateAsync (CreateAppointmentModel model, CancellationToken ct = default)
    {
        var res = await _http.PostAsJsonAsync("api/appointments", model, ct);
        if (!res.IsSuccessStatusCode)
        {
            throw new ApiException (await AuthApiClient.ReadMessageAsync(res, ct));
        }

        var body = await res.Content.ReadFromJsonAsync<ApiResponse<AppointmentModel>>(cancellationToken: ct);
        return body!.Data!;
    }



    public async Task ChangeStatusAsync(int id, string status, string? cancelReason, CancellationToken ct = default)
    {
        var res = await _http.PutAsJsonAsync($"api/appointments/{id}/status", new { Status = status, CancelReason = cancelReason }, ct);

        if (!res.IsSuccessStatusCode)
        {
            throw new ApiException(await AuthApiClient.ReadMessageAsync(res, ct));
        }
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var res = await _http.DeleteAsync($"api/appointments/{id}", ct);

        if (!res.IsSuccessStatusCode)
        {
            throw new ApiException(await AuthApiClient.ReadMessageAsync(res, ct));
        }
    }

}





    
