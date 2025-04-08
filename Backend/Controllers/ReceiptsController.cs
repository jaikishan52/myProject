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
            try
            {
                // ✅ Step 1: Validate all inputs before proceeding

                if (string.IsNullOrWhiteSpace(dto.EmployeeEmail) ||
                    !dto.EmployeeEmail.Contains("@") ||
                    !dto.EmployeeEmail.EndsWith("@university.edu"))
                {
                    return StatusCode(400, new { detail = "Please enter a valid university email address." });
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { detail = "Receipt file is required." });
                }

                if (dto.Amount <= 0)
                {
                    return BadRequest(new { detail = "Amount should be greater than zero." });
                }

                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    return BadRequest(new { detail = "Description is required." });
                }

                if (dto.Date == default(DateTime))
                {
                    return BadRequest(new { detail = "Please provide a valid date." });
                }

                // ✅ Step 2: Process file upload after validation
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "UploadedReceipts");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Optional: Sanitize file name to avoid special characters or spaces
                var safeFileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "_") + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, safeFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // ✅ Step 3: Save to database
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, new { detail = "An error occurred while submitting the receipt.", error = ex.Message });
            }
        }
    }
}
