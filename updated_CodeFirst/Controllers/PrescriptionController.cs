using CodeFirst.DTOs;
using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionRequestDTO request)
        {
            if (request == null || request.Medicaments == null || request.Medicaments.Count > 10)
            {
                return BadRequest("Invalid request data or too many medicaments.");
            }

            bool success = await _service.AddPrescriptionAsync(request);

            if (!success)
            {
                return BadRequest("Failed to add prescription.");
            }

            return Ok("Prescription successfully created.");
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> RetrievePatientDetails(int patientId)
        {
            var details = await _service.GetPatientDetailsAsync(patientId);

            if (details == null)
            {
                return NotFound("Patient not found.");
            }

            return Ok(details);
        }
    }
}