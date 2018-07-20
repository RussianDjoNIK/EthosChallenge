using System;
using EthosChallenge.Models;
using Xunit;

namespace EthosChallenge.Tests
{
    public class Calculator_CheckProcessing
    {
        [Fact]
        public void Calculator_StandardInput()
        {
            var standartInput = new InputRequest{Amount = 100000m, Interest = 5.5m, Downpayment = 20000m, Term = 30m};
            var res = Calculator.Process(standartInput);

            Assert.Equal(454.23m, res.MonthlyPayment);
            Assert.Equal(83523.23m, res.TotalInterest);
            Assert.Equal(163523.23m, res.TotalPayment);
        }

        [Fact]
        public void Calculator_DownpaymentOutOfRangeTest()
        {
            var wrongDownpayment = new InputRequest { Amount = 210m, Interest = 5.5m, Downpayment = 220m, Term = 3m};

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Process(wrongDownpayment));
            Assert.Equal("Downpayment (220) have to be less than Amount (210) and greater than 0\r\nParameter name: input", exception.Message);

            wrongDownpayment = new InputRequest { Amount = 210m, Interest = 5.5m, Downpayment = -1, Term = 3m };

            exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Process(wrongDownpayment));
            Assert.Equal("Downpayment (-1) have to be less than Amount (210) and greater than 0\r\nParameter name: input", exception.Message);
        }

        [Fact]
        public void Calculator_TermOutOfRangeTest()
        {
            var wrongTerm = new InputRequest { Amount = 210m, Interest = 5.5m, Downpayment = 100m, Term = -1m };

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Process(wrongTerm));
            Assert.Equal("Term in years (-1) have to be greater than 0 and less than 90\r\nParameter name: input", exception.Message);

            wrongTerm = new InputRequest { Amount = 210m, Interest = 5.5m, Downpayment = -1m, Term = 100m };

            exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Process(wrongTerm));
            Assert.Equal("Term in years (100) have to be greater than 0 and less than 90\r\nParameter name: input", exception.Message);
        }

        [Fact]
        public void Calculator_AmountOutOfRangeTest()
        {
            var wrongDownpayment = new InputRequest { Amount = -1m, Interest = 5.5m, Downpayment = 220m, Term = 3m };

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Process(wrongDownpayment));
            Assert.Equal("Amount (-1) have to be greater than 0\r\nParameter name: input", exception.Message);
        }
    }
}
