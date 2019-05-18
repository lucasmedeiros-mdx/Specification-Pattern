namespace SpecificationPattern.Sample.Domain.Entities
{
    public class Customer
    {
        public Customer(string ssn, string name, int creditScore, BankAccount bankAccount)
        {
            Ssn = ssn ?? throw new System.ArgumentNullException(nameof(ssn));
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            BankAccount = bankAccount ?? throw new System.ArgumentNullException(nameof(bankAccount));
            CreditScore = creditScore;
        }

        public BankAccount BankAccount { get; }
        public int CreditScore { get; }
        public string Name { get; }
        public string Ssn { get; }
    }
}
