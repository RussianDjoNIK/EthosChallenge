using System;
using System.Globalization;
using EthosChallenge.Models;
using Newtonsoft.Json;

namespace EthosChallenge
{
    class Program
    {
        private static readonly CultureInfo FrCulture = new CultureInfo("fr-FR");
        private static readonly CultureInfo UsCulture = new CultureInfo("en-US");

        static void Main(string[] args)
        {
            InputRequest userData = null;
            try
            {
                 userData = InputRequestFabric.Parse(args, UsCulture);
            }
            catch
            {
                // TODO log exception
                Console.WriteLine("Oops, something's happened. Most probably you've chose wrong Culture.");
            }


            if (userData == null)
                return;

            var res = Calculator.Process(userData);
            var json = JsonConvert.SerializeObject(res);
            Console.WriteLine("Success:");
            Console.WriteLine(json);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
