using System.Collections.Generic;

namespace CodeFirst.DTOs
{
    public class PatientDetailsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PrescriptionDTO> Prescriptions { get; set; }
    }
}