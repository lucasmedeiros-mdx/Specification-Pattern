using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SpecificationPattern.Tests")]
namespace SpecificationPattern.Sample.Finance.Specifications
{
    internal sealed class HighCreditScoreSpecification : SpecificationBase<int>
    {
        public override bool IsSatisfiedBy(int creditScore)
        {
            return creditScore > 700;
        }
    }
}
