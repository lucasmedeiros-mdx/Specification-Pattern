using System.Collections.Generic;

namespace SpecificationPattern
{
    public abstract class SpecificationBase<TTarget> : ISpecification<TTarget>
    {
        public SpecificationBase()
        {
            this.FailureReasons = new HashSet<string>();
        }

        public ICollection<string> FailureReasons { get; }

        public abstract bool IsSatisfiedBy(TTarget target);
    }
}
