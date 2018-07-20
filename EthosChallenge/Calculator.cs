using System;
using EthosChallenge.Models;

namespace EthosChallenge
{
    public static class Calculator
    {
        /// <summary>
        /// Count of months per year
        /// </summary>
        private const decimal MonthCount = 12m; // can be int, but I want to decrease count of type conversions

        /// <summary>
        /// Do calculation, based on input data
        /// </summary>
        /// <param name="input">Prepared input data of <see cref="InputRequest"/> class</param>
        /// <returns>Object of a <see cref="Payment"/> class</returns>
        public static Payment Process(InputRequest input)
        {
            #region Input Values Guards

            // TODO check Interest input. In case of it can be less than 0% and more than 100%

            if (input.Amount <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(input), $"Amount ({input.Amount}) have to be greater than 0");
            }

            if (input.Term <= 0m || input.Term > 90m)
            {
                throw new ArgumentOutOfRangeException(nameof(input), $"Term in years ({input.Term}) have to be greater than 0 and less than 90");
            }

            if (input.Downpayment >= input.Amount || input.Downpayment < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(input), $"Downpayment ({input.Downpayment}) have to be less than Amount ({input.Amount}) and greater than 0");
            }

            #endregion

            var loanSumm = input.Amount - input.Downpayment;
            var monthInterestRate = input.Interest / MonthCount / 100m;
            var paymentsCount = input.Term * MonthCount;

            // TODO probably, try to use some math library
            var coef = (decimal)Math.Pow((double)(1m + monthInterestRate), (double) paymentsCount);
            var annuityPaymentCoeff = monthInterestRate * coef / (coef - 1m);

            var monthlyPayment = annuityPaymentCoeff * loanSumm;
            var totalPayment = monthlyPayment * paymentsCount;

            var res = new Payment(monthlyPayment, totalPayment, totalPayment - loanSumm);
            return res;
        }
    }
}
