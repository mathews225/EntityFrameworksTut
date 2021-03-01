using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EntityFrameworksTut.Models {
	public class MajorsController {
		private readonly eddbContext _context;

		public async Task<IEnumerable<Major>> GetAll() {
			return await _context.Majors.ToListAsync();
		}

		public async Task<Major> GetByPK(int id) {
			return await _context.Majors.FindAsync(id);
		}

		public async Task<Major> Create(Major major) {
			if (major == null) { 
				throw new Exception("ERROR: Major cannot be null");
			}
			if (major.Id!=0) {
				throw new Exception("ERROR: Major id must be 0");
			}
			_context.Majors.Add(major);
			var recAffected = await _context.SaveChangesAsync();
			if (recAffected!=1) {
				throw new Exception("ERROR: Major not created");
			}
			return major;
		}

		public async Task Update(Major major) {
			if (major == null) {
				throw new Exception("ERROR: Major cannot be null");
			}
			if (major.Id <=0 ) {
				throw new Exception("ERROR major ID must be greater than 0");
			}
			_context.Entry(major).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			var rowsAffected = await _context.SaveChangesAsync();
			if (rowsAffected != 0) {
				throw new Exception("ERROR: change failed!");
			}
			return;
		}

		public async Task<Major> Delete(int id) {
			var major = await _context.Majors.FindAsync(id);
			if (major == null) {
				return null;
			}
			int count = await _context.Students.Where(s => s.MajorId == major.Id).CountAsync(); // make suire no students have the major before deletion
			if (count != 0 ) {
				throw new Exception($"ERROR: Cannot delete major. {count} Student(s) with major");
			}
			_context.Majors.Remove(major);
			var rowsAffected = await _context.SaveChangesAsync();
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
