using mobileApp.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mobileApp.Web.Models
{
    public class DbStudentRepository : IItemRepository
    {
        private readonly AppDbContext _dbContext;
        public DbStudentRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public void Add(Student item)
        {
            _dbContext.Students.Add(item);
            _dbContext.SaveChanges();
        }

        public Student Get(string id)
        {
            return _dbContext.Students.FirstOrDefault(s => s.Id.Equals(id));
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Students;
        }

        public Student Remove(string key)
        {
            var removed = _dbContext.Students.Remove(Get(key));
            _dbContext.SaveChanges();
            return removed.Entity;
        }

        public void Update(Student item)
        {
            _dbContext.Students.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
