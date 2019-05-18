namespace SpecificationPattern.Sample.Domain.Entities
{
    public class BankAccount
    {
        public BankAccount(decimal balance)
        {
            Balance = balance;
        }

        public decimal Balance { get; }
    }
}
