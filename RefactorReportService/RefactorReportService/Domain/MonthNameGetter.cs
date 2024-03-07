using System.Globalization;

namespace RefactorReportService.Domain
{
	public static class MonthNameGetter
	{
		public static string GetName(int year, int monthNum)
		{
			if (year > 2020 && monthNum > 1)
			{
				throw new Exception("Invalid date.");
			}

			return new DateTime(year, monthNum, 1)
				.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
		}
	}
}
