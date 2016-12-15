using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OppenIT.Core.Framework
{
    public enum BoolEnum
    {
        [EnumField(Description = "是", RawValue = 1)]
        True,
        [EnumField(Description = "否", RawValue = 0)]
        False
    }
}
