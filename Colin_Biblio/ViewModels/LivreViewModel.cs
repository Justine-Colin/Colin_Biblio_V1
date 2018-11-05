using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colin_Biblio.Models;

namespace Colin_Biblio.ViewModels
{
    public class LivreViewModel
    {
        public readonly Livre livre;
        public readonly Auteur auteur;
        public readonly Client emprunteurActuel;
        public readonly List<Client> emprunteursPasses;
        public readonly bool LivreExiste;
        public LivreViewModel(int IDlivre)
        {
            Dal dal = new Dal();
            if (LivreExiste = dal.LivreExiste(IDlivre))
            {
                livre = dal.ObtenirLivre(IDlivre);
                auteur = dal.ObtenirAuteur(livre.IdAuteur);
                List<Emprunt> emprunts = dal.ObtenirEmpruntsLivre(IDlivre);
                if (emprunts != null)
                {
                    foreach (Emprunt emprunt in emprunts)
                    {
                        emprunteurActuel = dal.ObtenirClient(emprunt.IDClient);
                    }
                }
            }
        }
    }
}