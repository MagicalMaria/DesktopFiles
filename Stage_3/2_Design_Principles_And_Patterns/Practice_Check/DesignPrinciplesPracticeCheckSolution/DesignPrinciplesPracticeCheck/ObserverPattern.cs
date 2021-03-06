using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPrinciplesPracticeCheck
{
	interface INotificationObserver
	{
		string Name { get; set; }
		void OnServerDown();
	}

	class SteveObserver : INotificationObserver
	{
		public string Name { get; set; }
		public void OnServerDown()
		{
			Console.WriteLine("Pay attention " + Name + "! Server is down.....");
		}
	}

	class JohnObserver : INotificationObserver
	{
		public string Name { get; set; }
		public void OnServerDown()
		{
			Console.WriteLine("Pay attention " + Name + "! Server is down.....");
		}
	}

	interface INotificationService
	{
		void AddSubscriber(INotificationObserver observer);
		void RemoveSubscriber(INotificationObserver observer);
		void NotifySubscriber();
	}

	class NotificationService : INotificationService
	{
		List<INotificationObserver> objects = new List<INotificationObserver>();

		public void AddSubscriber(INotificationObserver observer)
		{
			objects.Add(observer);
			foreach (var item in objects)
			{
				Console.WriteLine(item.Name);
			}
		}

		public void RemoveSubscriber(INotificationObserver observer)
		{
			objects.Remove(observer);
			foreach (var item in objects)
			{
				Console.WriteLine(item.Name);
			}
		}

		public void NotifySubscriber()
		{
			foreach (var item in objects)
			{
				item.OnServerDown();
			}
		}
	}
	class ObserverPattern
    {
		public static void Main(string[] args)
		{
			INotificationObserver steve = new SteveObserver();
			steve.Name = "Steve";
			INotificationObserver john = new JohnObserver();
			john.Name = "John";
			INotificationService service = new NotificationService();
			service.AddSubscriber(steve);
			service.AddSubscriber(john);
			service.NotifySubscriber();
			service.RemoveSubscriber(john);
			service.NotifySubscriber();
		}
	}
}
