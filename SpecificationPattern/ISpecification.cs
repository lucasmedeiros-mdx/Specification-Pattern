using System;
using System.Collections.Generic;

namespace SpecificationPattern
{
    public interface ISpecification<TTarget>
    {
        /// <summary>
        /// Collection containing the failure reasons.
        /// </summary>
        ICollection<string> FailureReasons { get; }

        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns>
        ///   <c>true</c> if target object satisfies the specification; otherwise, <c>false</c>.
        /// </returns>
        bool IsSatisfiedBy(TTarget target);
    }

    public interface ISpecification<TTarget, TKey> where TKey : Enum
    {
        /// <summary>
        /// Collection of keys and values containing the failure reasons.
        /// </summary>
        IDictionary<TKey, string> FailureReasons { get; }

        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns>
        ///   <c>true</c> if target object satisfies the specification; otherwise, <c>false</c>.
        /// </returns>
        bool IsSatisfiedBy(TTarget target);
    }
}
