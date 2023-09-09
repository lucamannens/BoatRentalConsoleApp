using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BoatRentalApp.Transactions {
    class CurrencyConverter {

        private static Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal> {

            {"USD", 1.0m },
            {"EUR", 0.85m },
            {"GBP", 0.73m },
        };

        public static string ChooseCurrency() {

            Console.WriteLine("Select your preferred currency: ");
            Console.WriteLine("1. USD");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. GBP");
            Console.Write("Enter your choice: ");
            string currencyChoice = Console.ReadLine();

            string selectedCurrency = "USD"; //Default
            switch (currencyChoice) {

                case "2":
                    selectedCurrency = "EUR";
                    break;
                case "3":
                    selectedCurrency = "GBP";
                    break;
            }
            return selectedCurrency;
        }

        public static string ConvertAndFormatCurrency(decimal amount, string fromCurrency, string toCurrency, Inventory inventory) {

            // Convert the amount from the source currency to the target currency
            decimal convertedAmount = ConvertAmount(amount, fromCurrency, toCurrency);

            // Format the converted amount in the target currency
            string formattedPrice = FormatCurrency(convertedAmount, toCurrency);

            return formattedPrice;
        }

        //This method converts the one currency to another currency based on the exchange rate.
        public static decimal ConvertAmount(decimal amount, string fromCurrency, string toCurrency) {

            decimal exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
            return amount * exchangeRate;
        }

        //This method retrieves the exchange rate between two currencies.
        public static decimal GetExchangeRate(string fromCurrency, string toCurrency) {

            if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates.ContainsKey(toCurrency)) {

                return exchangeRates[toCurrency] / exchangeRates[fromCurrency];
            }

            //Handle unsupported currencies or errors
            throw new ArgumentException("Invalid currencies.");
        }

        public static string FormatCurrency(decimal amount, string currencyCode) {
            // Implement currency formatting based on the currency symbol and formatting rules.
            // Example: "$1,234.56" for USD, "€1.234,56" for EUR, etc.
            // You can use CultureInfo for more advanced formatting.
            return $"{currencyCode} {amount:N2}";
        }

        public static List<string> ListSupportedCurrencies() {

            return new List<string>(exchangeRates.Keys);
        }
    }
}
