using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppenIT.Core.Framework
{
    public static class EntityInitializer
    {
        private static object _sync_obj = new object();
        static Dictionary<string, EntityDBContext> _entityDBHelperDict = null;
        public static string DefaultDBPair;
        static EntityInitializer()
        {
            _entityDBHelperDict = new Dictionary<string, EntityDBContext>();
        }
        public static void RegisterDBContext(string dbPair, string providerName, string connectionString, bool isDefault)
        {
            lock (_sync_obj)
            {
                _entityDBHelperDict.Add(dbPair, new EntityDBContext(providerName, connectionString));
                if (isDefault)
                    DefaultDBPair = dbPair;
            }
        }
        public static void RegisterDBContext(string dbPair, string providerName, string connectionString)
        {
            RegisterDBContext(dbPair, providerName, connectionString, true);
        }
        public static EntityDBContext GetDBContext(string dbPair)
        {
            if (_entityDBHelperDict.ContainsKey(dbPair))
                return _entityDBHelperDict[dbPair];
            return null;
        }
        public static void ReleaseDBContext()
        {
            lock (_sync_obj)
            {
                foreach (EntityDBContext dbContext in _entityDBHelperDict.Values)
                    dbContext.Dispose();
            }
        }
    }
}
