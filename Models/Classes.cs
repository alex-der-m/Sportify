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
    public class Classes
    {

        [Column("IdClasses")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Activities Activities { get; set; }

        [ForeignKey("Activities")]
        public int  ActivityId { get; set; }

        [Required]
        public DateTime Sched { get; set; }

        public Teachers Teachers { get; set; }

        [ForeignKey("Teachers")]
        public int  TeacherId { get; set; }

        [Required]
        public ICollection<Programmings> Programmings { get; set; }

        [Required]
        public int Quota { get; set; }

        public bool Active { get; set; }
        
    }
}