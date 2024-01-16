using Microsoft.EntityFrameworkCore;
using simpleApi.DbContexts;
using simpleApi.Model;
using System.Net;

namespace simpleApi.Extensions
{
    /// <summary>
    /// User Extensions To Usefull Functionlity
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Find User By Id Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static async Task<UserModel> FindeUserByIdAsync(this AppDbContext _context, Guid Id)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Id == Id);
        }
        /// <summary>
        ///  Find User By User Name Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static async Task<UserModel> FindeUserByUserNameAsync(this AppDbContext _context, string UserName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
        }
        /// <summary>
        /// Get All User Function
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<UserModel>>FindAllUserAsync(this AppDbContext _context)
        {
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// Create User Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<DataRespons> createUserAsync(this AppDbContext _context, UserModel user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

            }catch (Exception ex)
            {
                return new DataRespons() {
                    Message = ex.Message,
                    StatusCode =(int) HttpStatusCode.BadRequest,
                    };
            }
            return new DataRespons()
            {
                Id = user.Id,
                Message = "Success",
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
