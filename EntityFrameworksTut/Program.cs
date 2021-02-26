using System;
using System.Linq;
using EntityFrameworksTut.Models;

/* tools > Nuget Package Manager Console
 * PM> install-package Microsoft.EntityFrameworksCore.Tools
 * PM> install-package Microsoft.EntityFrameworksCore.SQLServer
 * PM> scaffold-dbcontext "server=localhost\sqlexpress;database=eddb;trusted_connection=true;" Microsoft.EntityFrameworkCOre.SqlServer -OutputDir Models
 * 
 */

namespace EntityFrameworksTut {
	class Program {
		static void Main(string[] args) {

			#region db connection

			var _context = new eddbContext();

			#region Example 1

			//var students = _context.Students.ToList();

			//foreach (var s in students) { 
			//	Console.WriteLine($"{s.Firstname} {s.Lastname}");
			//}

			#endregion

			#region Example 2

			//foreach (var s in _context.Students.ToList()) {
			//	Console.WriteLine($"{s.Firstname} {s.Lastname}");
			//}

			#endregion

			#region Example 3

			_context.Students.ToList().ForEach(s => Console.WriteLine($"{s.Firstname} {s.Lastname}"));

			#endregion

			#region LINQ

			var major = from m in _context.Majors
									where m.MinSat < 1000
									orderby m.Description
									select m;

			Console.WriteLine($"\nMinSat\t Description");
			foreach (var m in major) {
				Console.WriteLine($"{m.MinSat} \t {m.Description}");
			}

			// Join student & Major print name and major

			var allStudents = from s in _context.Students
												join m in _context.Majors
												on s.MajorId equals m.Id
												select new {
													lname = s.Lastname,
													fname = s.Firstname,
													major = s.MajorId == null ? "Undeclared" : m.Description
												};

			Console.WriteLine("\n");
			Console.WriteLine("{0,-15}{1,-15}{2,-10}", "LAST", "FIRST", "DESCRIPTION");

			foreach (var s in allStudents) {
				Console.WriteLine("{0,-15}{1,-15}{2,-10}",$"{s.lname},",s.fname,s.major);
			}

			#endregion

			#endregion

		}
	}
}
