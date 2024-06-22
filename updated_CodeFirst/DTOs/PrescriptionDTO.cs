using System;
using System.Collections.Generic;

namespace CodeFirst.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DoctorDTO Doctor { get; set; }
        public List<MedicamentDTO> Medicaments { get; set; }
    }
}