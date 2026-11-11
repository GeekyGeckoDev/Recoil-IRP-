using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class RecoilDbContextFactory : IDesignTimeDbContextFactory<RecoilDbContext>
    {
        public RecoilDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecoilDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RecoilDb;Encrypt = False; Integrated Security =True;TrustServerCertificate=True; Trusted_Connection = True");
            return new RecoilDbContext(optionsBuilder.Options);
        }
    }
}
