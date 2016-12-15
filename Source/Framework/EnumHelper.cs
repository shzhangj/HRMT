using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OppenIT.Core.Framework
{
    public class EnumHelper
    {
        public class EnumFieldInfo
        {
            string _value;
            public string Value
            {
                get { return _value; }
            }
            object _rawValue;
            public object RawValue
            {
                get { return _rawValue; }
            }
            string _description;
            public string Description
            {
                get { return _description; }
            }
            public EnumFieldInfo(string value, object rawValue, string description)
            {
                _value = value;
                _rawValue = rawValue;
                _description = description;
            }
        }
        public static string GetEnumDescription(object enm)
        {
            //TODO: 通过资源文件取
            string desc = enm.ToString();
            FieldInfo fi = enm.GetType().GetField(enm.ToString());
            if (fi != null)
            {
                EnumFieldAttribute enumFieldAtt = Attribute.GetCustomAttribute(fi, typeof(EnumFieldAttribute), false) as EnumFieldAttribute;
                if (enumFieldAtt != null)
                    desc = enumFieldAtt.Description;
            }
            return desc;
        }
        public static object GetEnumRawValue(Enum enm)
        {
            object rawValue = null;
            FieldInfo fi = enm.GetType().GetField(enm.ToString());
            if (fi != null)
            {
                EnumFieldAttribute enumFieldAtt = Attribute.GetCustomAttribute(fi, typeof(EnumFieldAttribute), false) as EnumFieldAttribute;
                if (enumFieldAtt != null)
                    rawValue = enumFieldAtt.RawValue;
            }
            return rawValue;
        }
        public static Enum GetEnumByRawValue(Type enmType,object rawValue)
        {
            FieldInfo[] fiList = enmType.GetFields();
            foreach (FieldInfo fi in fiList)
            {
                EnumFieldAttribute fiAtt = Attribute.GetCustomAttribute(fi, typeof(EnumFieldAttribute), false) as EnumFieldAttribute;
                if (fiAtt != null && fiAtt.RawValue.ToString() == rawValue.ToString())
                    return (Enum)Enum.Parse(enmType, fi.Name);
            }
            return null;
        }
        public static Enum GetEnumByValue(Type enmType, string value)
        {
            return (Enum)Enum.Parse(enmType, value);
        }
        public static List<EnumFieldInfo> GetEnumerator(Type enmType)
        {
            List<EnumFieldInfo> attList = new List<EnumFieldInfo>();
            FieldInfo[] fiList = enmType.GetFields();
            foreach (FieldInfo fi in fiList)
            {
                EnumFieldAttribute fiAtt = Attribute.GetCustomAttribute(fi, typeof(EnumFieldAttribute), false) as EnumFieldAttribute;
                if (fiAtt != null)
                    attList.Add(new EnumFieldInfo(fi.Name, fiAtt.RawValue, fiAtt.Description));
            }
            return attList;
        }
    }
}
