using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sportify_back.Models
{
    public class Users
    {

        [Column("IdUsers")]
        public int Id { get; set; }

        [Required]
        public int Dni { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Mail { get; set; }

        public string? Password { get; set; }

        public int Phone { get; set; }

        public string Address { get; set; }

      
        public Profiles? Profile { get; set; }

        [ForeignKey("Profiles")]
        public int  ProfileId { get; set; }

        public Plans? Plans { get; set; }

        [ForeignKey("Plans")]
        public int  PlanId { get; set; }

        public ICollection<Programmings>? Programmings { get; set; }

        public bool MedicalDocument  { get; set; }

        [NotMapped]
        public IFormFile? Document { get; set; }

        public bool Active { get; set; }

    }
}