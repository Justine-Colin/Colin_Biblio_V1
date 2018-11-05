using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colin_Biblio.Models;

namespace Colin_Biblio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /*  Routes utilisables :
         *  Add/Auteurs/Nom/Prenom
         *  Add/Clients/Nom/Mail/MDP
         *  Add/Emprunts/NomClient/IdLivre
         *  Add/Livre/Noms/DateParution/IdAuteur
		 */

        public ActionResult Add(string Table, string Entries)
        {
            if (Table != null && Entries != null)
            {
                string[] Tableau_Entries = Entries.Split('/');
                switch (Table)
                {
                    case "Auteur":
                        if (Tableau_Entries.Length == 2)
                        {
                            string nom = "";
                            string prenom = "";
                            string[] Tableau_Noms = Tableau_Entries[0].Split('_');
                            string[] tabprenoms = Tableau_Entries[1].Split('_');
                            foreach (string Tableau_Nom in Tableau_Noms)
                                nom += Tableau_Nom + " ";
                            foreach (string Tableau_Prenom in tabprenoms)
                                prenom += Tableau_Prenom + " ";
                            nom.Remove(nom.Length - 1);
                            prenom.Remove(prenom.Length - 1);
                            Dal dal = new Dal();
                            dal.AjouterAuteur(nom, prenom);
                            dal.Dispose();
                        }
                        else
                            return View("Erreur");
                        break;

                    case "Client":
                        if (Tableau_Entries.Length == 3)
                        {
                            Dal dal = new Dal();
                            dal.AjouterClient(Tableau_Entries[0], Tableau_Entries[1], Tableau_Entries[2]);
                            dal.Dispose();
                        }
                        else
                            return View("Erreur");
                        break;

                    case "Emprunt":
                        if (Tableau_Entries.Length == 3)
                        {
                            if (int.TryParse(Tableau_Entries[0], out int IDClient) && int.TryParse(Tableau_Entries[1], out int IDLivre))
                            {
                                Dal dal = new Dal();
                                dal.AjouterEmprunt(IDClient, IDLivre);
                                dal.Dispose();
                            }
                            else
                                return View("Erreur");
                        }
                        else
                            return View("Erreur");
                        break;

                    case "Livre":
                        if (Tableau_Entries.Length == 3)
                        {
                            if (DateTime.TryParse(Tableau_Entries[1], out DateTime DateParution) && int.TryParse(Tableau_Entries[2], out int IDauteur))
                            {
                                string nom = "";
                                string[] Tableau_Noms = Tableau_Entries[0].Split('_');
                                foreach (string Tableau_Nom in Tableau_Noms)
                                    nom += Tableau_Nom + " ";
                                Dal dal = new Dal();
                                dal.AjouterLivre(nom, DateParution, IDauteur);
                                dal.Dispose();
                            }
                            else
                            {
                                return View("Erreur");
                            }
                        }
                        else
                            return View("Erreur");
                        break;

                    default: return View("Erreur");
                }
                ViewBag.Message = "L'item a bien été ajouté.";
                return View("Index");
            }
            else
                return View("Erreur");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}