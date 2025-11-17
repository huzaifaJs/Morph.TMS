using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Enums
{
    public enum PolicyConditionType
    {
        Range = 0,        // min <= value <= max
        GreaterThan = 1,
        LessThan = 2
    }
}
