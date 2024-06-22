using CodeFirst.Requests;
using Microsoft.AspNetCore.Mvc;


namespace CodeFirst.Controllers
{
    public interface IPrescriptionController
    {
        Task<IActionResult> AddNewPrescription([FromBody] PrescriptionRequest request);
        Task<IActionResult> GetPatientDetails(int idPatient);
    }
}