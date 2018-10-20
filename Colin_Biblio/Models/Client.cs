using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colin_Biblio.Models
{
    public class Client
    {
        public int IdClient { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MotDePasse { get; set; }

    }
}