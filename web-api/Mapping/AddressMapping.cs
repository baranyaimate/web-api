﻿using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class AddressMapping : ClassMap<Address>
{
    public AddressMapping()
    {
        Table("addresses");
        Id(x => x.Id);
        Map(x => x.City);
        Map(x => x.Country);
        Map(x => x.Postcode);
        Map(x => x.State);
        Map(x => x.StreetName);
        Map(x => x.StreetNumber);
        References(x => x.User);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
    }
}