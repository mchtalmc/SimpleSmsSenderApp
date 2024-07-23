namespace SimpleSmsSenderApp.Services
{
	public interface ISmsService
	{
		Task<bool> SendSms(string phoneNumber, string message);
	}
}
