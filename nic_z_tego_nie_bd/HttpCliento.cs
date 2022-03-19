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
		//HttpClient client = new HttpClient();
		public static int reqInLastMinute;
		public static List<long> handledRequests = new List<long>();
		new public Task<HttpResponseMessage> GetAsync(string strong)
		{
			handledRequests.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds());
			handledRequests = handledRequests.Where((long x) => DateTimeOffset.Now.ToUnixTimeMilliseconds() - x < 60000).ToList();
			reqInLastMinute = handledRequests.Count;
			return base.GetAsync(strong);
		}
//		public string getStringFromUrl(string webAddres)
//		{
//			handledRequests.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds());
//			handledRequests = handledRequests.Where((long x) => DateTimeOffset.Now.ToUnixTimeMilliseconds() - x < 60000).ToList();
//			reqInLastMinute = handledRequests.Count;
//			var clResponse = client.GetAsync(webAddres);
//			var cwResponse = clResponse.Result.Content.ReadAsStringAsync();
//			string stResponse = cwResponse.Result;
//		//	client.Dispose();
//			return stResponse;
//		}
	//	public static async Task<string> getStringFromUrlAsync(string webAddres)
	//	{
	//		handledRequests.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds());
	//		handledRequests = handledRequests.Where((long x) => DateTimeOffset.Now.ToUnixTimeMilliseconds() - x < 60000).ToList();
	//		reqInLastMinute = handledRequests.Count;
	//		var clResponse = await client.GetAsync(webAddres);
	//		var cwResponse = clResponse.Result.Content.ReadAsStringAsync();
	//		string stResponse = cwResponse.Result;
	//		//	client.Dispose();
	//		return stResponse;
	//	}






		//	public static async Task kUTAS()
		//	{
		//		// Call asynchronous network methods in a try/catch block to handle exceptions.
		//		try
		//		{
		//			HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
		//			response.EnsureSuccessStatusCode();
		//			string responseBody = await response.Content.ReadAsStringAsync();
		//			// Above three lines can be replaced with new helper method below
		//			// string responseBody = await client.GetStringAsync(uri);
		//
		//			Console.WriteLine(responseBody);
		//		}
		//		catch (HttpRequestException e)
		//		{
		//			Console.WriteLine("\nException Caught!");
		//			Console.WriteLine("Message :{0} ", e.Message);
		//		}
		//	}
	}
}
