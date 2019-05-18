using System;
using System.Linq;
using SpecificationPattern.Sample.Domain.Entities;

namespace SpecificationPattern.Sample.Finance.Specifications
{
    public sealed class CustomerApprovedForLoanSpecification : KeyedSpecificationBase<Customer, LoanFailureReason>
    {
        private readonly ISpecification<string> ssnSpecification;
        private readonly ISpecification<int> creditScoreSpecification;
        private readonly ISpecification<BankAccount> accountBalanceSpecification;

        public CustomerApprovedForLoanSpecification(
            ISpecification<string> ssnSpecification,
            ISpecification<int> creditScoreSpecification,
            ISpecification<BankAccount> accountBalanceSpecification)
        {
            this.ssnSpecification = ssnSpecification ?? throw new ArgumentNullException(nameof(ssnSpecification));
            this.creditScoreSpecification = creditScoreSpecification ?? throw new ArgumentNullException(nameof(creditScoreSpecification));
            this.accountBalanceSpecification = accountBalanceSpecification ?? throw new ArgumentNullException(nameof(accountBalanceSpecification));
        }

        public CustomerApprovedForLoanSpecification() :this(
            new SocialSecurityNumberSpecification(),
            new HighCreditScoreSpecification(),
            new PositiveAccountBalanceSpecification()) { }

        public override bool IsSatisfiedBy(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            bool[] isSatisfied = new bool[3]
            {
                this.Check(this.ssnSpecification.IsSatisfiedBy(customer.Ssn), 
                    LoanFailureReason.InvalidSsn),

                this.Check(this.creditScoreSpecification.IsSatisfiedBy(customer.CreditScore),
                    LoanFailureReason.LowCredit),

                this.Check(this.accountBalanceSpecification.IsSatisfiedBy(customer.BankAccount),
                    LoanFailureReason.NegativeBalance)
            };

            return isSatisfied.All(x => x);
        }
    }
}
