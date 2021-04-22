using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPatternHandsOn
{
    public enum Manufacturers
    {
        Audi,Mercedes
    }

    abstract class Tire
    {
        public abstract int TireSize { get; }
        public abstract double TirePrice { get; }
    }

    abstract class HeadLight
    {
        public abstract int Brightness { get; }
        public abstract double HeadLightPrice { get; }
    }

    abstract class Factory
    {
        public abstract Tire MakeTire();
        public abstract HeadLight MakeHeadLight();
    }


    class AudiTire : Tire
    {
        public override int TireSize { get => 15; }
        public override double TirePrice { get => 600; }
    }

    class MercedesTire : Tire
    {
        public override int TireSize { get => 17; }
        public override double TirePrice { get => 800; }
    }

    class AudiHeadLight : HeadLight
    {
        public override int Brightness { get => 50; }
        public override double HeadLightPrice { get => 500; }
    }

    class MercedesHeadLight : HeadLight
    {
        public override int Brightness { get => 48; }
        public override double HeadLightPrice { get => 400; }
    }

    class AudiFactory : Factory
    {
        public override HeadLight MakeHeadLight()
        {
            HeadLight headLight = new AudiHeadLight();
            return headLight;
        }

        public override Tire MakeTire()
        {
            Tire tire = new AudiTire();
            return tire;
        }
    }

    class MercedesFactory : Factory
    {
        public override HeadLight MakeHeadLight()
        {
            HeadLight headLight = new MercedesHeadLight();
            return headLight;
        }

        public override Tire MakeTire()
        {
            Tire tire = new MercedesTire();
            return tire;
        }
    }

    class AbstractFactoryPattern
    {
        static void Main(string[] args)
        {
            Factory factory = null;
            HeadLight headLight = null;
            Tire tire = null;

            Console.WriteLine("Choose the Manufacturer : ");
            int count = 0;
            foreach(var item in Enum.GetValues(typeof(Manufacturers)).Cast<Manufacturers>().ToList())
            {
                count++;
                Console.WriteLine(count+". "+item);
            }
            string manufacturer = Console.ReadLine();
            if (manufacturer == Manufacturers.Audi.ToString())
            {
                factory = new AudiFactory();

                headLight=factory.MakeHeadLight();
                Console.WriteLine("You have ordered the HeadLight from "+manufacturer+" with Brightness Level - "+headLight.Brightness+" and Price - "+headLight.HeadLightPrice);
                tire=factory.MakeTire();
                Console.WriteLine("You have ordered the Tire from " + manufacturer + " with Tire Size - " + tire.TireSize + " and Price - " + tire.TirePrice);
            }
            else if (manufacturer == Manufacturers.Mercedes.ToString())
            {
                factory = new MercedesFactory();

                headLight=factory.MakeHeadLight();
                Console.WriteLine("You have ordered the HeadLight from " + manufacturer + " with Brightness Level - " + headLight.Brightness + " and Price - " + headLight.HeadLightPrice);
                tire =factory.MakeTire();
                Console.WriteLine("You have ordered the Tire from " + manufacturer + " with Tire Size - " + tire.TireSize + " and Price - " + tire.TirePrice);
            }
            else
            {
                Console.WriteLine("Invalid Manufacturer...");
            }
            Console.ReadKey();
        }
    }
}
