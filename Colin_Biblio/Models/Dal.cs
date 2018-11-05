using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Colin_Biblio.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;
        public Dal()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        // Auteur
        public List<Auteur> ObtientTousLesAuteurs()
        {
            return bdd.Auteurs.ToList();
        }

        public int AjouterAuteur(string nom, string prenom)
        {
            Auteur auteurAjoute = bdd.Auteurs.Add(new Auteur { Nom = nom, Prenom = prenom });
            bdd.SaveChanges();
            return auteurAjoute.IdAuteur;
        }

        public Auteur ObtenirAuteur(int id)
        {
            return bdd.Auteurs.FirstOrDefault(auteur => auteur.IdAuteur == id);
        }

        public Auteur ObtenirAuteur(string nom)
        {
            return bdd.Auteurs.FirstOrDefault(auteur => auteur.Nom == nom);
        }

        public void ModifierAuteur(int id, string nom, string prenom)
        {
            Auteur auteurTrouve = bdd.Auteurs.FirstOrDefault(auteur => auteur.IdAuteur == id);
            if (auteurTrouve != null)
            {
                auteurTrouve.Nom = nom;
                auteurTrouve.Prenom = prenom;
                bdd.SaveChanges();
            }
        }

        public bool AuteurExiste(string nom)
        {
            if (bdd.Auteurs.FirstOrDefault(auteur => auteur.Nom == nom) != null)
                return true;
            else
                return false;
        }

        public bool AuteurExiste(int IDauteur)
        {
            if (bdd.Auteurs.FirstOrDefault(auteur => auteur.IdAuteur == IDauteur) != null)
                return true;
            else
                return false;
        }

        // Client
        public Client ObtenirClient(int id)
        {
            return bdd.Clients.FirstOrDefault(client => client.IdClient == id);
        }

        public Client ObtenirClient(string idStr)
        {
            if (!int.TryParse(idStr, out int id))
                return bdd.Clients.FirstOrDefault(client => client.IdClient == id);
            else
                return null;
        }

        public int AjouterClient(string nom, string mail, string motDePasse)
        {
            Client clientAjoute = bdd.Clients.Add(new Client { Nom = nom, Email = mail, MotDePasse = EncodeMD5(motDePasse) });
            bdd.SaveChanges();
            return clientAjoute.IdClient;
        }

        public Client Authentifier(string nom, string motDePasse)
        {
            string motDePasseHashe = EncodeMD5(motDePasse);
            return bdd.Clients.FirstOrDefault(client => client.Nom == nom && client.MotDePasse == motDePasseHashe);
        }

        public Client AuthentifierMail(string email, string motDePasse)
        {
            string motDePasseHashe = EncodeMD5(motDePasse);
            return bdd.Clients.FirstOrDefault(client => client.Email == email && client.MotDePasse == motDePasseHashe);
        }

        // Livre
        public int AjouterLivre(string titre, DateTime dateParution, int IDauteur)
        {
            Livre livreAjoute = bdd.Livres.Add(new Livre { Titre = titre, DateParution = dateParution, IdAuteur = IDauteur });
            bdd.SaveChanges();
            return livreAjoute.IdLivre;
        }

        public Livre ObtenirLivre(int id)
        {
            return bdd.Livres.FirstOrDefault(livre => livre.IdLivre == id);
        }

        public List<Livre> ObtenirTousLesLivres()
        {
            return bdd.Livres.ToList();
        }

        public List<Livre> ObtenirLivresAuteur(int IDauteur)
        {
            return bdd.Livres.Where(livre => livre.IdAuteur == IDauteur).ToList();
        }

        public bool LivreExiste(int IDlivre)
        {
            if (bdd.Livres.FirstOrDefault(livre => livre.IdLivre == IDlivre) != null)
                return true;
            else
                return false;
        }


        // Emprunt
        public int AjouterEmprunt(int idClient, int idLivre)
        {
            Emprunt empruntAjoute = bdd.Emprunts.Add(new Emprunt { IDClient = idClient, IDLivre = idLivre });
            bdd.SaveChanges();
            return empruntAjoute.IDEmprunt;
        }
        public void ModifierEmprunt(int id, int idClient, int idLivre)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
            if (empruntTrouve != null)
            {
                empruntTrouve.IDClient = idClient;
                empruntTrouve.IDLivre = idLivre;
                bdd.SaveChanges();
            }
        }

        public void RetournerEmprunt(int id)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
            if (empruntTrouve != null)
            {
                bdd.Emprunts.Remove(empruntTrouve);
                bdd.SaveChanges();
            }
        }

        public Emprunt ObtenirEmprunt(int id)
        {
            return bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
        }

        public List<Emprunt> ObtenirEmpruntsClient(int IDclient)
        {
            return bdd.Emprunts.Where(emprunt => emprunt.IDClient == IDclient).ToList();
        }

        public List<Emprunt> ObtenirEmpruntsLivre(int IDlivre)
        {
            return bdd.Emprunts.Where(emprunt => emprunt.IDLivre == IDlivre).ToList();
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "e_bibli" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}