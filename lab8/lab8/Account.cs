using Lab8;

class Account
{
    public string Name { get; set; }
    public Currency Currency { get; set; }
    public decimal Balance { get; set; }
    public Guid Id { get; set; }
    public List<ITransaction> Transactions = new();
    public int transactionsCount => Transactions.Count;

    public Account(string name, Currency currency, decimal balance = 0)
    {
        Id = Guid.NewGuid();
        Name = name;
        Currency = currency;
        Balance = balance;
    }

    public void PrintAccountInfo()
    {
        Console.WriteLine("<-------------------------------------------------->");
        Console.WriteLine($"Ім'я рахунку: {Name}");
        Console.WriteLine($"Баланс на рахунку: {Balance} {Currency}");
    }

    public void ChangeAccountInfo()
    {
        Console.WriteLine($"Поточна інформація про даний рахунок:");
        PrintAccountInfo();

        Console.Write("Введіть нове ім'я рахунку (або просто натисніть Enter щоб залишити без змін): ");
        string? inputName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(inputName))
        {
            Name = inputName;
        }

        Console.WriteLine("Оберіть нову валюту рахунку (або введіть -1 щоб залишити без змін).");
        foreach (var (value, i) in Enum.GetValues<Currency>().Select((v, i) => (v, i + 1)))
        {
            Console.WriteLine($"{i}. ({value})");
        }
        Console.Write("-> ");
        int currencyChoise = InputHelper.ReadIntegerWithRetry(-1, Enum.GetValues<Currency>().Length);
        if (currencyChoise != -1)
        {
            Currency = (Currency)(currencyChoise - 1);
        }

        Console.Write("Введіть новий баланс для цього рахунку (або введіть -1 щоб залишити без змін): ");
        decimal newBalance = InputHelper.ReadDecimalWithRetry(-1, decimal.MaxValue);
        if (newBalance != -1)
        {
            Balance = newBalance;
        }

