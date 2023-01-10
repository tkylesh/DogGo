﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hmmm... You should really add a name...")]
        [MaxLength(35, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        public List<Dog> Dogs { get; set; }

    }
}
