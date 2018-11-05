using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Colin_Biblio.Models;
using System.Data.Entity;
using Moq;

namespace Colin_Biblio.Tests
{
    [TestClass]
    public class DalTests
    {
        private Dal dal;
        private BddContext Bdd = new BddContext();

        [TestInitialize]
        public void Init_AvantChaqueTest()
        {
            dal = new Dal();
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(Bdd);            
        }

        [TestCleanup]
        public void ApresChaqueTest()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void CreerAuteur_AvecUnNouvelAuteur_ObtientTousLesAuteursRenvoitBienLAuteur()
        {
            dal.AjouterAuteur("Hugo", "Victor");
            List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

            Assert.IsNotNull(auteurs);
            Assert.AreEqual(1, auteurs.Count);
            Assert.AreEqual("Hugo", auteurs[0].Nom);
            Assert.AreEqual("Victor", auteurs[0].Prenom);
        }

        [TestMethod]
        public void ModifierAuteur_CreationDUnNouvelAuteurEtChangementNomEtPrenom_LaModificationEstCorrecteApresRechargement()
        {
            dal.AjouterAuteur("Hugo", "Victor");
            dal.ModifierAuteur(1, "Shakespeare", "William");
            List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

            Assert.IsNotNull(auteurs);
            Assert.AreEqual(1, auteurs.Count);
            Assert.AreEqual("Shakespeare", auteurs[0].Nom);
            Assert.AreEqual("William", auteurs[0].Prenom);
        }

        [TestMethod]
        public void AuteurExiste_AvecCreationDunAuteur_RenvoiQuilExiste()
        {
            dal.AjouterAuteur("Hugo", "Victor");
            Assert.IsTrue(dal.AuteurExiste("Hugo"));
            Assert.IsFalse(dal.AuteurExiste("Shakespeare"));
        }

        [TestMethod]
        public void AuteurExiste_AvecAuteurInexistant_RenvoiQuilExiste()
        {
            Assert.IsFalse(dal.AuteurExiste("Tolkien"));
        }

        [TestMethod]
        public void ObtenirClient_ClientInexistant_RetourneNull()
        {
            Client clientInexistant = dal.ObtenirClient(15);
            Assert.IsNull(clientInexistant);
        }

        [TestMethod]
        public void ObtenirClient_IdNonNumerique_RetourneNull()
        {
            Client clientBadID = dal.ObtenirClient("abc");
            Assert.IsNull(clientBadID);
        }

        [TestMethod]
        public void AjouterClient_NouveauClientEtRecuperation_LeClientEstBienRecupere()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientOK = dal.ObtenirClient(1);

            Assert.IsNotNull(clientOK);
            Assert.AreEqual("Adans", clientOK.Nom);
            Assert.AreEqual("Adans@student.hel.be", clientOK.Email);
        }

        [TestMethod]
        public void Authentifier_LoginMdpOk_AuthentificationOK()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientOK = dal.Authentifier("Adans", "1234");

            Assert.IsNotNull(clientOK);
            Assert.AreEqual("Adans", clientOK.Nom);
            Assert.AreEqual("Adans@student.hel.be", clientOK.Email);
        }

        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.Authentifier("Adans", "4567");

            Assert.IsNull(clientKO);
        }

        [TestMethod]
        public void Authentifier_LoginKoMdpOk_AuthentificationKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.Authentifier("Adam", "1234");

            Assert.IsNull(clientKO);
        }

        [TestMethod]
        public void Authentifier_LoginMdpKo_AuthentificationKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.Authentifier("Adam", "4567");

            Assert.IsNull(clientKO);
        }

        [TestMethod]
        public void Authentifier_LoginMdpOk_AuthentificationMailOK()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientOK = dal.AuthentifierMail("Adans@student.hel.be", "1234");

            Assert.IsNotNull(clientOK);
            Assert.AreEqual("Adans", clientOK.Nom);
            Assert.AreEqual("Adans@student.hel.be", clientOK.Email);
        }

        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationMailKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.AuthentifierMail("Adans@student.hel.be", "4567");

            Assert.IsNull(clientKO);
        }

        [TestMethod]
        public void Authentifier_LoginKoMdpOk_AuthentificationMailKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.Authentifier("Adam@student.hel.be", "1234");

            Assert.IsNull(clientKO);
        }

        [TestMethod]
        public void Authentifier_LoginMdpKo_AuthentificationMailKO()
        {
            dal.AjouterClient("Adans", "Adans@student.hel.be", "1234");
            Client clientKO = dal.Authentifier("Adam@student.hel.be", "4567");

            Assert.IsNull(clientKO);
        }

        //Dates de parution fausse car format datetime va de 01/01/1753 à 31/12/9999
        [TestMethod]
        public void AjouterLivre_NouveauLivreEtRecuperation_LeLivreEstBienRecupere()
        {
            int idAuteur = dal.AjouterAuteur("Shakespeare", "William");
            dal.AjouterLivre("Le Songe d'une nuit d'été", DateTime.Parse("08/10/1900"), idAuteur);
            Livre livreTrouve = dal.ObtenirLivre(1);

            DateTime dateParution = DateTime.Parse("08/10/1900");
            Assert.IsNotNull(livreTrouve);
            Assert.AreEqual("Le Songe d'une nuit d'été", livreTrouve.Titre);
            Assert.AreEqual(dateParution, livreTrouve.DateParution);
            Assert.AreEqual(idAuteur, livreTrouve.IdAuteur);
        }

        [TestMethod]
        public void AjouterLivres_ObtenirTousLesLivres_IlsSontBienRecupere()
        {
            int idAuteur = dal.AjouterAuteur("Shakespeare", "William");
            dal.AjouterLivre("Le Songe d'une nuit d'été", DateTime.Parse("08/10/1900"), idAuteur);
            dal.AjouterLivre("La Tragique Histoire d'Hamlet, prince de Danemark", DateTime.Parse("01/01/1903"), idAuteur);
            List<Livre> tousLivres = dal.ObtenirTousLesLivres();

            Assert.IsNotNull(tousLivres);
            Assert.AreEqual(tousLivres.Count, 2);
            Assert.AreEqual("Le Songe d'une nuit d'été", tousLivres[0].Titre);
            Assert.AreEqual("La Tragique Histoire d'Hamlet, prince de Danemark", tousLivres[1].Titre);
        }
    }
}
