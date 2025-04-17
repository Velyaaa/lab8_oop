class Categories
{
    public List<Category> categories = new();
    public int CategoriesCount => categories.Count;

    public void AddCategory(string name)
    {
        Category newCategory = new Category(name);
        categories.Add(newCategory);
    }

    public bool RemoveCategory(string name) =>
        categories.RemoveAll(a => a.Name == name) > 0;

    public void PrintCategoriesList()
    {
        Console.WriteLine("Список доступних категорій:");
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"Ім'я категорії №{i + 1}: {categories[i].Name}");
        }
        Console.WriteLine("<-------------------------------------------------->");
    }
}