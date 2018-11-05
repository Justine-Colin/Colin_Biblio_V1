using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colin_Biblio.Models;

namespace Colin_Biblio.ViewModels
{
    public class AuteursViewModel
    {
        public List<Auteur> Auteurs;
        public AuteursViewModel()
        {
            Dal dal = new Dal();
            Auteurs = dal.ObtientTousLesAuteurs();
            dal.Dispose();
        }
    }
}