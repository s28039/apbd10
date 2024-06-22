using System;
using System.Collections.Generic;

namespace CodeFirst.DTOs
{
    public class PrescriptionRequestDTO
    {
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientDateOfBirth { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentDTO> Medicaments { get; set; }
    }
}