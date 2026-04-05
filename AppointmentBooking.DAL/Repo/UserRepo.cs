using AppointmentBooking.DAL.Context;
using AppointmentBooking.DAL.Entity;
using AppointmentBooking.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.DAL.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly DBContext _dbContext;
        public UserRepo()
        {
            _dbContext = new DBContext();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.Include(u => u.Patient).Include(u => u.Doctor).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> AddUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> AddPatientAsync(Patient patient)
        {
            await _dbContext.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
