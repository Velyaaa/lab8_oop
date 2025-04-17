class Category
{
    public string Name { get; set; }
    public Category(string name)
    {
        Name = name;
    }

    public void ChangeCategoryInfo()
    {
        Console.WriteLine($"Поточне ім'я категорії: {Name}");

        Console.Write("Введіть нове ім'я рахунку (або просто натисніть Enter щоб залишити без змін): ");
        string? inputName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(inputName))
        {
            Name = inputName;
        }

        Console.WriteLine($"Нова інформація про даний рахунок: {Name}");
    }
}