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
		public UInt64 profit { get; private set; }

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
				textBoxRecipe.AppendText((itemReq.amount * numericUpDownMultipl.Value).ToString() + " × " + Properties.AllItemsREPO.IDtoNAME(itemReq.item_dictKey));
				if (itemReq.item_dictKey != itemRecipe.reqItems.Last().item_dictKey) { textBoxRecipe.AppendText(Environment.NewLine); };
			}
		}

		public void refreshData()
		{
			UInt32 baseCost = 1200; //Price to put up an BIN auction
									//Prepare all variables
			decimal sellPrice = 0;
			UInt64 buyNowPrice = 0;
			UInt64 buyViaOfferPrice = 0;
			decimal expectedProfit = 0;
			decimal interest = 0;
			while (BazaarCheckup.bazaarObj.success != true || MainGui.AHInstance.ahCache.success != true) { return; }

			ItemsToCraft.Source source = BazaarCheckup.bazaarObj.products.ContainsKey(itemRecipe.item_dictKey) == true ? ItemsToCraft.Source.Bazaar : ItemsToCraft.Source.AuctionHouse;
			if (source != ItemsToCraft.Source.AuctionHouse || MainGui.AHInstance.ahCache.items.ContainsKey(itemRecipe.item_dictKey) == true)
			{ //Add something if item is not found HERE
				switch (source)
				{
					case ItemsToCraft.Source.Bazaar:
						sellPrice = (BazaarCheckup.bazaarObj.products[itemRecipe.item_dictKey].buy_summary[0].pricePerUnit - 0.1M) * numericUpDownMultipl.Value;
						break;
					case ItemsToCraft.Source.AuctionHouse:
						MainGui.AHInstance.ahCache.items[itemRecipe.item_dictKey].Sort((a, b) => a.starting_bid.CompareTo(b.starting_bid));
						sellPrice = ((decimal)MainGui.AHInstance.ahCache.items[itemRecipe.item_dictKey][0].starting_bid - 1) * numericUpDownMultipl.Value;
						break;
				}
			}




			buyNowPrice = 0;
			buyViaOfferPrice = 0;
			bool buyNowPriceOverflowed = false;
			foreach (var reqItem in itemRecipe.reqItems)
			{
				var reqItemAmountLeft = reqItem.amount * numericUpDownMultipl.Value;
				ItemsToCraft.Source reqItemSource = BazaarCheckup.bazaarObj.products.ContainsKey(reqItem.item_dictKey) == true ? ItemsToCraft.Source.Bazaar : ItemsToCraft.Source.AuctionHouse;
				if (reqItemSource == ItemsToCraft.Source.AuctionHouse && MainGui.AHInstance.ahCache.items.ContainsKey(reqItem.item_dictKey) == false) return; //Add something if item is not found HERE
				switch (reqItemSource)
				{
					case ItemsToCraft.Source.Bazaar:
						var bzOrders = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].buy_summary; //BZ list regarding this specific item
						var bzOrdersOffer = BazaarCheckup.bazaarObj.products[reqItem.item_dictKey].sell_summary;
						for (int i = 0; reqItemAmountLeft > 0; i++)
						{
							if (i == bzOrders.Count) { buyNowPriceOverflowed = true; break; }
							buyNowPrice += (UInt64)(bzOrders[i].pricePerUnit * (bzOrders[i].amount >= reqItemAmountLeft ? reqItemAmountLeft : bzOrders[i].amount));
							reqItemAmountLeft = bzOrders[i].amount >= reqItemAmountLeft ? 0 : reqItemAmountLeft - bzOrders[i].amount;
						}
						if (bzOrdersOffer.Count() != 0) { buyViaOfferPrice += ((UInt64)(bzOrdersOffer[0].pricePerUnit + 0.1M) * (reqItem.amount * (uint)numericUpDownMultipl.Value)); }
						else { buyViaOfferPrice += ((UInt64)(bzOrders[0].pricePerUnit - 0.1M) * (reqItem.amount * (uint)numericUpDownMultipl.Value)); }

						break;
					case ItemsToCraft.Source.AuctionHouse:
						var ahOrders = MainGui.AHInstance.ahCache.items[reqItem.item_dictKey]; //BZ list regarding this specific item
						ahOrders.Sort((a, b) => a.starting_bid.CompareTo(b.starting_bid));
						for (int i = 0; reqItemAmountLeft > 0; i++)
						{
							if (i == ahOrders.Count) { buyNowPriceOverflowed = true; break; }
							buyNowPrice += ahOrders[i].starting_bid;
							buyViaOfferPrice += ahOrders[0].starting_bid;
							reqItemAmountLeft--;
						}
						break;
				}
			}


			//Print gathered data to UserControl

			if (!buyNowPriceOverflowed)
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
			if ((sellPrice != 0 && !buyNowPriceOverflowed) && (sellPrice >= buyNowPrice) && !checkBoxUsingOffer.Checked)
			{
				expectedProfit = (((decimal)(sellPrice / numericUpDownMultipl.Value) * 0.99M) - baseCost - (decimal)(buyNowPrice / numericUpDownMultipl.Value)) * numericUpDownMultipl.Value; //Calculation for AH only for now
				interest = expectedProfit / buyNowPrice;
				textBoxProfit.Clear();
				textBoxProfit.Text = expectedProfit.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
				textBoxInterest.Clear();
				textBoxInterest.Text = interest.ToString("P");
			}
			else if (sellPrice != 0 && buyViaOfferPrice != 0)
			{
				if (!checkBoxUsingOffer.Checked) checkBoxUsingOffer.Checked = true;
				checkBoxUsingOffer.Checked = true;
				expectedProfit = (((decimal)(sellPrice / numericUpDownMultipl.Value) * 0.99M) - baseCost - (decimal)(buyViaOfferPrice / numericUpDownMultipl.Value)) * numericUpDownMultipl.Value; //Calculation for AH only for now
				interest = expectedProfit / buyViaOfferPrice;
				textBoxProfit.Clear();
				textBoxProfit.Text = expectedProfit.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA"));
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
			if (expectedProfit > 0 && interest > 0) profit = (UInt64)(expectedProfit * interest);
			else profit = 0;
		}//ENDOF refreshF

		private void numericUpDownMultipl_ValueChanged(object sender, EventArgs e)
		{
			ParseStaticData();
			refreshData();
		}

		private void checkBoxUsingOffer_CheckedChanged(object sender, EventArgs e)
		{
			refreshData();
		}
	}
}
