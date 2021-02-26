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

		public Student GetByPK(int id) {
			return _context.Students.Find(id);
		}

		public Student Create(Student student) {
			if (student==null) {
				throw new Exception("ERROR: student cannot be null!");
			}
			if (student.Id != 0) {
				throw new Exception("ERROR: student.Id must be 0!");
			}
			_context.Students.Add(student);
			var rowAffected = _context.SaveChanges();
			if (rowAffected != 1) {
				throw new Exception("ERROR: student not created");
			}
			return student;
		}



		// 2	constructor 
		public StudentsController() {
			_context = new eddbContext();
		}



	}
}
