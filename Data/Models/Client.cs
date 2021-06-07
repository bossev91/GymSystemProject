using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymSys.Data.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public int LastName { get; set; }

        [Required]
        public int PersonalNumber { get; set; }

        // [Required]
        // public Town Town { get; set; }

        [Column(TypeName = "char(30)")]
        public string PhoneNumber { get; set; }

        [EmailAddress]        
        public string EmailAddress { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int ClientCardId { get; set; }

        public ClientCard ClientCard { get; set; }     

        public int TownId { get; set; }

        public Town Town { get; set; }
    }
}
