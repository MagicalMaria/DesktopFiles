using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPrinciplesAndPatternsFinalCheck
{
	public enum ProductType
	{
		Electronics, Toys, Furnitures
	}

	public enum ChannelType
	{
		Website, Telecaller
	}

	abstract class Order
	{
		public ProductType productType;
		public ChannelType channelType;
		public abstract void ProcessOrder();
	}

	class ElectronicOrder : Order
	{
		public ElectronicOrder(ProductType productType, ChannelType channelType)
		{
			this.productType = productType;
			this.channelType = channelType;
		}

		public override void ProcessOrder()
		{
			Console.WriteLine(productType + " product is ordered through the " + channelType + " channel.");
		}
	}

	class FurnitureOrder : Order
	{
		public FurnitureOrder(ProductType productType, ChannelType channelType)
		{
			this.productType = productType;
			this.channelType = channelType;
		}

		public override void ProcessOrder()
		{
			Console.WriteLine(productType + " product is ordered through the " + channelType + " channel.");
		}
	}

	class ToysOrder : Order
	{
		public ToysOrder(ProductType productType, ChannelType channelType)
		{
			this.productType = productType;
			this.channelType = channelType;
		}

		public override void ProcessOrder()
		{
			Console.WriteLine(productType + " product is ordered through the " + channelType + " channel.");
		}
	}

	class PlaceOrder
	{
		Order order = null;
		public void OrderElectronicProduct(ChannelType channel)
		{
			order = new ElectronicOrder(ProductType.Electronics, channel);
			order.ProcessOrder();
		}
		public void OrderToyProduct(ChannelType channel)
		{
			order = new ToysOrder(ProductType.Toys, channel);
			order.ProcessOrder();
		}
		public void OrderFurnitureProduct(ChannelType channel)
		{
			order = new FurnitureOrder(ProductType.Furnitures, channel);
			order.ProcessOrder();
		}
	}
	class AbstractFactoryPattern
    {
        static void Main(string[] args)
        {
			PlaceOrder placeOrder = new PlaceOrder();
			placeOrder.OrderElectronicProduct(ChannelType.Telecaller);
			placeOrder.OrderToyProduct(ChannelType.Website);
			placeOrder.OrderFurnitureProduct(ChannelType.Telecaller);
		}
    }
}
