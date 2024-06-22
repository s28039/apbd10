
using CodeFirst.Data;
using CodeFirst.DTOs;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly MyContext _context;

        public PrescriptionService(MyContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPrescriptionAsync(PrescriptionRequestDTO request)
        {
            var patient = await _context.Patients.FindAsync(request.PatientId);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = request.PatientFirstName,
                    LastName = request.PatientLastName,
                    Birthdate = request.PatientDateOfBirth
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
            {
                return false;
            }

            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = doctor.IdDoctor
            };

            foreach (var medicamentDto in request.Medicaments)
            {
                var medicament = await _context.Medicaments.FindAsync(medicamentDto.Id);
                if (medicament == null)
                {
                    return false;
                }

                prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = medicament.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description
                });
            }

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == patientId);

            if (patient == null)
            {
                return null;
            }

            var patientDetails = new PatientDetailsDTO
            {
                Id = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = patient.Prescriptions.Select(p => new PrescriptionDTO
                {
                    Id = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDTO
                    {
                        Id = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Specialization = p.Doctor.Email // Assuming specialization is stored in Email field
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDTO
                    {
                        Id = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList()
                }).OrderBy(p => p.DueDate).ToList()
            };

            return patientDetails;
        }
    }
}
