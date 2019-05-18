using System;
using System.Linq;
using SpecificationPattern.Sample.Domain.Entities;
using SpecificationPattern.Sample.Finance.Specifications;
using Xunit;

namespace SpecificationPattern.Tests.Finance.Specifications
{
    public class PositiveAccountBalanceSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_ShouldThrowException_WhenBankAccountIsNull()
        {
            ISpecification<BankAccount> specification = new PositiveAccountBalanceSpecification();

            Assert.Throws<ArgumentNullException>(() => specification.IsSatisfiedBy(null));
        }

        [Fact]
        public void IsSatisfiedBy_ShouldBeSatisfied_WhenBalanceIsPositive()
        {
            var bankAccount = new BankAccount(1m);

            ISpecification<BankAccount> specification = new PositiveAccountBalanceSpecification();
            bool isSatisfied = specification.IsSatisfiedBy(bankAccount);

            Assert.True(isSatisfied);
            Assert.Empty(specification.FailureReasons);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsSatisfiedBy_ShouldNotBeSatisfied_WhenBalanceIsNotPositive(double balance)
        {
            var bankAccount = new BankAccount((decimal)balance);

            ISpecification<BankAccount> specification = new PositiveAccountBalanceSpecification();
            bool isSatisfied = specification.IsSatisfiedBy(bankAccount);

            Assert.False(isSatisfied);

            Assert.Equal("Account must have a positive balance for this operation",
                specification.FailureReasons.Single());
        }
    }
}
