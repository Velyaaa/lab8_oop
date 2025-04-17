using Lab8;

class Wallet
{
    public List<Account> accounts = new();
    public int AccountsCount => accounts.Count;

    public void AddAccountTest(string name, Currency currency, decimal balance = 0)
    {
        Account account = new Account(name, currency, balance);
        accounts.Add(account);
    }

    public bool AddAccount()
    {
        Console.Write("Введіть ім'я для нового рахунку (або просто натисніть Enter для відміни дії, та виходу до головного меню): ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        Console.WriteLine("Список доступних валют:");
        foreach (var (value, i) in Enum.GetValues<Currency>().Select((v, i) => (v, i + 1)))
        {
            Console.WriteLine($"{i}. ({value})");
        }
        Console.Write("Оберіть валюту нового рахунку (або введіть -1 для відміни дії, та виходу до головного меню): ");
        int currencyChoise = InputHelper.ReadIntegerWithRetry(-1, Enum.GetValues<Currency>().Length);
        if (currencyChoise == -1)
        {
            return false;
        }

        Console.Write("Введіть баланс для цього рахунку (або введіть -1 для відміни дії, та виходу до головного меню): ");
        decimal balance = InputHelper.ReadDecimalWithRetry(-1, decimal.MaxValue);
        if (balance == -1)
        {
            return false;
        }
        Account account = new Account(name, (Currency)currencyChoise - 1, balance);
        accounts.Add(account);

        return true;
    }
    public bool RemoveAccount(Guid id) =>
        accounts.RemoveAll(a => a.Id == id) > 0;

    public void PrintAccountsList()
    {
        Console.WriteLine("Список ваших доступних рахунків:");
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"Ім'я рахунку №{i + 1}: {accounts[i].Name}");
        }
        Console.WriteLine("<-------------------------------------------------->");
    }

    public bool FundsTranfer(Categories myCategories)
    {
        Console.WriteLine("<-------------------------------------------------->");
        PrintAccountsList();
        Console.Write("Оберіть рахунок, з якого будуть переведені кошти (або введіть 0 для відміни): ");
        int accountSenderIndex = InputHelper.ReadIntegerWithRetry(0, AccountsCount);
        if (accountSenderIndex == 0)
        {
            return false;
        }

        Console.Write("Оберіть рахунок, на який будуть переведені кошти (або введіть 0 для відміни): ");
        int accountAddresseeIndex = InputHelper.ReadIntegerWithRetry(0, AccountsCount);
        if (accountAddresseeIndex == 0)
        {
            return false;
        }

        if (accounts[accountSenderIndex - 1].Currency != accounts[accountAddresseeIndex - 1].Currency)
        {
            Console.WriteLine("Переведення коштів можливе лише між рахунками з однаковими валютами.");
            
            return false;
        }
        
        Console.Write("Введіть суму переказу: ");
        decimal sum = InputHelper.ReadDecimalWithRetry(0, accounts[accountSenderIndex - 1].Balance);

        bool categoryFound = false;
        for (int i = 0; i < myCategories.CategoriesCount; i++)
        {
            if (myCategories.categories[i].Name == "P2P Transfer")
            {
                accounts[accountSenderIndex - 1].AddExpense(myCategories.categories[i], sum, DateTime.Now);
                categoryFound = true;
            }
        }
        if (categoryFound == false)
        {
            myCategories.AddCategory("P2P Transfer");
            accounts[accountSenderIndex - 1].AddExpense(myCategories.categories.Last(), sum, DateTime.Now);
        }

        accounts[accountAddresseeIndex - 1].AddIncome(myCategories.categories.Last(), sum, DateTime.Now);

        return true;
    }
}