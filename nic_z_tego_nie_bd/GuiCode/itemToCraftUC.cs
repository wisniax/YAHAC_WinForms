using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class itemToCraftUC : UserControl
	{
		ItemsToCraft.ItemRecipe itemRecipe;
		public itemToCraftUC()
		{
			InitializeComponent();

		}
		public void initialize(ItemsToCraft.ItemRecipe item)
		{
			itemRecipe = item;
			ParseStaticData();
			refreshData();
		}

		private void ParseStaticData()
		{
			//Set tiltle
			labelItemName.Text = itemRecipe.item_name;

			//Set recipe
			textBoxRecipe.Clear();
			foreach (var itemReq in itemRecipe.reqItems)
			{
				textBoxRecipe.Text += itemReq.amount.ToString() + " × " + itemReq.item_name + '\n';
			}
		}

		public void refreshData()
		{
			decimal baseCost = 1200; //Price to put up an BIN auction

			//Prepare all variables
			decimal sellPrice = 0;
			decimal buyNowPrice;
			decimal buyViaOfferPrice;
			decimal expectedProfit;
			decimal interest;

			//Item sell price
			try
			{
				switch (itemRecipe.sellTo)
				{
					case ItemsToCraft.Source.Bazaar:
						sellPrice = BazaarCheckup.bazaarObj.products[itemRecipe.selldictKey].buy_summary[0].pricePerUnit - 1;
						break;
					case ItemsToCraft.Source.AuctionHouse:
						sellPrice = (decimal)AuctionHouseInstance.ahCache.items[itemRecipe.selldictKey][0].starting_bid - 1;
						break;
				}
			}
			catch { return; } //Better error handling SOON TM :)


			//Buy now and via offer price
			try
			{
				buyNowPrice = 0;
				buyViaOfferPrice = 0;
				foreach (var reqItem in itemRecipe.reqItems)
				{
					var reqItemAmountLeft = reqItem.amount;
					switch (reqItem.source)
					{
						case ItemsToCraft.Source.Bazaar:
							var bzOrders = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].buy_summary; //BZ list regarding this specific item
							var bzOrdersOffer = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].sell_summary;
							for (int i = 0; reqItemAmountLeft > 0; i++)
							{
								buyNowPrice += bzOrders[i].pricePerUnit * (bzOrders[i].amount >= reqItemAmountLeft ? reqItemAmountLeft : bzOrders[i].amount);
								reqItemAmountLeft = bzOrders[i].amount >= reqItemAmountLeft ? 0 : reqItemAmountLeft - bzOrders[i].amount;
							}
							buyViaOfferPrice += (decimal.Add(bzOrdersOffer[0].pricePerUnit, (decimal)0.1)) * reqItem.amount;
							break;
						case ItemsToCraft.Source.AuctionHouse:
							var ahOrders = AuctionHouseInstance.ahCache.items[reqItem.item_dictKey]; //BZ list regarding this specific item
							for (int i = 0; reqItemAmountLeft > 0; i++)
							{
								buyNowPrice += ahOrders[i].starting_bid;
								buyViaOfferPrice += ahOrders[i].starting_bid;
								reqItemAmountLeft--;
							}
							break;
					}
				}
			}
			catch { return; }


			//Profit and interest calculation
			expectedProfit = (sellPrice * (decimal)0.99) - baseCost - buyNowPrice; //Calculation for AH only for now
			interest = 100*expectedProfit / buyNowPrice;


			//Print gathered data to UserControl
			textBoxBINprice.Clear();
			textBoxBINprice.Text = (buyNowPrice).ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
			textBoxOfferBUY.Clear();
			textBoxOfferBUY.Text = (buyViaOfferPrice).ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
			textBoxsellPrice.Clear();
			textBoxsellPrice.Text = sellPrice.ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
			textBoxProfit.Clear();
			textBoxProfit.Text = expectedProfit.ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
			textBoxInterest.Clear();
			textBoxInterest.Text = interest.ToString("P");

		}//ENDOF refreshF





	}
}
