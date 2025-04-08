using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReceiptsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitReceipt([FromForm] ReceiptDto dto, IFormFile file)
        {
            if (!dto.EmployeeEmail.EndsWith("@university.edu"))
            {
                return StatusCode(403, new { detail = "Access restricted to university employees." });
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Receipt file is required.");
            }

            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "UploadedReceipts");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var receipt = new Receipt
            {
                Date = dto.Date,
                Amount = dto.Amount,
                Description = dto.Description,
                FilePath = filePath,
                EmployeeEmail = dto.EmployeeEmail
            };

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
           

            return Ok(new { message = "Receipt submitted successfully!", receiptId = receipt.Id });
        }
    }
}
