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


			var sctrl = new StudentsController();


			#region GetAll
			var students = sctrl.GetAll();
			foreach (var s in students) {
				Console.WriteLine("{0,-12}{0,-12}",s.Lastname,s.Firstname );
			}
			#endregion

			#region GetByPK
			var id = 1;
			var student = sctrl.GetByPK(id);
			if (student == null) {
				Console.WriteLine($"Student w/ Id {id} Not Found");
			} else {
				Console.WriteLine("{0,-12}{0,-12}", student.Lastname, student.Firstname);
			}
			Console.WriteLine("\n");
			#endregion

			#region Create
			//var sGreg = new Student {
			//	Id = 0,
			//	Firstname = "Greg",
			//	Lastname = "Doud",
			//	StateCode = "OH",
			//	Gpa = 2.1m,
			//	Sat = 805,
			//	MajorId = 1
			//};

			//var sGregNew = sctrl.Create(sGreg);
			//Console.WriteLine($"{sGregNew.Id},  {sGregNew.Firstname},  {sGregNew.Lastname}");

			//Console.WriteLine("\n");
			#endregion

			#region Update
			var sGN = sctrl.GetByPK(66);

			// Set Firstname to "Gregory"
			sGN.Firstname = "Gregory";
			sctrl.Update(sGN);

			Console.WriteLine($"{sGN.Id},  {sGN.Firstname},  {sGN.Lastname}");

			Console.WriteLine("\n");
			#endregion

			#region Delete
			var sGNd = sctrl.Delete(67);

			Console.WriteLine("\n");
			#endregion




		}


		#region db connection
		static void Run1() {
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
