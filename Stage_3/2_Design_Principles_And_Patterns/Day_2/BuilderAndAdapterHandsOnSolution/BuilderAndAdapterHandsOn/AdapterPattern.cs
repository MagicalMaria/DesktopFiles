using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderAndAdapterHandsOn
{
    interface Currency
    {
        double getPrice();
    }

    class AmericanCar : Currency
    {
        public double getPrice()
        {
            return 20;
        }
    }

    interface CurrencyAdapter
    {
        double getPrice();
    }

    class EuropeanCar : CurrencyAdapter
    {
        private Currency currency;

        public EuropeanCar(Currency cur)
        {
            currency = cur;
        }

        public double getPrice()
        {
            return getPriceInEuros(currency.getPrice());
        }

        private double getPriceInEuros(double americanDollars)
        {
            return americanDollars * 0.84;
        }
    }

    class AdapterPattern
    {
        public static void Main(string[] args)
        {
            Currency currency = new AmericanCar();
            CurrencyAdapter currencyAdapter = new EuropeanCar(currency);
            Console.WriteLine(currencyAdapter.getPrice());
            Console.ReadKey();
        }
    }
}
