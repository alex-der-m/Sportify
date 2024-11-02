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

        [Required  (ErrorMessage= "El nombre del profesor es obligatorio")]
        public string Name { get; set; }

        [Required  (ErrorMessage= "El DNI es obligatorio")]
       
        public int Dni { get; set; }

        [Required  (ErrorMessage= "Debe ingresar un correo electrónico válido")]
        public string Mail { get; set; }
        [Required  (ErrorMessage= "El número de teléfono es obligatorio")]
        public int Phone { get; set; }
        [Required  (ErrorMessage= "El domicilio es obligatorio")]
        public string Address { get; set; }

        public Activities? Activities { get; set; } 

        [Display(Name ="Activities")]
        public int  ActivitiesId { get; set; }
        public List<Classes> Classes { get; set; } = new List<Classes>();
        public bool Active { get; set; }
        
    }
}