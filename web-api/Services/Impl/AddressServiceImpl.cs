using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class AddressServiceImpl : IAddressService
{
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

        var user = session.Query<User>().SingleOrDefault(x => x.Id == addressDto.UserId);
        var oldAddress = session.Query<Address>().SingleOrDefault(x => x.Id == id);

        if (user == null || oldAddress == null) throw new Exception("Not found");

        var address = new Address
        {
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

        session.Update(address);

        return address;
    }

    public ActionResult<Address> SaveAddress(AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = session.Query<User>().SingleOrDefault(x => x.Id == addressDto.UserId);

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