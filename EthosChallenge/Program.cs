using System;
using System.Globalization;
using EthosChallenge.Models;
using Newtonsoft.Json;

namespace EthosChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            InputRequest userData = null;
            try
            {
                 userData = InputRequestFabric.Parse(args, CultureInfo.CurrentCulture);
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
