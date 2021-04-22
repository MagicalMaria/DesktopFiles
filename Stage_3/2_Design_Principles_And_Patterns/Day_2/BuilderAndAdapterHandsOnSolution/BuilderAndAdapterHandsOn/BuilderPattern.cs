using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderAndAdapterHandsOn
{
    interface Builder
    {
        void BuildPackage();
    }

    class ChildPackageBuilder : Builder
    {
        public void BuildPackage()
        {
            Console.WriteLine("Child Package cosists of 2 Sweets, 1 Savory......");
        }
    }

    class AdultPackageBuilder : Builder
    {
        public void BuildPackage()
        {
            Console.WriteLine("Adult Package cosists of 2 Sweets, 2 Savory......");
        }
    }

    class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPackage();
        }
    }

    class BuilderPattern
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            Builder childPackage = new ChildPackageBuilder();
            Builder adultPackage = new AdultPackageBuilder();

            director.Construct(childPackage);
            director.Construct(adultPackage);

            Console.ReadKey();
        }
    }
}
