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
		public UInt32 profit { get; private set; }

		public itemToCraftUC()
		{
			profit = new();
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
			labelItemName.Text = Properties.AllItemsREPO.IDtoNAME(itemRecipe.item_dictKey);

			//Set recipe
			textBoxRecipe.Clear();
			foreach (var itemReq in itemRecipe.reqItems)
			{
				textBoxRecipe.AppendText(itemReq.amount.ToString() + " × " + Properties.AllItemsREPO.IDtoNAME(itemReq.item_dictKey) + Environment.NewLine);
			}
		}

		public void refreshData()
		{
			UInt32 baseCost = 1200; //Price to put up an BIN auction
			//Prepare all variables
			decimal sellPrice = 0;
			decimal buyNowPrice;
			decimal buyViaOfferPrice;
			decimal expectedProfit = 0;
			decimal interest;
			while (BazaarCheckup.bazaarObj.success!=true||AuctionHouseInstance.ahCache.success!=true) { return; }

			ItemsToCraft.Source source = BazaarCheckup.bazaarObj.products.ContainsKey(itemRecipe.item_dictKey) == true ? ItemsToCraft.Source.Bazaar : ItemsToCraft.Source.AuctionHouse;
			if (source != ItemsToCraft.Source.AuctionHouse || AuctionHouseInstance.ahCache.items.ContainsKey(itemRecipe.item_dictKey) == true)
			{ //Add something if item is not found HERE
				switch (source)
				{
					case ItemsToCraft.Source.Bazaar:
						sellPrice = BazaarCheckup.bazaarObj.products[itemRecipe.item_dictKey].buy_summary[0].pricePerUnit - 1;
						break;
					case ItemsToCraft.Source.AuctionHouse:
						AuctionHouseInstance.ahCache.items[itemRecipe.item_dictKey].Sort((a, b) => a.starting_bid.CompareTo(b.starting_bid));
						sellPrice = (decimal)AuctionHouseInstance.ahCache.items[itemRecipe.item_dictKey][0].starting_bid - 1;
						break;
				}
			}




			buyNowPrice = 0;
			buyViaOfferPrice = 0;
			foreach (var reqItem in itemRecipe.reqItems)
			{
				var reqItemAmountLeft = reqItem.amount;
				ItemsToCraft.Source reqItemSource = BazaarCheckup.bazaarObj.products.ContainsKey(reqItem.item_dictKey) == true ? ItemsToCraft.Source.Bazaar : ItemsToCraft.Source.AuctionHouse;
				if (reqItemSource == ItemsToCraft.Source.AuctionHouse && AuctionHouseInstance.ahCache.items.ContainsKey(reqItem.item_dictKey) == false) return; //Add something if item is not found HERE
				switch (reqItemSource)
				{
					case ItemsToCraft.Source.Bazaar:
						var bzOrders = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].buy_summary; //BZ list regarding this specific item
						var bzOrdersOffer = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].sell_summary;
						for (int i = 0; reqItemAmountLeft > 0; i++)
						{
							if (i == bzOrders.Count) { buyNowPrice = 0; break; }
							buyNowPrice += bzOrders[i].pricePerUnit * (bzOrders[i].amount >= reqItemAmountLeft ? reqItemAmountLeft : bzOrders[i].amount);
							reqItemAmountLeft = bzOrders[i].amount >= reqItemAmountLeft ? 0 : reqItemAmountLeft - bzOrders[i].amount;
						}
						buyViaOfferPrice += (decimal.Add(bzOrdersOffer[0].pricePerUnit, (decimal)0.1)) * reqItem.amount;
						break;
					case ItemsToCraft.Source.AuctionHouse:
						var ahOrders = AuctionHouseInstance.ahCache.items[reqItem.item_dictKey]; //BZ list regarding this specific item
						ahOrders.Sort((a, b) => a.starting_bid.CompareTo(b.starting_bid));
						for (int i = 0; reqItemAmountLeft > 0; i++)
						{
							if (i == ahOrders.Count) { buyNowPrice = 0; break; }
							buyNowPrice += ahOrders[i].starting_bid;
							buyViaOfferPrice += ahOrders[0].starting_bid;
							reqItemAmountLeft--;
						}
						break;
				}
			}


			//Print gathered data to UserControl

			if (buyNowPrice != 0)
			{
				textBoxBINprice.Clear();
				textBoxBINprice.Text = (buyNowPrice).ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
			}
			else
			{
				textBoxBINprice.Clear();
				textBoxBINprice.Text = "NaN";
			}


			if (buyViaOfferPrice != 0)
			{
				textBoxOfferBUY.Clear();
				textBoxOfferBUY.Text = (buyViaOfferPrice).ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
			}
			else
			{
				textBoxOfferBUY.Clear();
				textBoxOfferBUY.Text = "NaN";
			}


			if (sellPrice != 0)
			{
				textBoxsellPrice.Clear();
				textBoxsellPrice.Text = sellPrice.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
			}
			else
			{
				textBoxsellPrice.Clear();
				textBoxsellPrice.Text = "NaN";
			}

			//Profit and interest calculation
			if ((sellPrice != 0&&buyNowPrice !=0)&&(sellPrice>=buyNowPrice))
			{
				expectedProfit = (sellPrice * (decimal)0.99) - baseCost - buyNowPrice; //Calculation for AH only for now
				interest = expectedProfit / buyNowPrice;
				textBoxProfit.Clear();
				textBoxProfit.Text = expectedProfit.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
				textBoxInterest.Clear();
				textBoxInterest.Text = interest.ToString("P");
			}
			else if (sellPrice != 0 && buyViaOfferPrice != 0)
			{
				expectedProfit = (sellPrice * (decimal)0.99) - baseCost - buyViaOfferPrice; //Calculation for AH only for now
				interest = expectedProfit / buyViaOfferPrice;
				textBoxProfit.Clear();
				textBoxProfit.Text = expectedProfit.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA")) + " (O)";
				textBoxInterest.Clear();
				textBoxInterest.Text = interest.ToString("P");
			}
			else
			{
				textBoxProfit.Clear();
				textBoxProfit.Text = "NaN";
				textBoxInterest.Clear();
				textBoxInterest.Text = "NaN";
			}
			if (expectedProfit > 0) profit = (UInt32)expectedProfit;
			else profit = 0;
		}//ENDOF refreshF





	}
}
