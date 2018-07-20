using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using EthosChallenge.Models;

namespace EthosChallenge
{
    public static class InputRequestFabric
    {
        /// <summary>
        /// Generate valid <see cref="InputRequest"/> object, based on free text
        /// </summary>
        /// <param name="input">Command line arguments</param>
        /// <param name="culture">Culture info</param>
        /// <returns>Object of <see cref="InputRequest"/> class</returns>
        public static InputRequest Parse(string[] input, CultureInfo culture)
        {
            var props = GetOptionProperties<InputRequest>();

            #region Input Values Guards

            var minParametersCount = props.Count * 2;
            if (input.Length < minParametersCount)
            {
                throw new ArgumentException($"Input have to have at least {minParametersCount} parameters");
            }

            #endregion

            Debug.WriteLine($"Culture is {culture}");

            var result = new InputRequest();

            var inputParamsLen = input.Length;
            var optionalSymbols = new char[4];
            var isNewProperty = false;
            PropertyInfo property = null;
            for (var i = 0; i < inputParamsLen; i++)
            {
                var currentArgValue = input[i];
                object parsedValue = null;

                // Loop for evaluating value of new property
                while (isNewProperty)
                {
                    currentArgValue = input[i];
                    var argValue = currentArgValue.Trim(optionalSymbols);
                    // escape optional symbols in the middle of the value
                    foreach (var optionalSymbol in optionalSymbols)
                    {
                        argValue = argValue.Replace(optionalSymbol.ToString(), "");
                    }

                    try
                    {   
                        parsedValue = Convert.ChangeType(argValue, property.PropertyType, culture);
                        Debug.WriteLine($"For {property.Name} parsed value is {parsedValue}");
                        currentArgValue = argValue;
                        i++;
                    }
                    catch
                    {
                        Debug.WriteLine($"Failed {argValue}");
                        isNewProperty = false;
                    }

                    if (i >= inputParamsLen - 1)
                    {
                        break;
                    }
                }

                if (property != null)
                {
                    property.SetValue(result, Convert.ChangeType(parsedValue, property.PropertyType), null);
                }

                // Loop for matching option keys
                for (var j = props.Count - 1; j >= 0; j--)
                {
                    var propAttrName = props[j].Item1.Name;
                    optionalSymbols = props[j].Item1.OptionalSymbols;
                    property = props[j].Item2;

                    if (!currentArgValue.StartsWith(propAttrName, true, culture))
                    {
                        Debug.WriteLine($"For '{currentArgValue}' was skipped key '{propAttrName}'");
                        continue;
                    }

                    isNewProperty = true;
                    Debug.WriteLine($"Removing '{propAttrName}'");
                    props.RemoveAt(j);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Utilize method for generating <see cref="ValueTuple{OptionAttribute, PropertyInfo}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Tuple <see cref="ValueTuple{OptionAttribute, PropertyInfo}"/> with option parameters and prop info</returns>
        private static List<ValueTuple<OptionAttribute, PropertyInfo>> GetOptionProperties<T>() where T: class
        {
            return typeof(T).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(OptionAttribute)))
                .Select(a =>
                {
                    var option = a.GetCustomAttributes(false).OfType<OptionAttribute>().First();
                    return (option, a);
                })
                .ToList();
        }
    }
}
