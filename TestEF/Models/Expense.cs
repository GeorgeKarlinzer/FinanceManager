namespace TestEF.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Label { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual ExpenseCategory Category { get; set; }
        public float Amount { get; set; }
    }
}
