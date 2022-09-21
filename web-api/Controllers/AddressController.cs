﻿using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    // GET: api/address
    [HttpGet]
    public ActionResult<IEnumerable<Address>> GetAll()
    {
        return _addressService.GetAll();
    }
    
    // GET: api/address/{id}
    [HttpGet("{id}")]
    public ActionResult<Address> GetAddress(int id)
    {
        return _addressService.GetAddressById(id);
    }
    
    // PUT: api/address/{id}
    [HttpPut("{id}")]
    public ActionResult<Address> UpdateAddress(int id, AddressDto addressDto)
    {
        try
        {
            return _addressService.UpdateAddress(id, addressDto);
        }
        catch
        {
            return NotFound();
        }
    }
    
    // POST: api/address
    [HttpPost]
    public ActionResult<Address> SaveAddress(AddressDto addressDto)
    {
        return _addressService.SaveAddress(addressDto);
    }
    
    // DELETE: api/address/{id}
    [HttpDelete("{id}")]
    public void DeleteAddress(int id)
    {
        _addressService.DeleteAddress(id);
    }
}