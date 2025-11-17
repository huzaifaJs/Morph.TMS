using Abp.Events.Bus;
using Morpho.Domain.Entities.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Events
{
    public class PolicyViolatedEvent : EventData
    {
        public PolicyViolation Violation { get; }

        public PolicyViolatedEvent(PolicyViolation violation)
        {
            Violation = violation;
        }
    }
}
