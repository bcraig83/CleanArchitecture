using Domain.Exceptions;
using Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class AdAccountTests
    {
        [Fact]
        public void For_ShouldBuildObjectWithCorrectDomainAndName()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            account.Domain.ShouldBe("SSW");
            account.Name.ShouldBe("Jason");
        }

        [Fact]
        public void ToString_ShouldResultInCorrectString()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            var result = account.ToString();

            result.ShouldBe(accountString);
        }

        [Fact]
        public void ImplicitConversionToString_ShouldResultInCorrectString()
        {
            const string accountString = "SSW\\Jason";

            var account = AdAccount.For(accountString);

            string result = account;

            result.ShouldBe(accountString);
        }

        [Fact]
        public void ExplicitConversionFromString_ShouldSetDomainAndName_WhenUsingValidAdAccount()
        {
            const string accountString = "SSW\\Jason";

            var account = (AdAccount)accountString;

            account.Domain.ShouldBe("SSW");
            account.Name.ShouldBe("Jason");
        }

        [Fact]
        public void ExplicitConversionFromString_ShouldThrowAdAccountInvalidException_WhenUsingInvalidAdAccount()
        {
            Should.Throw<AdAccountInvalidException>
                (() => (AdAccount)"SSWJason");
        }
    }
}