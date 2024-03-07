namespace RefactorReportService.Configuration
{
	/// <summary>
	/// Configuration data for database.
	/// </summary>
	public class DatabaseConfiguration
	{
		/// <summary>
		/// Section containing configuration for the database.
		/// </summary>
		public const string ConfigSectionName = "Database";

		/// <summary>
		/// Connection to database.
		/// </summary>
		public string Connection { get; set; }
	}
}
