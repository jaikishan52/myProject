namespace Backend.Models
{
    public class ReceiptDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;
    }
}
