﻿namespace Entities.Concrete;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    //Relations
    public int? CartId { get; set; }
    public Cart? Cart { get; set; }
    public List<Favourite> Favourites { get; set; }
    public List<Sale> Sales { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Shipping> Shippings { get; set; }

}