using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nic_z_tego_nie_bd
{
	class AuctionHouseBeta : IAuctionHouse
	{
		/// <summary>
		/// Const links
		/// </summary>
		private const string auctionsUrl = "https://api.hypixel.net/skyblock/auctions?page=";
		private const string auctions_endedUrl = "https://api.hypixel.net/skyblock/auctions_ended";

		/// <summary>
		/// Whole ah content; always up to date
		/// </summary>
		public AuctionHouseInstance.auctionHouse ahCache { get; set; }
		/// <summary>
		/// Checks how long ago full ah was refreshed
		/// </summary>
		int sinceLastRefresh { get; set; }
		bool gatherWholeAH { get; set; }

		HttpCliento httpCliento;
		int floatingAge;

		AuctionHouseBeta()
		{
			sinceLastRefresh = -1;
			gatherWholeAH = true;
		}


		public async Task<bool> refresh()
		{
			await Task.Delay(0);
			return true;
		}
		public void hardrefresh()
		{
			gatherWholeAH = true;
		}
	}
}
