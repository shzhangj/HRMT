using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppenIT.Core.Framework
{
    public enum IsActiveEnum
    {
        [EnumField(Description="在用",RawValue=1)]
        Active,
        [EnumField(Description = "禁用", RawValue = 0)]
        Disabled
    }
}
