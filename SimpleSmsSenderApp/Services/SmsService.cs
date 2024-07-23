using Microsoft.Extensions.Options;
using SimpleSmsSenderApp.Models;
using System.Net;
using System.Text;

namespace SimpleSmsSenderApp.Services
{
	public class SmsService : ISmsService
	{
		private readonly IOptions<SmsSettings> _smsSettingService;

		public SmsService(IOptions<SmsSettings> smsSettingService)
		{
			_smsSettingService = smsSettingService;
		}

		public async Task<bool> SendSms(string phoneNumber, string message)
		{
			try
			{

				string ss = "";
				ss += "<?xml version='1.0' encoding='UTF-8'?>";
				ss += "<mainbody>";
				ss += "<header>";
				ss += "<company dil='TR'>Netgsm</company>";
				ss += $"<usercode>{_smsSettingService.Value.Username}</usercode>";
				ss += $"<password>{_smsSettingService.Value.Password}</password>";
				ss += "<type>1:n</type>";
				ss += $"<msgheader>{_smsSettingService.Value.Header}</msgheader>";
				ss += "<appkey></appkey>";
				ss += "</header>";
				ss += "<body>";
				ss += "<msg>";
				ss += $"<![CDATA[Mesaj {message}]]>";
				ss += "</msg>";
				ss += $"<no>{phoneNumber}</no>";
				ss += "</body>  ";
				ss += "</mainbody>";

				using WebClient client = new WebClient();
				HttpWebRequest request = WebRequest.Create(_smsSettingService.Value.PostAddress) as HttpWebRequest;
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				Byte[] postArray = Encoding.UTF8.GetBytes(ss);
				Byte[] response = client.UploadData(_smsSettingService.Value.PostAddress, "POST", postArray);
				Char[] returnChars = Encoding.UTF8.GetChars(response);

				await Task.CompletedTask;
				return true;


			}
			catch (Exception e)
			{

				return false;
			}
		}
	}
}
