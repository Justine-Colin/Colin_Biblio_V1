using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colin_Biblio.Models
{
    public class Emprunt
    {
        [Key]
        public int IDEmprunt { get; set; }
        [Required]
        public int IDClient { get; set; }
        [Required]
        public int IDLivre { get; set; }
        [Required]
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }
    }
}