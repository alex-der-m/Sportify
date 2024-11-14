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

        [Required  (ErrorMessage= "El DNI es obligatorio")]
        public int Dni { get; set; }

        [Required  (ErrorMessage= "El nombre del usuario es obligatorio")]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required  (ErrorMessage= "Debe ingresar un correo electrónico válido")]
        public string Mail { get; set; }

        [Required  (ErrorMessage= "La contraseña es obligatoria")]
        public string? Password { get; set; }
        
        [Display(Name="Teléfono")]
        [Required  (ErrorMessage= "El número de teléfono es obligatorio")]
        public int Phone { get; set; }

        [Required  (ErrorMessage= "El domicilio es obligatorio")]
        [Display(Name="Dirección")]
        public string Address { get; set; }

        [Display(Name="Perfil")]
        [ForeignKey("ProfileId")] //Este era required solo
        public Profiles? Profile { get; set; }

        [ForeignKey("Profiles")] // y este era foreingKey (lo di vuelta)
        [Display(Name="Tipo de Usuario")]
        public int  ProfileId { get; set; }

        [Display(Name="Plan")]
        public Plans? Plans { get; set; }

        [ForeignKey("Plans")]
        [Display(Name="Tipo de Plan")]
        public int  PlanId { get; set; }

        public ICollection<Programmings>? Programmings { get; set; }

        [Display(Name="Documento Médico")]
        public bool MedicalDocument  { get; set; }

        [NotMapped]
        [Display(Name="Documento")]
        public IFormFile? Document { get; set; }

        public string? DocumentName { get; set; } // Para almacenar el nombre del archivo

        public byte[]? DocumentContent { get; set; } // Para almacenar el contenido del archivo


        [Display(Name="Activa")]
        public bool Active { get; set; }

    }
}