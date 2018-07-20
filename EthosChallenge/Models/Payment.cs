using System;
using Newtonsoft.Json;

namespace EthosChallenge.Models
{
    /// <summary>
    /// Calculation result structure 
    /// </summary>
    public struct Payment
    {
        /// <summary>
        /// Output values rounding coefficient
        /// </summary>
        private const int RoundingCoef = 2;

        private decimal _monthlyPaymentOrigin;
        private decimal _totalPaymentOrigin;
        private decimal _totalInterestOrigin;

        /// <summary>
        /// Payment value per month
        /// </summary>
        [JsonProperty(PropertyName = "monthly payment")]
        public decimal MonthlyPayment { get; }

        /// <summary>
        /// Total payment value
        /// </summary>
        [JsonProperty(PropertyName = "total payment")]
        public decimal TotalPayment { get; }

        /// <summary>
        /// Value of payment over loan
        /// </summary>
        [JsonProperty(PropertyName = "total interest")]
        public decimal TotalInterest { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payment"/> class.
        /// </summary>
        /// /// <param name="monthlyPayment">Payment per month</param>
        /// <param name="totalPayment">Total payment</param>
        /// <param name="totalInterest">Payment over loan</param>
        public Payment(decimal monthlyPayment, decimal totalPayment, decimal totalInterest)
        {
            _monthlyPaymentOrigin = monthlyPayment;
            _totalPaymentOrigin = totalPayment;
            _totalInterestOrigin = totalInterest;

            MonthlyPayment = Decimal.Round(_monthlyPaymentOrigin, RoundingCoef);
            TotalPayment = Decimal.Round(_totalPaymentOrigin, RoundingCoef);
            TotalInterest = Decimal.Round(_totalInterestOrigin, RoundingCoef);
        }
    }
}
