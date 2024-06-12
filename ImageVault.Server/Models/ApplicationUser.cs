    using Microsoft.AspNetCore.Identity;

    namespace ImageVault.Server.Models;

    public class ApplicationUser : IdentityUser
    {
        public string? PreferedColorSchema { get; set; } = "light";

        public string? ProfilePicture { get; set; }

        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        
        public override string Email { get; set; }

        public override string? UserName
        {
            get => Email;
            set => base.UserName = value;
        }

        public string? CountryOfOrigin { get; set; } 
        
    }