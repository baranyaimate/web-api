using FluentNHibernate.Conventions;
using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class AddressServiceImpl : IAddressService
{
    private readonly IMapper _mapper;

    private readonly IUserService _userService;

    public AddressServiceImpl(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public IEnumerable<Address> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().ToList();
    }

    public Address GetAddressById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var address = session.Query<Address>().SingleOrDefault(a => a.Id == id);

        if (address is null) throw new Exception($"Address({id}) not found");

        return address;
    }

    public void DeleteAddress(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var address = GetAddressById(id);

        if (address is null) throw new Exception($"Address({id}) not found");

        session.Query<Address>()
            .Where(a => a.Id == id)
            .Delete();
    }

    public Address UpdateAddress(int id, AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var address = _mapper.Map<Address>(addressDto);
        address.Id = id;

        using var transaction = session.BeginTransaction();

        session.Update(address);
        transaction.Commit();

        return address;
    }

    public Address SaveAddress(AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var address = _mapper.Map<Address>(addressDto);

        session.Save(address);

        return address;
    }

    public IEnumerable<Address> GetAddressesByUserId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        if (_userService.GetUserById(id).Equals(null)) throw new Exception($"User({id}) not found");
        
        return session.Query<Address>().Where(address => address.User.Id == id).ToList();
    }

    public bool IsEmpty()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().IsEmpty();
    }
}