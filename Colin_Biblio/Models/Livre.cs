using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colin_Biblio.Models
{
    public class Livre
    {
        [Key]
        public int IdLivre { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public DateTime DateParution { get; set; }
        [Required]
        public int IdAuteur { get; set; }
    }
}