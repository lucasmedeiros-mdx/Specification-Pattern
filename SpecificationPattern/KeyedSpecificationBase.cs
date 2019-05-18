using System;
using System.Collections.Generic;

namespace SpecificationPattern
{
    public abstract class KeyedSpecificationBase<TTarget, TKey> : ISpecification<TTarget, TKey> where TKey : Enum
    {
        public KeyedSpecificationBase()
        {
            this.FailureReasons = new Dictionary<TKey, string>();
        }

        public IDictionary<TKey, string> FailureReasons { get; }

        public abstract bool IsSatisfiedBy(TTarget target);
    }
}
