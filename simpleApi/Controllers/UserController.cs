using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using simpleApi.DbContexts;
using simpleApi.Extensions;
using simpleApi.Model;
using simpleApi.Model.DTOs;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace simpleApi.Controllers;

public class Validator : AbstractValidator<UserModel>
{
    public Validator() 
    {
        RuleFor(x => x.Name).Empty().WithMessage("Name con not be empty");
        RuleFor(x => x.UserName).Empty().WithMessage("user name con not by empty").MaximumLength(50);
        RuleFor(x => x.Email).Empty().WithMessage("email con not by empty").EmailAddress();
        RuleFor(x => x.Password).MaximumLength(6).WithMessage("passowd length most 6  ").NotEmpty().WithMessage("passowrd con not be empty");
    }
}
public class UserController:Controller
{
    private readonly AppDbContext _context;
    public UserController( AppDbContext context) {
        _context = context;
    }
    /// <summary>
    /// Get All User As List
    /// </summary>
    /// <returns></returns>
    [Route("api/user/get_all")]
    [HttpGet]
    public async Task<IEnumerable<UserModel>> GetAllUserMeth()
    {
return await _context.FindAllUserAsync();
    }
    /// <summary>
    /// Get User By Id
    /// </summary>
    /// <returns></returns>
    [Route("api/user/get_by_id")]
    [HttpGet]
    public async Task<UserModel> GetUserByIdMeth(Guid Id)
    {
        return await _context.FindeUserByIdAsync(Id);
    }
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Route("api/user/create")]
    [HttpPost]
  public async Task<ActionResult<DataRespons>> CreateUserMeth([FromBody] UserDTOS user)
    {
        var userCreate = new UserModel()
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            UserName = user.UserName,
            Password = user.Password,
            AddressId = null,
        };
        AddressModel address = new AddressModel();
        /// if User Name Exist
       var  userEx= await _context.FindeUserByUserNameAsync(user.UserName);
        if(userEx != null) 
        {
            /// return Message If User Exist
            return new DataRespons()
            {
                Message = "user exist",
                StatusCode =(int) HttpStatusCode.Conflict
            };
        }
        /// If Address Not Null Then puh it To User.Address
        if(user.Address != null)
        {
            address = new AddressModel()
            {
                Id = Guid.NewGuid(),
                Location = user.Address.Location,
                Description = user.Address.Description,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
            };
            var res = await _context.CreateAddressAsync(address);
            if(res.StatusCode !=200) 
            {
                throw new Exception(JsonConvert.SerializeObject(res));
            }
            userCreate.AddressId = res.Id;
        }
       var respons = await _context.createUserAsync(userCreate);
        return Ok(respons);
    }
}
