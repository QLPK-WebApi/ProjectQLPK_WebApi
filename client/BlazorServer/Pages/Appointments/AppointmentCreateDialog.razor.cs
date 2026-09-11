using BlazorServer.Models;
using BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorServer.Pages.Appointments;


public partial class AppointmentCreateDialog
{
    [Inject] private AppointmentApiClient Api { get; set; } = default!;

    private  MudDialog _dialogRef = default!;
    private readonly CreateAppointmentModel _model = new();
    private readonly DialogOptions _dialogOptions = new()
    {
        BackdropClick = false, // không cho đóng dialog khi click ngoài form
        CloseOnEscapeKey = false, // không cho đóng dialog khi nhấn Esc
        MaxWidth = MaxWidth.Small, // Kích thước dialog ở mức nhỏ-vừa
        FullWidth = true // sử dụng chiều rộng không vượt quá Small
    };

    private string? _error;
    private bool _saving;

    [Parameter] public EventCallback OnSaved { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    private async Task SaveAsync()
    {
        _error = null;
        _saving = true;

        try
        {
            await Api.CreateAsync(_model);
            await _dialogRef.CloseAsync();
            await OnSaved.InvokeAsync();
        }
        catch (ApiException ex) { _error = ex.Message; }

        catch { _error = "Không kết nối được tới API."; }
        finally { _saving = false; }
    }

    private async Task CancelAsync()
    {
        await _dialogRef.CloseAsync();
        await OnCancel.InvokeAsync();
    }
}


