using System;
using System.Globalization;
using Xunit;

namespace EthosChallenge.Tests
{
    public class InputRequestFabric_CheckParsing
    {
        private static readonly CultureInfo UsCulture = new CultureInfo("en-US");
        private static readonly CultureInfo FrCulture = new CultureInfo("fr-FR");

        [Fact]
        public void InputRequestFabric_ArgumentsLength()
        {
            string[] args = { "arg1" };
            var ex = Assert.Throws<ArgumentException>(() => InputRequestFabric.Parse(args, UsCulture));
            Assert.Equal("Input have to have at least 8 parameters", ex.Message);
        }

        [Fact]
        public void InputRequestFabric_StandartArguments()
        {
            string[] args = { "amount:", "100000", "interest:", "5.5 %", "downpayment:", "20000", "term:", "30" };
            var input = InputRequestFabric.Parse(args, UsCulture);

            Assert.Equal(100000m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);
        }

        [Fact]
        public void InputRequestFabric_StandartArgumentsPercentWithWhiteSpaceTest()
        {
            string[] args = { "amount:", "100000", "interest:", "5.5 %", "downpayment:", "20000", "term:", "30" };
            var input = InputRequestFabric.Parse(args, UsCulture);

            Assert.Equal(100000m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);
        }

        [Fact]
        public void InputRequestFabric_StandartArgumentsPercentWithoutWhiteSpaceTest()
        {
            string[] args = { "amount:", "100000", "interest:", "5.5%", "downpayment:", "20000", "term:", "30" };
            var input = InputRequestFabric.Parse(args, UsCulture);

            Assert.Equal(100000m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);
        }

        [Fact]
        public void InputRequestFabric_StandartArgumentsDifferentAmountInputTest()
        {
            string[] args = { "amount:", "100 000", "interest:", "5,5%", "downpayment:", "20 000", "term:", "30" };
            var input = InputRequestFabric.Parse(args, FrCulture);

            Assert.Equal(100000m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);

            args = new[] { "amount:", "100,000", "interest:", "5 .5%", "downpayment:", "20000", "term:", "30" };
            input = InputRequestFabric.Parse(args, UsCulture);

            Assert.Equal(100000m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);

            args = new[] { "amount:", "100,000.12", "interest:", "5.5%", "downpayment:", "20000", "term:", "30" };
            input = InputRequestFabric.Parse(args, UsCulture);

            Assert.Equal(100000.12m, input.Amount);
            Assert.Equal(5.5m, input.Interest);
            Assert.Equal(20000m, input.Downpayment);
            Assert.Equal(30m, input.Term);
        }
    }
}
