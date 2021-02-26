using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworksTut.Models {
	public class StudentsController {

		// 1	readonly allows for it to be written to within constructors only
		//		this also take cares of the open reader and close reader r 
		private readonly eddbContext _context;


		// 3 Methods
		public IEnumerable<Student> GetAll() {
			return _context.Students.ToList();
		}


		// 2	constructor 
		public StudentsController() {
			_context = new eddbContext();
		}



	}
}
