namespace RefactorReportService.Domain
{
    public class Report
    {
        public string S { get; set; }
        public void Save()
		{
			File.WriteAllText("D:\\report.txt", S);
        }
    }
}
