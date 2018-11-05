using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin_Biblio.Models
{
    public interface IDal : IDisposable
    {
        // Auteur
        List<Auteur> ObtientTousLesAuteurs();
        int AjouterAuteur(string nom, string prenom);
        Auteur ObtenirAuteur(int id);
        void ModifierAuteur(int id, string nom, string prenom);
        bool AuteurExiste(string nom);
        bool AuteurExiste(int IDauteur);

        // Client
        Client ObtenirClient(int id);
        Client ObtenirClient(string idStr);
        int AjouterClient(string nom, string mail, string motDePasse);
        Client Authentifier(string nom, string motDePasse);
        Client AuthentifierMail(string email, string motDePasse);

        // Livre
        int AjouterLivre(string titre, DateTime dateParution, int IDauteur);
        Livre ObtenirLivre(int id);
        List<Livre> ObtenirTousLesLivres();
        List<Livre> ObtenirLivresAuteur(int IDauteur);
        bool LivreExiste(int IDlivre);

        // Emprunt
        int AjouterEmprunt(int idClient, int idLivre);
        void ModifierEmprunt(int id, int idClient, int idLivre);
        void RetournerEmprunt(int id);
        Emprunt ObtenirEmprunt(int id);
        List<Emprunt> ObtenirEmpruntsClient(int IDclient);
        List<Emprunt> ObtenirEmpruntsLivre(int IDlivre);
    }
}
