using BlazorServer.Models;
using BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace BlazorServer.Pages.Appointments;

public partial class Appointment
{
    [Inject] private AppointmentApiClient Api { get; set; } = default!;
    private PagedResult<AppointmentModel>? _result;

    private string? _keyword;
    private string? _error;
    private bool _loading;
    private bool _showCreate;
    protected override Task OnInitializedAsync()
        => LoadAsync(1);   // auto-run on page load (tự chạy khi mở trang , mặc định là trang 1)
    private async Task LoadAsync(int pageIndex)
    {
        _loading = true;
        _error = null;

        try
        {
            _result = await Api.GetPagedAsync(pageIndex, 10, _keyword);
        }
        catch (ApiException ex) { _error = ex.Message; }
        catch { _error = "Không kết nối được tới Api."; }
        finally { _loading = false; }
    }
    private async Task OnSearchKey(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
            await LoadAsync(1);
    }



    private static Color StatusColor(string s) => s switch
    {
        "Scheduled" => Color.Secondary,
        "CheckedIn" => Color.Info,
        "Completed" => Color.Success,
        "Cancelled" => Color.Error,
        _ => Color.Default
    };
    private static string StatusText(string s) => s switch
    {
        "Scheduled" => "Đã đặt",
        "CheckedIn" => "Đã đến",
        "Completed" => "Hoàn tất",
        "Cancelled" => "Đã hủy",
        _ => s
    };

    private const string CheckedIn = "CheckedIn";
    private const string Completed = "Completed";
    private async Task ChangeStatus(AppointmentModel a, string status)
    {
        try
        {
            await Api.ChangeStatusAsync(a.Id, status, null);
            await LoadAsync(_result!.PageIndex); // Load lại UI  
        }
        catch (ApiException ex) { _error = ex.Message; }
    }
    private async Task CancelAsync(AppointmentModel a)
    {
        try
        {
            await Api.ChangeStatusAsync(a.Id, "Cancelled", "Hủy bởi người dùng");
            await LoadAsync(_result!.PageIndex);
        }
        catch (ApiException ex) { _error = ex.Message; }
    }



    private void OpenCreate() => _showCreate = true;
    private async Task OnCreated()
    {
        _showCreate = false;
        await LoadAsync(1);
    }
}

    







