
namespace App_QLPK.WebApi.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_QLPK.Application.DTO.Appointments;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Application.Services;
using App_QLPK.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using WebApi.Models;



[ApiController]
[Route("api/[controller]")]
[Authorize] // mọi Endpoint yêu cầu Jwt hợp lệ
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;
    public AppointmentController(IAppointmentService servece)
        => _service = servece;


    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? keyword = null,
        CancellationToken ct = default)
    {
        var result = await _service.GetPagedAsync(pageIndex, pageSize, keyword, ct);
        return Ok(new { message = "Thành công", data = result });
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        if (dto is null)
        {
            return NotFound(new { message = "Lịch hẹn không tồn tại" });
        }

        return Ok(new { message = "Thành công", data = dto });
    }



    [HttpPost("Create")]
    [Authorize(Roles = "ADMIN, RECEPTIONIST, DOCTOR")]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            new { message = "Tạo lịch hẹn thành công", data = created });
    }


    [HttpPut("Update{id:int}")]
    [Authorize(Roles = "ADMIN, RECEPTIONIST, DOCTOR")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentDTO dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        return Ok(new { message = "Cập nhật thành công", data = updated });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN, RECEPTIONIST")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return Ok(new { message = "Đã hủy lịch hẹn." });
    }
}
