using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IAddressService
{
    IEnumerable<Address> GetAll();

    Address GetAddressById(int id);

    Address UpdateAddress(int id, AddressDto addressDto);

    Address SaveAddress(AddressDto addressDto);

    IEnumerable<Address> GetAddressesByUserId(int id);

    void DeleteAddress(int id);

    bool IsEmpty();
}