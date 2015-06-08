namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            POI = new HashSet<POI>();
            Uitstap = new HashSet<Uitstap>();
            AspNetRoles = new HashSet<AspNetRoles>();
            Uitstap1 = new HashSet<Uitstap>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsPending { get; set; }

        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        public ICollection<POI> POI { get; set; }

        public ICollection<Uitstap> Uitstap { get; set; }

        public ICollection<AspNetRoles> AspNetRoles { get; set; }

        public ICollection<Uitstap> Uitstap1 { get; set; }
    }
}
