using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class CollectorConfigurationRepository
    {
        private readonly RecoilDbContext _context;

        public CollectorConfigurationRepository(RecoilDbContext context)
        {
            _context = context;
        }


    }
}
