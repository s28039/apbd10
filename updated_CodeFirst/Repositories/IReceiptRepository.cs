using CodeFirst.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Repositories;

public interface IReceiptRepository
{
    Task<IActionResult> CreatePrescription(PrescriptionRequest request);
}