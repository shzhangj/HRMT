using OppenIT.Core.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OppenIT.Core.Framework
{
    public class EntityDBContext : DbHelperBase
    {
        public EntityDBContext(string dbProviderName, string connectionString)
            : base(dbProviderName, connectionString)
        {
            
        }
    }
}
