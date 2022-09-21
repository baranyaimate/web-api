using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IAddressService
{
    ActionResult<IEnumerable<Address>> GetAll();
    
    ActionResult<Address> GetAddressById(int id);

    ActionResult<Address> UpdateAddress(int id, AddressDto addressDto);
    
    ActionResult<Address> SaveAddress(AddressDto addressDto);

    void DeleteAddress(int id);
}