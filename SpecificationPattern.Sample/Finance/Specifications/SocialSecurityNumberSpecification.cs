using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("SpecificationPattern.Tests")]
namespace SpecificationPattern.Sample.Finance.Specifications
{
    internal sealed class SocialSecurityNumberSpecification : SpecificationBase<string>
    {
        public override bool IsSatisfiedBy(string ssn)
        {
            return ssn == null ? false : Regex.IsMatch(ssn, @"^\d{3}-\d{2}-\d{4}$");
        }
    }
}
