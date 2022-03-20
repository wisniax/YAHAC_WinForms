using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace nic_z_tego_nie_bd
{
	//Reference: https://docs.microsoft.com/pl-pl/dotnet/api/system.net.http.httpclient?view=net-6.0
	class HttpCliento : HttpClient
	{
		public static int reqInLastMinute;
		public static List<long> handledRequests = new List<long>();
		new public Task<HttpResponseMessage> GetAsync(string strong)
		{
			handledRequests.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds());
			handledRequests = handledRequests.Where((long x) => DateTimeOffset.Now.ToUnixTimeMilliseconds() - x < 60000).ToList();
			reqInLastMinute = handledRequests.Count;
			return base.GetAsync(strong);
		}
	}
}
