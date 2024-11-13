namespace RomanNumerals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var converter = new StandardConverter();
            Console.WriteLine("  Roman Numeral Converter  ");
            Console.WriteLine("===========================\n");

            Console.WriteLine("Enter a number to convert to numerals, or numerals to convert to a number:");

            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.ToUpper() == "N")
                {
                    break;
                }
                else if (int.TryParse(input, out var value))
                {
                    try
                    {
                        Console.WriteLine($"{input} in Roman numerals is {converter.ToRomanNumerals(value)}\n");
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine($"{input} is out of range, value must be between 1 and 3999\n");
                    }
                }
                else
                {
                    try
                    {
                        Console.WriteLine($"{input} as a number is {converter.FromRomanNumerals(input)}\n");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine($"{input} is invalid, can only contain one of 'I, V, X, L, C, D, M'\n");
                    }
                }
                Console.WriteLine("Enter another number or numerals to convert, or press Enter or 'n' to exit:");
            }
        }
    }
}
