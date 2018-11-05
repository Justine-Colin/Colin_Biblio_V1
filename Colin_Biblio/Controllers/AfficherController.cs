using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colin_Biblio.ViewModels;


namespace Colin_Biblio.Controllers
{
    public class AfficherController : Controller
    {
        // GET: Afficher
        // Liste livres + auteurs
        public ActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();
            return View(vm);
        }

        // GET: Afficher/Auteurs
        // Liste auteurs
        public ActionResult Auteurs()
        {
            AuteursViewModel vm = new AuteursViewModel();
            return View(vm);
        }

        // GET: Afficher/Auteur?IDauteur=IdAuteur
        // Aauteur + livres auteur
        public ActionResult Auteur(int IDauteur)
        {
            if (IDauteur != 0)
            {
                AuteurViewModel vm = new AuteurViewModel(IDauteur);
                if (vm.auteurExiste)
                    return View(vm);
                else
                    return View("Erreur");
            }
            else
                return View("Erreur");
        }

        // GET: Afficher/Livre?IDlivre=IdLivre
        // Livre + auteur + client
        public ActionResult Livre(int IDlivre)
        {
            LivreViewModel vm = new LivreViewModel(IDlivre);
            if (vm.LivreExiste)
                return View(vm);
            else
                return View("Erreur");
        }
    }
}