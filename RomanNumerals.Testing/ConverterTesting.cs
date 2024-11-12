namespace RomanNumerals.Testing
{
    [TestClass]
    public class ConverterTesting
    {
        private StandardConverter _testObject;

        public static IEnumerable<object[]> SingleNumerals
        {
            get => new[]
            {
                new object[] { 'I', 1},
                new object[] { 'V', 5},
                new object[] { 'X', 10},
                new object[] { 'L', 50},
                new object[] { 'C', 100},
                new object[] { 'D', 500},
                new object[] { 'M', 1000}
            };
        }

        public static IEnumerable<object[]> InvalidNumerals
        {
            get => new[]
            {
                new object[] { "H" },
                new object[] { "A" },
                new object[] { "B" },
                new object[] { "E" },
                new object[] { "F" },
                new object[] { "CDIZ" },
                new object[] { "BLI" },
            };
        }

        public static IEnumerable<object[]> MultipleNumerals
        {
            get => new[]
            {
                new object[] { "IV", 4 },
                new object[] { "IX", 9 },
                new object[] { "XXI", 21 },
                new object[] { "XXXIV", 34 },
                new object[] { "XC", 90 },
                new object[] { "CXXIII", 123 },
                new object[] { "XLIV", 44 },
                new object[] { "CDXXXIII", 433 },
                new object[] { "CMLXXIV", 974 },
                new object[] { "CMXCIX", 999 },
            };
        }

        [TestInitialize]
        public void Init()
        {
            _testObject = new StandardConverter();
        }

        [TestMethod]
        [DynamicData(nameof(InvalidNumerals))]
        public void TestThrowsException(string invalidRomanNumerals)
        {
            Assert.ThrowsException<InvalidCastException>(() => 
                _testObject.FromRomanNumerals(invalidRomanNumerals));
        }

        [TestMethod]
        [DynamicData(nameof(SingleNumerals))]
        public void TestSingleNumeralToInt(char numeral, int number)
        {
            var convertedNumber = _testObject.FromRomanNumerals(numeral.ToString());
            Assert.AreEqual(number, convertedNumber);
        }

        [TestMethod]
        [DynamicData(nameof(MultipleNumerals))]
        public void TestMultipleNumeralsToInt(string numerals, int number)
        {
            var convertedNumber = _testObject.FromRomanNumerals(numerals.ToString());
            Assert.AreEqual(number, convertedNumber);
        }

        [TestMethod]
        [DynamicData(nameof(SingleNumerals))]
        public void TestNumberToSingleNumeral(char numeral, int number)
        {
            var convertedNumeral = _testObject.ToRomanNumerals(number);
            Assert.AreEqual(numeral.ToString(), convertedNumeral);
        }

        [TestMethod]
        [DynamicData(nameof(MultipleNumerals))]
        public void TestNumberToMultipleNumerals(string numerals, int number)
        {
            var convertedNumeral = _testObject.ToRomanNumerals(number);
            Assert.AreEqual(numerals, convertedNumeral);
        }
    }
}