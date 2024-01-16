using Microsoft.EntityFrameworkCore;
using simpleApi.DbContexts;
using simpleApi.Model;
using System.Net;
using Newtonsoft.Json;

namespace simpleApi.Extensions
{
    /// <summary>
    /// Address Extensions To Usefull Functionlity
    /// </summary>
    public static class AddressExtensions
    {
        /// <summary>
        /// Find Address By Id Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static async Task<AddressModel> FindeAddressByIdAsync(this AppDbContext _context, Guid Id)
        {
            return await _context.Address.FirstOrDefaultAsync(u => u.Id == Id);
        }
        /// <summary>
        ///  Find Address By User Id Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static async Task<AddressModel> FindeAddressByUserIdAsync(this AppDbContext _context, Guid Id)
        {
            var addressId = await _context.FindeUserByIdAsync(Id);
            /// If the User Has No Address
            if(addressId == null)
            {
                var error = new DataRespons()
                {
                    Id = Id,
                    Message = "The User has no address",
                    StatusCode = (int)HttpStatusCode.NotFound,
                };
                throw new Exception(JsonConvert.SerializeObject(error));
            }
            return await _context.Address.FirstOrDefaultAsync(a => a.Id == addressId.AddressId);
        }
        /// <summary>
        /// Get All Address Function
        /// </summary>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<AddressModel>> FindAllAddressAsync(this AppDbContext _context)
        {
            return await _context.Address.ToListAsync();
        }
        /// <summary>
        /// Create Address Async Function
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateAddressAsync(this AppDbContext _context, AddressModel address)
        {
            try
            {
                _context.Address.Add(address);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return new DataRespons()
                {
                    Message = ex.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            return new DataRespons()
            {
                Id = address.Id,
                Message = "Success",
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
