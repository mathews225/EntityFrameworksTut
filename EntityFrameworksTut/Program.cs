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

			foreach (var s in _context.Students.ToList()) {
				Console.WriteLine($"{s.Firstname} {s.Lastname}");
			}


			#endregion

		}
	}
}
