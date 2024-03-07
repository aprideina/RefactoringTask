using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;
using RefactorReportService.Configuration;
using RefactorReportService.Domain;

namespace RefactorReportService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReportController : ControllerBase
	{
		private readonly DatabaseConfiguration config;
		
		public ReportController(IOptions<DatabaseConfiguration> databaseConfiguration) {
			config = databaseConfiguration.Value;
		}

		[HttpGet]
		[Route("{year}/{month}")]
		public IActionResult Download(int year, int month)
		{
			var actions = new List<(Action<Employee, Report>, Employee)>();
			var report = new Report() { S = MonthNameGetter.GetName(year, month) };

			var conn = new NpgsqlConnection(config.Connection);
			conn.Open();
			var cmd = new NpgsqlCommand("SELECT d.name from deps d where d.active = true", conn);
			var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				List<Employee> emplist = new List<Employee>();
				var depName = reader.GetString(0);
				var conn1 = new NpgsqlConnection(config.Connection);
				conn1.Open();
				var cmd1 = new NpgsqlCommand("SELECT e.name, e.inn, d.name from emps e left join deps d on e.departmentid = d.id", conn1);
				var reader1 = cmd1.ExecuteReader();
				while (reader1.Read())
				{
					var emp = new Employee() { Name = reader1.GetString(0), Inn = reader1.GetString(1), Department = reader1.GetString(2) };
					emp.BuhCode = EmpCodeResolver.GetCode(emp.Inn).Result;
					emp.Salary = emp.Salary();
					if (emp.Department != depName)
						continue;
					emplist.Add(emp);
				}

				actions.Add((new ReportFormatter(null).NL, new Employee()));
				actions.Add((new ReportFormatter(null).WL, new Employee()));
				actions.Add((new ReportFormatter(null).NL, new Employee()));
				actions.Add((new ReportFormatter(null).WD, new Employee() { Department = depName }));
				for (int i = 1; i < emplist.Count(); i++)
				{
					actions.Add((new ReportFormatter(emplist[i]).NL, emplist[i]));
					actions.Add((new ReportFormatter(emplist[i]).WE, emplist[i]));
					actions.Add((new ReportFormatter(emplist[i]).WT, emplist[i]));
					actions.Add((new ReportFormatter(emplist[i]).WS, emplist[i]));
				}

			}
			actions.Add((new ReportFormatter(null).NL, null));
			actions.Add((new ReportFormatter(null).WL, null));

			foreach (var act in actions)
			{
				act.Item1(act.Item2, report);
			}
			report.Save();
			var file = System.IO.File.ReadAllBytes("D:\\report.txt");
			var response = File(file, "application/octet-stream", "report.txt");
			return response;
		}
	}
}
