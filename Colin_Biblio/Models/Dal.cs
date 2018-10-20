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

        //Auteur
        public List<Auteur> ObtientTousLesAuteurs()
        {
            return bdd.Auteurs.ToList();
        }

        public void CreerAuteur(string nom, string prenom)
        {
            bdd.Auteurs.Add(new Auteur { Nom = nom, Prenom = prenom });
        }

        public int AjouterAuteur(string nom, string prenom)
        {
            Auteur Ajout = bdd.Auteurs.Add(new Auteur { Nom = nom, Prenom = prenom });
            bdd.SaveChanges();
            return Ajout.IdAuteur;
        }

        public Auteur ObtenirAuteur(int id)
        {
            return bdd.Auteurs.FirstOrDefault(Auteur => Auteur.IdAuteur == id);
        }

        public void ModifierAuteur(int id, string nom, string prenom)
        {
            Auteur AuteurTrouve = bdd.Auteurs.FirstOrDefault(Auteur => Auteur.IdAuteur == id);
            if (AuteurTrouve != null)
            {
                AuteurTrouve.Nom = nom;
                AuteurTrouve.Prenom = prenom;
                bdd.SaveChanges();
            }
        }

        public bool AuteurExiste(string nom)
        {
            return bdd.Auteurs.Any(Auteur => string.Compare(Auteur.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        //Client
        public Client ObtenirClient(int id)
        {
            return bdd.Clients.FirstOrDefault(c => c.IdClient == id);
        }

        public Client ObtenirClient(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return ObtenirClient(id);
            return null;
        }

        public int AjouterClient(string nom, string email, string motdepasse)
        {
            string motDePasseEncode = EncodeMD5(motdepasse);
            Client client = new Client { Nom = nom, Email = email, MotDePasse = motDePasseEncode };
            bdd.Clients.Add(client);
            bdd.SaveChanges();
            return client.IdClient;
        }

        public Client Authentifier(string nom, string motdepasse)
        {
            string motDePasseEncode = EncodeMD5(motdepasse);
            return bdd.Clients.FirstOrDefault(c => c.Nom == nom && c.MotDePasse == motDePasseEncode);
        }
        public Client AuthentifierMail(string email, string motdepasse)
        {
            string motDePasseEncode = EncodeMD5(motdepasse);
            return bdd.Clients.FirstOrDefault(c => c.Email == email && c.MotDePasse == motDePasseEncode);
        }


        //Livre
        public int AjouterLivre(string titre, string dateparution, string auteur)
        {
            Livre livre = new Livre { Titre = titre, DateParution = dateparution, Auteur = auteur };
            bdd.Livres.Add(livre);
            bdd.SaveChanges();
            return livre.IdLivre;
        }

        public Livre ObtenirLivre(int id)
        {
            return bdd.Livres.FirstOrDefault(l => l.IdLivre == id);
        }

        // Emprunt
        public int AjouterEmprunt(int idClient, int idLivre, DateTime dateEmprunt)
        {
            Emprunt empruntAjoute = bdd.Emprunts.Add(new Emprunt { IDClient = idClient, IDLivre = idLivre, DateEmprunt = dateEmprunt });
            bdd.SaveChanges();
            return empruntAjoute.IDEmprunt;
        }
        public void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt, DateTime dateRetour)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
            if (empruntTrouve != null)
            {
                empruntTrouve.IDEmprunt = idClient;
                empruntTrouve.IDLivre = idLivre;
                empruntTrouve.DateEmprunt = dateEmprunt;
                empruntTrouve.DateRetour = dateRetour;
                bdd.SaveChanges();
            }
        }
        public void ModifierEmprunt(int id, int idClient, int idLivre, DateTime dateEmprunt)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
            if (empruntTrouve != null)
            {
                empruntTrouve.IDClient = idClient;
                empruntTrouve.IDLivre = idLivre;
                empruntTrouve.DateEmprunt = dateEmprunt;
                bdd.SaveChanges();
            }
        }
        public void RetournerEmprunt(int id)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
            if (empruntTrouve != null)
            {
                empruntTrouve.DateRetour = DateTime.Now;
                bdd.SaveChanges();
            }
        }
        public Emprunt ObtenirEmprunt(int id)
        {
            return bdd.Emprunts.FirstOrDefault(emprunt => emprunt.IDEmprunt == id);
        }

        //Autre
        public void Dispose()
        {
            bdd.Dispose();

        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Biblio" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    } 
}