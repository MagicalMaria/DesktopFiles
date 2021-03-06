using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPrinciplesAndPatternsFinalCheck
{
	interface Event
	{
		string EventID { get; set; }
		string EventOrganizer { get; set; }
		string EventName { get; set; }
		DateTime EventDate { get; set; }
		int TotalNoOfTickets { get; set; }
		int TicketsBooked { get; set; }
	}

	class EventCreation : Event
	{
		public EventCreation(string eventID, string eventOrganizer, string eventName, DateTime eventDate, int totalNoOfTickets, int ticketsBooked)
		{
			EventID = eventID;
			EventOrganizer = eventOrganizer;
			EventName = eventName;
			EventDate = eventDate;
			TotalNoOfTickets = totalNoOfTickets;
			TicketsBooked = ticketsBooked;
		}
		public string EventID { get; set; }
		public string EventOrganizer { get; set; }
		public string EventName { get; set; }
		public DateTime EventDate { get; set; }
		public int TotalNoOfTickets { get; set; }
		public int TicketsBooked { get; set; }
	}

	interface INotificationService
	{
		void GetEvents();
		void BookTicket(string EventID, int ticketsCount);
		void OnBookingExceeds100(string EventID);
	}

	public class NotifictaionService : INotificationService
	{
		List<Event> eventList = new List<Event>()
		{
				new EventCreation("ID001","Ragu","Russian Culturals",DateTime.ParseExact("20/05/2021","dd/mm/yyyy",null),500,99),
				new EventCreation("ID002","John","Tech Collaboration",DateTime.ParseExact("18/07/2021","dd/mm/yyyy",null),500,70),
				new EventCreation("ID003","Tamil","Asian Magics",DateTime.ParseExact("08/08/2021","dd/mm/yyyy",null),500,99),
				new EventCreation("ID004","Josh","Fun Zone",DateTime.ParseExact("25/06/2021","dd/mm/yyyy",null),500,50),
				new EventCreation("ID005","Mosh","Trends in IT",DateTime.ParseExact("16/04/2021","dd/mm/yyyy",null),500,99)
		};

		public void GetEvents()
		{
			Console.WriteLine("Event ID               Event Organizer                 Event Name                    Date                 Available Tickets");
			Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
			foreach (var evnt in eventList)
			{
				Console.WriteLine(evnt.EventID + "                     " + evnt.EventOrganizer + "                 " + evnt.EventName + "                   " + evnt.EventDate + "               " + (evnt.TotalNoOfTickets - evnt.TicketsBooked));
			}
		}

		public void BookTicket(string EventID, int ticketsCount)
		{
			foreach (var evnt in eventList)
			{
				if (evnt.EventID.Equals(EventID))
				{
					evnt.TicketsBooked += ticketsCount;
					Console.WriteLine("You ticket has been confirmed.....");
					if (evnt.TicketsBooked > 100)
						OnBookingExceeds100(EventID);
				}
			}
		}

		public void OnBookingExceeds100(string EventID)
		{
			foreach (var evnt in eventList)
			{
				if (evnt.EventID.Equals(EventID))
					Console.WriteLine("Hello " + evnt.EventOrganizer + "! Booking count exceeds 100 for the event " + evnt.EventName + "......");
			}
		}
	}
	class ObserverPattern
    {
		public static void Main(string[] args)
		{
			INotificationService service = new NotifictaionService();
			service.GetEvents();
			Console.WriteLine("Enter the Event ID that you want to book");
			string evntID = Console.ReadLine();
			Console.WriteLine("Enter the number of tickets");
			int ticketsCount = int.Parse(Console.ReadLine());
			service.BookTicket(evntID, ticketsCount);
		}
	}
}
