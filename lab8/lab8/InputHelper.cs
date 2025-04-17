using System.Globalization;

public static class InputHelper
{
    public static int ReadIntegerWithRetry(int min, int max)
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int result))
            {
                Console.Write($"Невірний формат. Введіть ціле число від {min} до {max}: ");
                continue;
            }

            if (result < min || result > max)
            {
                Console.Write($"Введіть ціле число від {min} до {max}: ");
                continue;
            }

            return result;
        }
    }

    public static decimal ReadDecimalWithRetry(decimal min, decimal max)
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (!decimal.TryParse(input, out decimal result))
            {
                Console.Write($"Невірний формат. Введіть дійсне число від {min} до {max}: ");
                continue;
            }

            if (result < min || result > max || (result > -1 && result < 0))
            {
                Console.Write($"Число має бути в межах від {min + 1} до {max}, або -1.");
                continue;
            }

            return result;
        }
    }

    public static DateTime ReadDateWithRetry()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            Console.Write("Некоректний формат дати. Спробуйте ще раз: ");
        }
    }
}
