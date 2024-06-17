using GameNow.Database;
using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNow.Infrastructure.Repositories
{
    public class UserRepository : IRepository<IdentityUser>
    {
        public GameNowContext _dbContext;

        public UserRepository(GameNowContext gameNowContext)
        {
            _dbContext = gameNowContext;
        }

        public async Task Add(IdentityUser entity)
        {
            await _dbContext.Users.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            var user = _dbContext.Users.Where(u => u.Id == Id.ToString()).First();
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var user = _dbContext.Users.Where(u => u.Id == Id.ToString()).First();
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Update(IdentityUser entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public IdentityUser GetById(int Id)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Id == Id.ToString());
        }

        public IdentityUser GetById(string Id)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Id == Id.ToString());
        }

        public IdentityUser GetByEmail(string Email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email == Email);
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return _dbContext.Users.ToList();
        }

    }
}
