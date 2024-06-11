    using Microsoft.AspNetCore.Identity;

    namespace ImageVault.Server.Models;

    public class ApplicationUser : IdentityUser
    {
        public string PreferedColorSchema { get; set; } = "light";

        public string ProfilePicture { get; set; }

        public string FirstName { get; set; }
      
        public string LastName { get; set; }

        public string CountryOfOrigin { get; set; } 
        
    }