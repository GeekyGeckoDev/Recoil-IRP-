using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class SecurityEventRepository
    {
        private readonly RecoilDbContext _context;

        public SecurityEventRepository(RecoilDbContext context)
        {
            _context = context;
        }


    }
}
