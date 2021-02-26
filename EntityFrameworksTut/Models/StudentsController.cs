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

		public void Update(Student student) {
			if (student == null) {
				throw new Exception("ERROR: student cannot be null!");
			}
			if (student.Id <= 0) {
				throw new Exception("ERROR: student.Id must be 0!");
			}
			_context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			var rowAffected = _context.SaveChanges();
			if (rowAffected != 1) {
				throw new Exception("ERROR: change failed!");
			}
			return;
		}

		public Student Delete(int id) {
			var student =_context.Students.Find(id);
			if (student==null) {
				return null;
			}
			_context.Students.Remove(student);
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected != 1) {
				throw new Exception("ERROR: Remove Failed");
			}
			Console.WriteLine($"STUDENT DELETED!");
			//Console.WriteLine("0,-6}{1,-15}{2,-15}",student.Id,student.Lastname,student.Firstname); Doesn't work 
			return student;
		}

		/* Read all the students with SAT values between 1000 and 1200 inclusive
		 * order the results by SAT score descending */

		public IEnumerable<Student> GetBySatRange(int lowSat, int highSat) {
			return _context.Students.Where(s => s.Sat >= lowSat && s.Sat <= highSat).OrderByDescending(s => s.Sat).ToList();
		}

		public IEnumerable<Student> GetBySatRangeQ(int lowSat, int highSat) {
			return (from s in _context.Students
							where s.Sat >= lowSat && s.Sat <= lowSat
							orderby s.Sat descending
							select s).ToList();
		}


		// 2	constructor
		public StudentsController() {
			_context = new eddbContext();
		}



	}
}
