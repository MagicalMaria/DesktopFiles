using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPrinciplesAndPatternsPracticeCheck
{
	#region Car Related - Product and its parent abstract class

	public enum CarType
	{
		MICRO, MINI, LUXURY
	}

	public enum Location
	{
		DEFAULT, USA, INDIA
	}

	abstract class Car
	{
		public Car(CarType model, Location location)
		{
			this.carType = model;
			this.location = location;
		}

		public abstract void Construct();

		public CarType carType { get; set; }
		public Location location { get; set; }

		public override string ToString()
		{
			return "CarModel - " + carType.ToString() + " located in " + location.ToString();
		}
	}

	class LuxuryCar : Car
	{
		public LuxuryCar(CarType carType, Location location)
			: base(CarType.LUXURY, location)
		{
			Construct();
		}

		public override void Construct()
		{
			Console.WriteLine("Connecting to luxury car");
			Console.WriteLine(base.ToString());
		}
	}

	class MicroCar : Car
	{
		public MicroCar(CarType carType, Location location)
			: base(CarType.MICRO, location)
		{
			Construct();
		}

		public override void Construct()
		{
			Console.WriteLine("Connecting to Micro car");
			Console.WriteLine(base.ToString());
		}
	}

	class MiniCar : Car
	{
		public MiniCar(CarType carType, Location location)
			: base(CarType.MINI, location)
		{
			Construct();
		}

		public override void Construct()
		{
			Console.WriteLine("Connecting to Mini car");
			Console.WriteLine(base.ToString());
		}
	}

	#endregion
	public class AbstractFactoryPattern
	{
		public static void Main(string[] args)
		{
			CarFactory carFactory = new ConcreteCarFactory();
			CarClient carClient = new CarClient(carFactory);
			carClient.BuildMicroCar(Location.USA);
			carClient.BuildMiniCar(Location.INDIA);
			carClient.BuildLuxuryCar(Location.DEFAULT);
			Console.ReadKey();
		}
	}


	#region Abstract factory to expose to client

	abstract class CarFactory
	{
		public abstract void BuildMicroCar(Location location);
		public abstract void BuildMiniCar(Location location);
		public abstract void BuildLuxuryCar(Location location);
	}

	class ConcreteCarFactory : CarFactory
	{
		Car car = null;
		public override void BuildMicroCar(Location location)
		{
			car = new MicroCar(CarType.MICRO, location);
			car.ToString();
		}
		public override void BuildMiniCar(Location location)
		{
			car = new MiniCar(CarType.MINI, location);
			car.ToString();
		}
		public override void BuildLuxuryCar(Location location)
		{
			car = new LuxuryCar(CarType.LUXURY, location);
			car.ToString();
		}
	}

	class CarClient : ConcreteCarFactory
	{
		CarFactory carFactory = null;
		public CarClient(CarFactory carFactory)
		{
			this.carFactory = carFactory;
		}

		public override void BuildMicroCar(Location location)
		{
			carFactory.BuildMicroCar(location);
		}
		public override void BuildMiniCar(Location location)
		{
			carFactory.BuildMiniCar(location);
		}
		public override void BuildLuxuryCar(Location location)
		{
			carFactory.BuildLuxuryCar(location);
		}
	}

	#endregion
}
