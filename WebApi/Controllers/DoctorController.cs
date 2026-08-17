using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_QLPK.Application.DTO;
using App_QLPK.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
            => _service = service;


        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDoctorDTO dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            return Ok(new {message = "Tạo thành công.", data = created});
        }

        
    }
}