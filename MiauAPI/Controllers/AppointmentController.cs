using MiauAPI.Common;
using MiauAPI.Models.QueryObjects;
using MiauAPI.Models.QueryParameters;
using MiauAPI.Models.Requests;
using MiauAPI.Models.Responses;
using MiauAPI.Services;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using System.Text.Json;

namespace MiauAPI.Controllers;

[ApiController]
[Route(ApiConstants.MainEndpoint)]
public sealed class AppointmentController : ControllerBase
{
    private readonly AppointmentService _service;

    public AppointmentController(AppointmentService service)
        => _service = service;


    [HttpPost("create")]
    [ProducesResponseType(typeof(CreatedAppointmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OneOf<CreatedAppointmentResponse, ErrorResponse>>> RegisterAsync([FromBody] CreatedAppointmentRequest appointment)
        => await _service.CreateAppointmentAsync(appointment, base.Request.Path.Value!);

    [HttpGet()]
    [ProducesResponseType(typeof(GetAppointmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OneOf<GetAppointmentResponse, ErrorResponse>>> GetAsync([FromQuery] AppointmentParameters appointmentsParameters)
    {
        var appointmentsPaged = await _service.GetAppointmentAsync(appointmentsParameters);

        if (appointmentsPaged.Result is OkObjectResult response && response.Value is GetAppointmentResponse appointmentResponse)
        {
            var metadata = new
            {
                appointmentResponse.Appointments.TotalCount,
                appointmentResponse.Appointments.PageSize,
                appointmentResponse.Appointments.CurrentPage,
                appointmentResponse.Appointments.TotalPages,
                appointmentResponse.Appointments.HasNext,
                appointmentResponse.Appointments.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        }
        return appointmentsPaged;
    }

}