using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sportify_back.Models
{
    public class Teachers
    {

        [Column("IdTeachers")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Dni { get; set; }

        [Required]
        public string Mail { get; set; }

        public int Phone { get; set; }

        public string Address { get; set; }

        public List<Activities> Activities { get; set; }

        public List<Classes> Classes { get; set; }
        public bool Active { get; set; }
        
    }
}