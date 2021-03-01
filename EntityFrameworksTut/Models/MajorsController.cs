using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace EntityFrameworksTut.Models {
	public class MajorsController {
		private readonly eddbContext _context;

		public IEnumerable<Major> GetAll() {
			return _context.Majors.ToList();
		}

		public Major GetByPK(int id) {
			return _context.Majors.Find(id);
		}

		public Major Create(Major major) {
			if (major == null) { 
				throw new Exception("ERROR: Major cannot be null");
			}
			if (major.Id!=0) {
				throw new Exception("ERROR: Major id must be 0");
			}
			_context.Majors.Add(major);
			var recAffected = _context.SaveChanges();
			if (recAffected!=1) {
				throw new Exception("ERROR: Major not created");
			}
			return major;
		}

		public void Update(Major major) {
			if (major == null) {
				throw new Exception("ERROR: Major cannot be null");
			}
			if (major.Id <=0 ) {
				throw new Exception("ERROR major ID must be 0");
			}
			_context.Entry(major).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected != 0) {
				throw new Exception("ERROR: change failed!");
			}
			return;
		}

		public Major Delete(int id) {
			var major = _context.Majors.Find(id);
			if (major == null) {
				return null;
			}
			_context.Majors.Remove(major);
			var rowsAffected = _context.SaveChanges();
			if (rowsAffected!=1) {
				throw new Exception("EROOR: Remove failed");
			}
			Console.WriteLine("Major Deleted");
			return major;
		}
		public MajorsController() {
			_context = new eddbContext();
		}
	
	
	}
}
