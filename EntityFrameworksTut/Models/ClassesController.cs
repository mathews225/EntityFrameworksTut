using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EntityFrameworksTut.Models {
	public class ClassesController {

		private readonly eddbContext _context;

		public IEnumerable<Class> GetAll() {
			return _context.Classes.ToList();
		}

		public Class GetByPK(int id) {
			return _context.Classes.Find(id);
		}

		public Class Create(Class course) {
			if (course == null) {
				throw new Exception("ERROR: Class cannot be null");
			}
			if (course.Id != 0) {
				throw new Exception("ERROR: Class ID must be 0");
			}
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected != 1) {
				throw new Exception("ERROR: Class not created");
			}
			return course;
		}

		public void Update(Class course) {
			if (course == null) {
				throw new Exception("ERROR: Class cannot be null");
			}
			if (course.Id <= 0) {
				throw new Exception("ERROR: course cannot be greater than 0");
			}
			_context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected!=1) {
				throw new Exception("ERROR: Change Failed");
			}
			return;
		}

		public Class Delete(int id) {
			var course = _context.Classes.Find(id);
			if (course == null) {
				return null;
			}
			_context.Classes.Remove(course);
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected!=1) {
				throw new Exception("ERROR: Remove Failed");
			}
			Console.WriteLine("STUDENT DELETED");
			return course;
		}










		public ClassesController() {
			_context = new eddbContext();
		}

	}
}
