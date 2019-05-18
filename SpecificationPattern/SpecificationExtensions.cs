using System;

namespace SpecificationPattern
{
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Checks if a specification is valid and adds a failure reason if it is not satisfied
        /// </summary>
        /// <typeparam name="T">The type that the specification can verify</typeparam>
        /// <param name="specification">The specification</param>
        /// <param name="isSatisfied">Is the specification has been satisfied</param>
        /// <param name="failureReason">The failure reason to be added</param>
        /// <returns>
        ///   <c>true</c> if the specification is satisfied; otherwise, <c>false</c>.
        /// </returns>
        public static bool Check<T>(this ISpecification<T> specification, bool isSatisfied, string failureReason)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            if (failureReason == null)
                throw new ArgumentNullException(nameof(failureReason));

            if (!isSatisfied && specification.FailureReasons != null)
            {
                specification.FailureReasons.Add(failureReason);
            }

            return isSatisfied;
        }

        /// <summary>
        /// Checks if a specification is valid and adds a failure reason for a specific failure type if it is not satisfied
        /// </summary>
        /// <typeparam name="TTarget">The type that the specification can verify</typeparam>
        /// <typeparam name="TKey">The type that will identify the failure reason</typeparam>
        /// <param name="specification">The specification</param>
        /// <param name="isSatisfied">Is the specification has been satisfied</param>
        /// <param name="failureType">The failure type that maps to the failure reason</param>
        /// <returns>
        ///   <c>true</c> if the specification is satisfied; otherwise, <c>false</c>.
        /// </returns>
        public static bool Check<TTarget, TKey>(this ISpecification<TTarget, TKey> specification, bool isSatisfied,
            TKey failureType) where TKey : Enum
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            if (!isSatisfied && specification.FailureReasons != null)
            {
                specification.FailureReasons[failureType] = failureType.GetDescription();
            }

            return isSatisfied;
        }
    }
}
