using System;
using System.Runtime.CompilerServices;
using SpecificationPattern.Sample.Domain.Entities;

[assembly: InternalsVisibleTo("SpecificationPattern.Tests")]
namespace SpecificationPattern.Sample.Finance.Specifications
{
    internal sealed class PositiveAccountBalanceSpecification : SpecificationBase<BankAccount>
    {
        public override bool IsSatisfiedBy(BankAccount bankAccount)
        {
            if (bankAccount == null)
                throw new ArgumentNullException(nameof(bankAccount));

            return this.Check(bankAccount.Balance > 0, "Account must have a positive balance for this operation");
        }
    }
}
