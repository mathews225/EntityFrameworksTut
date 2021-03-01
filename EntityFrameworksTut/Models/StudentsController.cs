using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworksTut.Models {
	public class StudentsController {

		// 1	private so that no other class can use it
		//		readonly allows for it to be written to within constructors only
		//		this also take cares of the open reader and close reader r 
		private readonly eddbContext _context;


		// 3 Methods

		// wrap IEnumerable to make the method async
		public async Task<IEnumerable<Student>> GetAll() {
			return await _context.Students.ToListAsync();
		}

		public async Task<Student> GetByPK(int id) {
			return await _context.Students.FindAsync(id);
		}

		public async Task<Student> Create(Student student) {
			if (student==null) {
				throw new Exception("ERROR: student cannot be null!");
			}
			if (student.Id != 0) {
				throw new Exception("ERROR: student.Id must be 0!");
			}
			_context.Students.Add(student); // now await or AddAsync b/c sudent is added to cache and not the DB
			var rowAffected = await _context.SaveChangesAsync();
			if (rowAffected != 1) {
				throw new Exception("ERROR: student not created");
			}
			return student;
		}
		
		// void gets changed to Task with async
		public async Task Update(Student student) {
			if (student == null) {
				throw new Exception("ERROR: student cannot be null!");
			}
			if (student.Id <= 0) {
				throw new Exception("ERROR: student.Id must be greater than 0!");
			}
			_context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			var rowAffected = await _context.SaveChangesAsync();
			if (rowAffected != 1) {
				throw new Exception("ERROR: change failed!");
			}
			return;
		}

		public async Task<Student> Delete(int id) {
			var student =_context.Students.Find(id);
			if (student==null) {
				return null;
			}
			_context.Students.Remove(student);
			var rowsAffected = await _context.SaveChangesAsync();
			if (rowsAffected != 1) {
				throw new Exception("ERROR: Remove Failed");
			}
			Console.WriteLine($"STUDENT DELETED!");
			//Console.WriteLine("0,-6}{1,-15}{2,-15}",student.Id,student.Lastname,student.Firstname); Doesn't work 
			return student;
		}

		/* Read all the students with SAT values between 1000 and 1200 inclusive
		 * order the results by SAT score descending */

		public async Task<IEnumerable<Student>> GetBySatRange(int lowSat, int highSat) {
			return await _context.Students.Where(s => s.Sat >= lowSat && s.Sat <= highSat).OrderByDescending(s => s.Sat).ToListAsync();
		}

		public async Task<IEnumerable<Student>> GetBySatRangeQ(int lowSat, int highSat) {
			return await (from s in _context.Students
							where s.Sat >= lowSat && s.Sat <= lowSat
							orderby s.Sat descending
							select s).ToListAsync();
		}


		// 2	constructor
		public StudentsController() {
			_context = new eddbContext();
		}



	}
}
