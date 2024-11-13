using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public class StandardConverter
    {
        private (char numeral, int number)[] _values = new[] 
        { 
            ('M', 1000),
            ('D', 500),
            ('C', 100),
            ('L', 50),
            ('X', 10),
            ('V', 5),
            ('I', 1)
        }; 

        public int FromRomanNumerals(string numerals)
        {
            var count = 0;
            var prevChar = ' ';
            foreach (var character in numerals.ToUpper())
            {
                var charValue = GetIntFromChar(character) ?? throw new ArgumentException();
                count += charValue;
                // have to remove twice the value of the previous char if it is
                // a subtractive character, since we also added the value of the
                // character in the previous iteration of the loop.
                count -= 2 * SubractiveAmount(character, prevChar);
                prevChar = character;
            }
            return count;
        }

        public string ToRomanNumerals(int number)
        {
            if (number < 1 || number > 4000)
            {
                throw new ArgumentOutOfRangeException();
            }

            var builder = new StringBuilder();

            for (int i = 0; i < _values.Length; i++)
            {
                var value = _values[i];
                var count = number / value.number;

                if (count == 1 &&!IsPowerOfTen(value.numeral) && number / _values[i + 1].number == 9)
                {
                    builder.Append(_values[i + 1].numeral);
                    builder.Append(_values[i - 1].numeral);
                    number -= _values[i + 1].number * 9;
                }
                else if (count == 4)
                {
                    builder.Append(value.numeral);
                    builder.Append(_values[i - 1].numeral);
                    number -= value.number * 4;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        builder.Append(value.numeral);
                        number -= value.number;
                    }
                }
            }
            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int? GetIntFromChar(char c) =>
            c switch
            {
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1000,
                _ => null
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SubractiveAmount(char currentChar, char prevChar)
        {
            switch (currentChar)
            {
                case 'V':
                case 'X':
                    return prevChar == 'I' ? 1 : 0;
                case 'L':
                case 'C':
                    return prevChar == 'X' ? 10 : 0;
                case 'D':
                case 'M':
                    return prevChar == 'C' ? 100 : 0;
                default:
                    return 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsPowerOfTen(char numeral) =>
            numeral switch
            {
                'M' => true,
                'C' => true,
                'X' => true,
                'I' => true,
                _ => false
            };
    }
}
