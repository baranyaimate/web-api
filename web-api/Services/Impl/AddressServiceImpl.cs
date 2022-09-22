using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class AddressServiceImpl : IAddressService
{

    private readonly IUserService _userService;

    public AddressServiceImpl(IUserService userService)
    {
        _userService = userService;
    }
    
    public ActionResult<IEnumerable<Address>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().ToList();
    }

    public ActionResult<Address> GetAddressById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().Single(x => x.Id == id);
    }

    public void DeleteAddress(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Address>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public ActionResult<Address> UpdateAddress(int id, AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = _userService.GetUserById(addressDto.UserId).Value;
        var oldAddress = GetAddressById(id).Value;
        
        if (user == null || oldAddress == null) throw new Exception("Not found");
        
        var address = new Address
        {
            Id = id,
            Country = addressDto.Country,
            City = addressDto.City,
            Postcode = addressDto.Postcode,
            State = addressDto.State,
            StreetName = addressDto.StreetName,
            StreetNumber = addressDto.StreetNumber,
            User = user,
            CreatedAt = oldAddress.CreatedAt,
            UpdatedAt = DateTime.Now
        };

        using var transaction = session.BeginTransaction();
        
        session.Merge(address);
        transaction.Commit();

        return address;
    }

    public ActionResult<Address> SaveAddress(AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = _userService.GetUserById(addressDto.UserId).Value;

        if (user == null) throw new Exception("Not found");

        var address = new Address
        {
            Country = addressDto.Country,
            City = addressDto.City,
            Postcode = addressDto.Postcode,
            State = addressDto.State,
            StreetName = addressDto.StreetName,
            StreetNumber = addressDto.StreetNumber,
            User = user,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        session.Save(address);

        return address;
    }
}