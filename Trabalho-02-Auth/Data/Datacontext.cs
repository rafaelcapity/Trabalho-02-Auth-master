using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Trabalho_02_Auth.Models;

namespace Trabalho_02_Auth.Data
{
    public class Datacontext : DbContext
    {
        


        public DbSet<User> Users{ get; set; }

        public DbSet<Fornecedor> Fornecedors{ get; set; }

        public DbSet<Produto> Produtos{ get; set; }
    }
}