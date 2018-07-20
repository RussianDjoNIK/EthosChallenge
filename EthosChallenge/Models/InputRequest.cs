namespace EthosChallenge.Models
{
    /// <summary>
    /// Class with input request for calculationg
    /// </summary>
    public class InputRequest
    {
        /// <summary>
        /// Total amount of requested mortage loan
        /// </summary>
        [Option("amount", new[] {' '})]
        public decimal Amount { get; set; }

        /// <summary>
        /// Annual interest rate
        /// </summary>
        [Option("interest", new[] { ' ', '%' })]
        public decimal Interest { get; set; }

        /// <summary>
        /// Downpayment of requested mortage loan
        /// </summary>
        [Option("downpayment", new[] { ' ' })]
        public decimal Downpayment { get; set; }

        /// <summary>
        /// The term in years
        /// </summary>
        [Option("term", new[] { ' ' })]
        public decimal Term { get; set; }
    }
}
