using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExtension.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public string   FirstName     { get; set; }
        public string   LastName      { get; set; }
        public string   Gender        { get; set; }
        public DateTime DateOfBirth   { get; set; }        
        public string   MobileContact { get; set; }
        public string   HomeContact   { get; set; }
        public string   StreetAddress { get; set; }        
        public string   City          { get; set; }
        public string   PostalCode    { get; set; }
        public string   Province      { get; set; }
        public string   Country       { get; set; }
    }
}
