using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IAddressService
{
    IEnumerable<Address> GetAll();

    Address GetAddressById(int id);

    Address UpdateAddress(int id, AddressDto addressDto);

    Address SaveAddress(AddressDto addressDto);

    void DeleteAddress(int id);

    bool IsEmpty();
}