using System;

namespace EthosChallenge
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        public string Name { get; }
        public char[] OptionalSymbols { get; }

        public OptionAttribute(string name, char[] optionalSymbols)
        {
            Name = name;
            OptionalSymbols = optionalSymbols;
        }
    }
}
