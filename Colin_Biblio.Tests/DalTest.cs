using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Colin_Biblio.Models;
using System.Data.Entity;

namespace Colin_Biblio.Tests
{
    [TestClass]
    public class DalTests
    {
        private Dal dal;

        [TestInitialize]
        public void Init_AvantChaqueTest()
        {
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
            dal = new Dal();
        }

        [TestCleanup]
        public void ApresChaqueTest()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void CreerAuteur_AvecUnNouvelAuteur_ObtientTousLesAuteursRenvoitBienLAuteur()
        {
            dal.AjouterAuteur("Riordan", "Rick");
            List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

            Assert.IsNotNull(auteurs);
            Assert.AreEqual(1, auteurs.Count);
            Assert.AreEqual("Riordan", auteurs[0].Nom);
            Assert.AreEqual("Rick", auteurs[0].Prenom);
        }

        [TestMethod]
        public void ModifierAuteur_CreationDUnNouvelAuteurEtChangementNomEtPrenom_LaModificationEstCorrecteApresRechargement()
        {
            dal.AjouterAuteur("Riordan", "Rick");
            dal.ModifierAuteur(1, "Rowling", "J.K.");
            List<Auteur> auteurs = dal.ObtientTousLesAuteurs();

            Assert.IsNotNull(auteurs);
            Assert.AreEqual(1, auteurs.Count);
            Assert.AreEqual("Rowling", auteurs[0].Nom);
            Assert.AreEqual("J.K.", auteurs[0].Prenom);
        }

        [TestMethod]
        public void AuteurExiste_AvecCreationDunAuteur_RenvoiQuilExiste()
        {
            dal.AjouterAuteur("Riordan", "Rick");
            Assert.IsTrue(dal.AuteurExiste("Riordan"));
            Assert.IsFalse(dal.AuteurExiste("Rowling"));
        }

        [TestMethod]
        public void ObtenirClient_ClientInexistant_RetourneNull()
        {
            Client client = dal.ObtenirClient(15);
            Assert.IsNull(client);
        }

        [TestMethod]
        public void ObtenirClient_IdNonNumerique_RetourneNull()
        {
            Client client = dal.ObtenirClient("abc");
            Assert.IsNull(client);
        }

        [TestMethod]
        public void AjouterClient_NouveauClientEtRecuperation_LeClientEstBienRecupere()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.ObtenirClient(1);

            Assert.IsNotNull(client);
            Assert.AreEqual("Colin", client.Nom);
            Assert.AreEqual("Colin@student.hel.be", client.Email);
        }

        [TestMethod]
        public void Authentifier_LoginMdpOk_AuthentificationOK()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Colin", "1234");

            Assert.IsNotNull(client);
            Assert.AreEqual("Colin", client.Nom);
            Assert.AreEqual("Colin@student.hel.be", client.Email);
        }

        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Colin", "4567");

            Assert.IsNull(client);
        }

        [TestMethod]
        public void Authentifier_LoginKoMdpOk_AuthentificationKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Quollain", "1234");

            Assert.IsNull(client);
        }

        [TestMethod]
        public void Authentifier_LoginMdpKo_AuthentificationKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Quollain", "4567");

            Assert.IsNull(client);
        }

        [TestMethod]
        public void Authentifier_LoginMdpOk_AuthentificationMailOK()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.AuthentifierMail("Colin@student.hel.be", "1234");

            Assert.IsNotNull(client);
            Assert.AreEqual("Colin", client.Nom);
            Assert.AreEqual("Colin@student.hel.be", client.Email);
        }

        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationMailKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.AuthentifierMail("Colin@student.hel.be", "4567");

            Assert.IsNull(client);
        }

        [TestMethod]
        public void Authentifier_LoginKoMdpOk_AuthentificationMailKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Quollain@student.hel.be", "1234");

            Assert.IsNull(client);
        }

        [TestMethod]
        public void Authentifier_LoginMdpKo_AuthentificationMailKO()
        {
            dal.AjouterClient("Colin", "Colin@student.hel.be", "1234");
            Client client = dal.Authentifier("Quollain@student.hel.be", "4567");

            Assert.IsNull(client);
        }


        [TestMethod]
        public void AjouterLivre__NouveauLivreEtRecuperation_LeLivreEstBienRecupere()
        {
            dal.AjouterLivre("Harry Potter et la chambre des secrets", "12/02/1950", "Rowling");
            Livre livre = dal.ObtenirLivre(1);
            Assert.IsNotNull(livre);
            Assert.AreEqual("Harry Potter et la chambre des secrets", livre.Titre);
        }
    }
}
