using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class AddressServiceImpl : IAddressService
{

    private readonly IMapper _mapper;

    public AddressServiceImpl(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public IEnumerable<Address> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().ToList();
    }

    public Address GetAddressById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var address = session.Query<Address>().SingleOrDefault(x => x.Id == id);

        if (address == null) throw new BadHttpRequestException("Address not found");

        return address;
    }

    public void DeleteAddress(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Address>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Address UpdateAddress(int id, AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        var address = _mapper.Map<Address>(addressDto);

        using var transaction = session.BeginTransaction();
        
        session.Merge(address);
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
}