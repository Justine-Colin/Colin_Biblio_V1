using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Colin_Biblio.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
    }
}