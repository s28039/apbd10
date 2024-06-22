using CodeFirst.DTOs;
using System.Threading.Tasks;

namespace CodeFirst.Services
{
    public interface IPrescriptionService
    {
        Task<bool> AddPrescriptionAsync(PrescriptionRequestDTO request);
        Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId);
    }
}