        Console.WriteLine($"Нова інформація про даний рахунок:");
        PrintAccountInfo();
    }

    public void AddIncome(Category category, decimal sum, DateTime dateAndTime, string description = "")
    {
        Income newIncome = new Income(category, sum, dateAndTime, description);
        Transactions.Add(newIncome);
        Balance += sum;
    }

    public bool RemoveIncome(Guid id)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction != null)
        {
            Balance -= transaction.Sum;

            Transactions.Remove(transaction);
            return true;
        }
        return false;
    }
    public void AddExpense(Category category, decimal sum, DateTime dateAndTime, string description = "")
    {
        Expense newExpense = new Expense(category, sum, dateAndTime, description);
        Transactions.Add(newExpense);
        if (Balance >= sum)
        {
            Balance -= sum;
        }
        else
        {
            Console.WriteLine("На рахунку недостатньо коштів для здійснення операції.");
        }
    }

    public bool RemoveExpense(Guid id)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction != null)
        {
            Balance += transaction.Sum;

            Transactions.Remove(transaction);
            return true;
        }
        return false;
    }

    public bool AddTransaction(Categories myCategories)
    {
        Console.WriteLine("Оберіть тип транзакції: витрата (1), дохід (2) або введіть 0 для відміни");
        int typeChoice = InputHelper.ReadIntegerWithRetry(0, 2);
        if (typeChoice == 0)
        {
            return false;
        }

        Console.WriteLine("\n<-------------------------------------------------->");
        myCategories.PrintCategoriesList();
        Console.Write("Оберіть категорію, до якої належатиме дана транзакція (або введіть 0 для відміни дії, та виходу до головного меню): ");
        int categoriyChoice = InputHelper.ReadIntegerWithRetry(0, myCategories.CategoriesCount);
        if (categoriyChoice == 0)
        {
            return false;
        }

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.Write("Введіть дату транзакції у форматі ДД.ММ.РРРР: ");
        DateTime date = InputHelper.ReadDateWithRetry();

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.Write("Введіть суму транзакції: ");
        decimal sum = InputHelper.ReadDecimalWithRetry(0, decimal.MaxValue);

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.Write("Введіть опис транзакції (або просто натисніть Enter, щоб залишити її без опису): ");
        string? description = Console.ReadLine();

        switch (typeChoice)
        {
            case 1:
                AddExpense(myCategories.categories[categoriyChoice - 1], sum, date, description!);
                break;

            case 2:
                AddIncome(myCategories.categories[categoriyChoice - 1], sum, date, description!);
                break;

            default:
                Console.WriteLine("Сталася помилка. Некоректний ввід.");
                break;
        }
        return true;
    }

    public void PrintTransactionsHistory(DateTime startDate = default, DateTime endDate = default)
    {
        if (Transactions.Count == 0)
        {
            Console.WriteLine("На цьому рахунку ще не було жодних транзакцій.");
            return;
        }
        if (startDate == default)
            startDate = DateTime.MinValue;

        if (endDate == default)
            endDate = DateTime.MaxValue;

        Transactions = Transactions.OrderByDescending(t => t.Date).ToList();

        for (int i = 0; i < transactionsCount; i++)
        {
            if ((Transactions[i].Date >= startDate && Transactions[i].Date <= endDate)
             || (Transactions[i].Date <= startDate && Transactions[i].Date >= endDate))
            {
                Console.WriteLine("\n<-------------------------------------------------->");
                Console.WriteLine($"Транзакція № {i + 1}");
                Console.WriteLine($"Тип транзакції: {Transactions[i].Type}");
                Console.WriteLine($"Дата: {Transactions[i].Date.ToShortDateString()}");
                Console.WriteLine($"Сума: {Transactions[i].Sum}");
                Console.WriteLine($"Категорія: {Transactions[i].Category!.Name}");
                if (!string.IsNullOrWhiteSpace(Transactions[i].Description))
                {
                    Console.WriteLine($"Опис: {Transactions[i].Description}");
                }
            }
        }

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.WriteLine($"\nПоточний баланс: {Balance}");
    }

    public void SearchByCategory(Categories myCategories)
    {
        Console.WriteLine("\n<-------------------------------------------------->");
        myCategories.PrintCategoriesList();

        Console.Write("Оберіть категорію, за якою бажаєте здійснити пошук: ");
        int choice = InputHelper.ReadIntegerWithRetry(1, myCategories.CategoriesCount);

        Console.WriteLine($"\nСписок транзакцій у категорії '{myCategories.categories[choice - 1].Name}': ");
        for (int i = 0; i < transactionsCount; i++)
        {
            if (Transactions[i].Category == myCategories.categories[choice - 1])
            {
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine($"Транзакція № {i + 1}");
                Console.WriteLine($"Тип транзакції: {Transactions[i].Type}");
                Console.WriteLine($"Дата: {Transactions[i].Date.ToShortDateString()}");
                Console.WriteLine($"Сума: {Transactions[i].Sum}");
                Console.WriteLine($"Категорія: {Transactions[i].Category!.Name}");
                if (!string.IsNullOrWhiteSpace(Transactions[i].Description))
                {
                    Console.WriteLine($"Опис: {Transactions[i].Description}");
                }
            }
        }
    }

    public void SearchBySum()
    {
        Console.WriteLine("\n<-------------------------------------------------->");
        Console.Write("Введіть суму транзакції: ");
        decimal sum = InputHelper.ReadDecimalWithRetry(0, decimal.MaxValue);

        Console.WriteLine($"\nСписок транзакцій з сумою '{sum}': ");
        for (int i = 0; i < transactionsCount; i++)
        {
            if (sum == Transactions[i].Sum)
            {
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine($"Транзакція № {i + 1}");
                Console.WriteLine($"Тип транзакції: {Transactions[i].Type}");
                Console.WriteLine($"Дата: {Transactions[i].Date.ToShortDateString()}");
                Console.WriteLine($"Сума: {Transactions[i].Sum}");
                Console.WriteLine($"Категорія: {Transactions[i].Category!.Name}");
                if (!string.IsNullOrWhiteSpace(Transactions[i].Description))
                {
                    Console.WriteLine($"Опис: {Transactions[i].Description}");
                }
            }
        }
    }

    public void SearchByDate()
    {
        Console.WriteLine("\n<-------------------------------------------------->");
        Console.Write("Введіть дату транзакції у форматі ДД.ММ.РРРР: ");
        DateTime date = InputHelper.ReadDateWithRetry();

        Console.WriteLine($"\nСписок транзакцій за датою '{date.ToShortDateString()}': ");
        for (int i = 0; i < transactionsCount; i++)
        {
            if (date == Transactions[i].Date)
            {
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine($"Транзакція № {i + 1}");
                Console.WriteLine($"Тип транзакції: {Transactions[i].Type}");
                Console.WriteLine($"Дата: {Transactions[i].Date.ToShortDateString()}");
                Console.WriteLine($"Сума: {Transactions[i].Sum}");
                Console.WriteLine($"Категорія: {Transactions[i].Category!.Name}");
                if (!string.IsNullOrWhiteSpace(Transactions[i].Description))
                {
                    Console.WriteLine($"Опис: {Transactions[i].Description}");
                }
            }
        }
    }

    public void ShowStatistics(Categories myCategories)
    {
        Console.Write("Введіть початкову дату у форматі ДД.ММ.РРРР: ");
        DateTime startDate = InputHelper.ReadDateWithRetry();

        Console.Write("Введіть кінцеву дату у форматі ДД.ММ.РРРР: ");
        DateTime endDate = InputHelper.ReadDateWithRetry();

        if (startDate > endDate)
        {
            DateTime temp = startDate;
            startDate = endDate;
            endDate = temp;
        }
        decimal incomeSum = 0;
        decimal expenseSum = 0;

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.WriteLine($"Статистика по всіх транзакціях з {startDate.ToShortDateString()} до {endDate.ToShortDateString()}");
        for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
        {
            incomeSum = 0;
            expenseSum = 0;
            foreach (var transaction in Transactions)
            {
                if (transaction.Date == currentDate)
                {
                    if (transaction.Type == "Income")
                    {
                        incomeSum += transaction.Sum;
                    }
                    else
                    {
                        expenseSum += transaction.Sum;
                    }
                }
            }
            Console.WriteLine($"{currentDate.ToShortDateString()} — Прибуток: {incomeSum} {Currency} | Витрати: {expenseSum} {Currency}");
        }

        Console.WriteLine("\n<-------------------------------------------------->");
        Console.WriteLine($"Статистика по всіх витратах, розподілених за категоріями з {startDate.ToShortDateString()} до {endDate.ToShortDateString()}");
        foreach (var category in myCategories.categories)
        {
            incomeSum = 0;
            expenseSum = 0;
            for (int i = 0; i < transactionsCount; i++)
            {
                if (Transactions[i].Date >= startDate && Transactions[i].Date <= endDate)
                {
                    if (Transactions[i].Category == category)
                    {
                        if (Transactions[i].Type == "Income")
                        {
                            incomeSum += Transactions[i].Sum;
                        }
                        else
                        {
                            expenseSum += Transactions[i].Sum;
                        }
                    }
                }
            }
            Console.WriteLine($"Категорія '{category.Name}' — Витрати: {expenseSum} {Currency} | Прибуток: {incomeSum} {Currency}");
        }

        incomeSum = 0;
        expenseSum = 0;
        foreach (var transaction in Transactions)
        {
            if (transaction.Date >= startDate && transaction.Date <= endDate)
            {
                if (transaction.Type == "Income")
                {
                    incomeSum += transaction.Sum;
                }
                else
                {
                    expenseSum += transaction.Sum;
                }
            }
        }
        
        Console.WriteLine("Загалом за період:");
        Console.WriteLine($"— Всього прибутку: {incomeSum} {Currency}");
        Console.WriteLine($"— Всього витрат: {expenseSum} {Currency}");
        Console.WriteLine($"— Чистий прибуток: {incomeSum - expenseSum} {Currency}");
    }
}