using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colin_Biblio.Models;

namespace Colin_Biblio.ViewModels
{
    public class IndexViewModel
    {
        private readonly List<Livre> livres;
        public List<LivreAut> livreAuts;
        public IndexViewModel()
        {
            Dal dal = new Dal();
            livres = dal.ObtenirTousLesLivres();
            livreAuts = new List<LivreAut>();
            foreach (Livre livre in livres)
            {
                Auteur auteur = dal.ObtenirAuteur(livre.IdAuteur);
                livreAuts.Add(new LivreAut { Livre = livre, Auteur = auteur });
            }
            dal.Dispose();
        }
    }
    public class LivreAut
    {
        public Livre Livre { get; set; }
        public Auteur Auteur { get; set; }
    }
}