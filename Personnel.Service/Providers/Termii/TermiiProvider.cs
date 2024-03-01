using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Personnel.Model.ServiceModel;

namespace Personnel.Service.Providers.Termii
{
	public class TermiiProvider : ITermiiProvider
	{
		private readonly HttpClient _httpClient;

		public TermiiProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<TermiiResponse> SendMessage(NotificationModel model)
		{
			_httpClient.BaseAddress = new Uri("https://api.ng.termii.com");
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			string requestUrl = "/api/sms/send";

			HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Application.Json);
			try
			{
				HttpResponseMessage httpResponse = await _httpClient.PostAsync(requestUrl, content);
				var stringContent = await httpResponse.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<TermiiResponse>(stringContent);
				return response;

			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<BulkResponse> SendBulkMessage(BulkNotifiaction model)
		{
			_httpClient.BaseAddress = new Uri("https://api.ng.termii.com");
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			string requestUrl = "/api/sms/send/bulk";

			HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Application.Json);
			try
			{
				HttpResponseMessage httpResponse = await _httpClient.PostAsync(requestUrl, content);
				var stringContent = await httpResponse.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<BulkResponse>(stringContent);
				return response;

			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}


	public interface ITermiiProvider
	{
		Task<TermiiResponse> SendMessage(NotificationModel model);
		Task<BulkResponse> SendBulkMessage(BulkNotifiaction model);

	}
}
