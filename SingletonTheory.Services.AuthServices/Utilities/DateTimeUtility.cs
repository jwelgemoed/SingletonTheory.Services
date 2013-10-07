using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public static class DateTimeUtility
	{
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, string timeZoneId)
		{
			TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("UTC");
			try
			{
				tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			}
			finally { }

			return TimeZoneInfo.ConvertTimeFromUtc(dateTime, tzi);
		}

		public static DateTime ConvertTimeToUtc(DateTime dateTime, string timeZoneId)
		{
			TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("UTC");
			try
			{
				tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			}
			finally { }

			return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), tzi);
		}
	}
}