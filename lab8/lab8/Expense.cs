class Expense : ITransaction
{
    public Category? Category { get; set; }
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
    public string? Description { get; set; }
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public Expense(Category category, decimal sum, DateTime date, string description = "")
    {
        Id = Guid.NewGuid();
        Description = description;
        Category = category;
        Sum = sum;
        Date = date;
        Type = "Expense";
    }
}