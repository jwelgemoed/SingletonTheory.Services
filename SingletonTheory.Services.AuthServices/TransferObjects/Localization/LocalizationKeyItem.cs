
namespace SingletonTheory.Services.AuthServices.TransferObjects.Localization
{
	public class LocalizationKeyItem
	{
		public long LocaleId { get; set; }
		public string Locale { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
	}
}