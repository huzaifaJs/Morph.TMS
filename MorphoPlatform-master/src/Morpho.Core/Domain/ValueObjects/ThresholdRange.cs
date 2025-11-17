using Abp.Domain.Values;
using System;
using System.Collections.Generic;

namespace Morpho.Domain.ValueObjects
{
    public class ThresholdRange : ValueObject
    {
        public decimal? Min { get; private set; }
        public decimal? Max { get; private set; }
        public decimal? MaxVariation { get; private set; }

        protected ThresholdRange() { }

        public ThresholdRange(decimal? min, decimal? max, decimal? maxVariation = null)
        {
            Min = min;
            Max = max;
            MaxVariation = maxVariation;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Min;
            yield return Max;
            yield return MaxVariation;
        }
    }
}
