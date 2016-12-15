using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppenIT.Core.Framework
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
    public sealed class EnumFieldAttribute : Attribute
    {
        public EnumFieldAttribute()
        {
            
        }
        private object _rawValue;
        public object RawValue
        {
            get { return _rawValue; }
            set { _rawValue = value;}
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
