using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin_Biblio.Models
{
    public interface IDal : IDisposable
    {
        //Auteur
        List<Auteur> ObtientTousLesAuteurs();
        void CreerAuteur(string nom, string prenom);
        int AjouterAuteur(string nom, string prenom);
        Auteur ObtenirAuteur(int id);
        void ModifierAuteur(int id, string nom, string prenom);
        bool AuteurExiste(string nom);

        //Client
        Client ObtenirClient(int id);
        Client ObtenirClient(string idStr);
        int AjouterClient(string nom, string email, string motdepasse);
        Client Authentifier(string nom, string motdepasse);

        //Livre
        int AjouterLivre(string titre, string dateparution, string auteur);
        Livre ObtenirLivre(int id);

        // Emprunt
        int AjouterEmprunt(int idClient, int idLivre, DateTime dateEmprunt);
        void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt, DateTime dateRetour);
        void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt);
        void RetournerEmprunt(int id);
        Emprunt ObtenirEmprunt(int id);
    }
}
