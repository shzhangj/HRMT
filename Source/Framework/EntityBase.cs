using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OppenIT.Core.Framework
{
    public abstract class EntityBase<TEntity>
    {
        public TEntity _entityObject;
        private EntityDBContext _entityDBContext = null;
        protected EntityDBContext EntityDBContext 
        {
            get
            {
                if (_entityDBContext == null)
                {
                    _entityDBContext = EntityInitializer.GetDBContext(this.DBPairName);
                    if (_entityDBContext == null)
                        throw new Exception("dbpair:" + this.DBPairName + " Not Initializing!");
                }
                return _entityDBContext;
            }
        }
        public void SubmitModifies()
        {
            this.SubmitModifies(null);
        }
        public void SubmitModifies(object obj)
        {
            this.UpdateTrigger(obj);
            this.EntityDBContext.Update(_entityObject);
        }
        public void Delete()
        {
            this.EntityDBContext.Delete(_entityObject);
        }
        public void Insert()
        {
            this.EntityDBContext.Insert(_entityObject);
        }
        public TKey Insert<TKey>()
        {
            return this.EntityDBContext.Insert<TKey>(_entityObject);
        }
        protected virtual void UpdateTrigger(object obj)
        {
            
        }
        protected virtual string DBPairName
        {
            get { return EntityInitializer.DefaultDBPair; }
        }
    }
}
