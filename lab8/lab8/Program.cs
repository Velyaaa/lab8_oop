namespace Lab8
{
    public enum Currency
    {
        UAH,
        USD,
        EUR,
        PLN,
        GBP
    }
    class Program
    {
        static void Main()
        {
            Wallet myWallet = new Wallet();
            myWallet.AddAccountTest("Privat", Currency.UAH, 15966);
            myWallet.AddAccountTest("Monobank", Currency.UAH, 200);
            myWallet.AddAccountTest("Deposit", Currency.USD, 1000);
            myWallet.AddAccountTest("Scholarship", Currency.UAH, 2000);

            Categories myCategories = new Categories();
            myCategories.AddCategory("Sport");
            myCategories.AddCategory("Books");
            myCategories.AddCategory("Clothes");
            myCategories.AddCategory("Scholarship");
            myCategories.AddCategory("P2P Transfer");

            myWallet.accounts[0].AddIncome(myCategories.categories[3], 300, new DateTime(2025, 4, 13), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[0], 1500, new DateTime(2025, 4, 13), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[1], 1250, new DateTime(2025, 4, 13), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[2], 167, new DateTime(2025, 4, 13), "");

            myWallet.accounts[0].AddIncome(myCategories.categories[3], 300, new DateTime(2025, 4, 12), "");
            myWallet.accounts[0].AddIncome(myCategories.categories[3], 250, new DateTime(2025, 4, 12), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[3], 500, new DateTime(2025, 4, 12), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[3], 1000, new DateTime(2025, 4, 12), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[1], 300, new DateTime(2025, 4, 12), "");
            myWallet.accounts[0].AddExpense(myCategories.categories[1], 908, new DateTime(2025, 4, 12), "");

            myWallet.accounts[0].AddIncome(myCategories.categories[0], 10000, new DateTime(2024, 8, 03), "");


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Доступний функціонал:");
                Console.WriteLine("1. Управління категоріями витрат та доходів");
                Console.WriteLine("2. Управління рахунками");
                Console.WriteLine("3. Управління витратами та доходами");
                Console.WriteLine("4. Пошук категорій та транзакцій");
                Console.WriteLine("0. Вийти з програми");
                Console.Write("Оберіть одну з вищезазначених опцій: ");
                int choice = InputHelper.ReadIntegerWithRetry(0, 4);

                if (choice == 0)
                    return;

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\n<-------------------------------------------------->");
                        Console.WriteLine("Доступні операції з вашими категоріями:");
                        Console.WriteLine("\t1. Додати нову категорію");
                        Console.WriteLine("\t2. Видалити наявну категорію");
                        Console.WriteLine("\t3. Змінити дані про категорію");
                        Console.WriteLine("\t4. Перегляд списку категорій");
                        Console.WriteLine("\t0. Вийти до головного меню");

                        Console.Write("Оберіть вашу наступну дію: ");
                        choice = InputHelper.ReadIntegerWithRetry(0, 4);
                        switch (choice)
                        {
                            case 0:
                                break;

                            case 1:
                                Console.Write("Введіть ім'я для нової категорії (або просто натисніть Enter для відміни дії, та виходу до головного меню): ");
                                string? name = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    break;
                                }
                                myCategories.AddCategory(name);

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myCategories.PrintCategoriesList();

                                Console.Write("Оберіть категорію, яку ви бажаєте видалити (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myCategories.CategoriesCount);
                                if (choice == 0)
                                {
                                    break;
                                }

                                bool removed = myCategories.RemoveCategory(myCategories.categories[choice - 1].Name);
                                Console.WriteLine(removed ? "Категорію видалено." : "Категорію не знайдено.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 3:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myCategories.PrintCategoriesList();

                                Console.Write("Оберіть категорію, інформацію про яку бажаєте змінити: ");
                                int categoryIndex = InputHelper.ReadIntegerWithRetry(1, myCategories.CategoriesCount);
                                myCategories.categories[categoryIndex - 1].ChangeCategoryInfo();

                                Console.WriteLine("Інформацію про категорію було успішно змінено.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();

                                break;

                            case 4:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myCategories.PrintCategoriesList();
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Сталася помилка. Некоректний ввід.");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("\n<-------------------------------------------------->");
                        Console.WriteLine("Доступні операції з вашими рахунками:");
                        Console.WriteLine("\t1. Додати новий рахунок");
                        Console.WriteLine("\t2. Видалити наявний рахунок");
                        Console.WriteLine("\t3. Змінити дані про наявний рахунок");
                        Console.WriteLine("\t4. Перегляд списку власних рахунків");
                        Console.WriteLine("\t5. Перегляд коштів на наявному рахунку");
                        Console.WriteLine("\t0. Вийти до головного меню");

                        Console.Write("Оберіть вашу наступну дію: ");
                        choice = InputHelper.ReadIntegerWithRetry(0, 5);

                        switch (choice)
                        {
                            case 0:
                                break;

                            case 1:
                                bool added = myWallet.AddAccount();
                                Console.WriteLine(added ? "✔ Рахунок створено успішно!" : "Сталася помилка, спробуйте ще раз.");

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();

                                Console.Write("Оберіть рахунок, який ви бажаєте видалити (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 0)
                                {
                                    break;
                                }

                                bool removed = myWallet.RemoveAccount(myWallet.accounts[choice - 1].Id);
                                Console.WriteLine(removed ? "Рахунок видалено." : "Рахунок не знайдено.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();

                                break;

                            case 3:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();

                                Console.Write("Оберіть рахунок, інформацію про який бажаєте змінити: ");
                                int accountIndex = InputHelper.ReadIntegerWithRetry(1, myWallet.AccountsCount);
                                myWallet.accounts[accountIndex - 1].ChangeAccountInfo();

                                Console.WriteLine("Інформацію про рахунок було успішно змінено.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();

                                break;

                            case 4:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 5:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();

                                Console.Write("Оберіть рахунок, інформацію про який ви бажаєте переглянути");
                                Console.WriteLine("(або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 0)
                                {
                                    break;
                                }
                                myWallet.accounts[choice - 1].PrintAccountInfo();

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Сталася помилка. Некоректний ввід.");
                                break;
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n<-------------------------------------------------->");
                        Console.WriteLine("Доступні операції з витратами та доходами:");
                        Console.WriteLine("\t1. Додати нову транзакцію");
                        Console.WriteLine("\t2. Видалити здійснену транзакцію");
                        Console.WriteLine("\t3. Перевести гроші з рахунку на рахунок");
                        Console.WriteLine("\t4. Перегляд прибутків та витрат за певний період");
                        Console.WriteLine("\t5. Перегляд статистики прибутків/витрат за певний період по днях, по категоріях");
                        Console.WriteLine("\t0. Вийти до головного меню");

                        Console.Write("Оберіть вашу наступну дію: ");
                        choice = InputHelper.ReadIntegerWithRetry(0, 5);
                        switch (choice)
                        {
                            case 0:
                                break;

                            case 1:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();
                                Console.Write("Оберіть рахунок, до якого ви бажаєте додати транзакцію (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 0)
                                {
                                    break;
                                }
                                if (!myWallet.accounts[choice - 1].AddTransaction(myCategories))
                                {
                                    break;
                                }

                                Console.WriteLine("Нову транзакцію було успішно створено.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();
                                Console.Write("Оберіть рахунок, у якого ви бажаєте видалити транзакцію (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.accounts[choice - 1].PrintTransactionsHistory();

                                if (myWallet.accounts[choice - 1].Transactions.Count == 0)
                                {
                                    Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                    Console.ReadKey();
                                    break;
                                }

                                Console.Write("Оберіть транзакцію яку ви бажаєте видалити: ");
                                int transactionChoice = InputHelper.ReadIntegerWithRetry(0, myWallet.accounts[choice - 1].transactionsCount);

                                if (myWallet.accounts[choice - 1].Transactions[transactionChoice - 1].Type == "Income")
                                {
                                    bool removed = myWallet.accounts[choice - 1].RemoveIncome(myWallet.accounts[choice - 1].Transactions[transactionChoice - 1].Id);
                                    Console.WriteLine(removed ? "Транзакцію видалено." : "Транзакцію не знайдено.");
                                }
                                else
                                {
                                    bool removed = myWallet.accounts[choice - 1].RemoveExpense(myWallet.accounts[choice - 1].Transactions[transactionChoice - 1].Id);
                                    Console.WriteLine(removed ? "Транзакцію видалено." : "Транзакцію не знайдено.");
                                }

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 3:
                                bool transferSuccess = myWallet.FundsTranfer(myCategories);
                                Console.WriteLine(transferSuccess ? "Переказ коштів пройшов успішно" : "Сталася помилка.");
                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 4:
                                Console.WriteLine("\n<-------------------------------------------------->");
                                myWallet.PrintAccountsList();

                                Console.Write("Оберіть рахунок, історю транзакцій якого ви бажаєте переглянути (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                int accountChoiceToCheck = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (accountChoiceToCheck == 0)
                                {
                                    break;
                                }

                                if (myWallet.accounts[accountChoiceToCheck - 1].transactionsCount == 0)
                                {
                                    myWallet.accounts[accountChoiceToCheck - 1].PrintTransactionsHistory();
                                    Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                    Console.ReadKey();
                                    break;
                                }

                                Console.Write("Оберіть: відобразити всю історію транзакцій(1) чи за певний період(2): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 1)
                                {
                                    myWallet.accounts[accountChoiceToCheck - 1].PrintTransactionsHistory();
                                }
                                else
                                {
                                    Console.Write("Введіть початкову дату у форматі ДД.ММ.РРРР: ");
                                    DateTime startDate = InputHelper.ReadDateWithRetry();

                                    Console.Write("Введіть кінцеву дату у форматі ДД.ММ.РРРР: ");
                                    DateTime endDate = InputHelper.ReadDateWithRetry();

                                    Console.WriteLine($"Ваша історія транзакцій в період з {startDate.ToShortDateString()} та {endDate.ToShortDateString()}:");
                                    myWallet.accounts[accountChoiceToCheck - 1].PrintTransactionsHistory(startDate, endDate);
                                }

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 5:
                                myWallet.PrintAccountsList();
                                Console.Write("Оберіть рахунок, чию статистику ви бажаєте отримати (або введіть 0 для відміни дії, та виходу до головного меню): ");
                                choice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                                if (choice == 0)
                                {
                                    break;
                                }
                                myWallet.accounts[choice - 1].ShowStatistics(myCategories);

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Сталася помилка. Некоректний ввід.");
                                break;
                        }
                        break;

                    case 4:
                        Console.WriteLine("\n<-------------------------------------------------->");
                        Console.WriteLine("Доступні операції пошуку:");
                        Console.WriteLine("\t1. Пошук витрат та прибутків за категорією");
                        Console.WriteLine("\t2. Пошук витрат та прибутків за сумою");
                        Console.WriteLine("\t3. Пошук витрат та прибутків за датою");
                        Console.WriteLine("\t0. Вийти до головного меню");

                        Console.Write("Оберіть вашу наступну дію: ");
                        choice = InputHelper.ReadIntegerWithRetry(0, 3);
                        if (choice == 0)
                        {
                            break;
                        }

                        myWallet.PrintAccountsList();
                        Console.Write("Оберіть рахунок, чию транзакцію ви бажаєте знайти (або введіть 0 для відміни дії, та виходу до головного меню): ");
                        int accountChoice = InputHelper.ReadIntegerWithRetry(0, myWallet.AccountsCount);
                        if (accountChoice == 0)
                        {
                            break;
                        }

                        switch (choice)
                        {
                            case 1:
                                myWallet.accounts[accountChoice - 1].SearchByCategory(myCategories);

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 2:
                                myWallet.accounts[accountChoice - 1].SearchBySum();

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            case 3:
                                myWallet.accounts[accountChoice - 1].SearchByDate();

                                Console.WriteLine("Натисніть будь-яку клавішу для повернення до меню...");
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Сталася помилка. Некоректний ввід.");
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("Сталася помилка. Некоректний ввід.");
                        break;
                }
            }
        }
    }
}

