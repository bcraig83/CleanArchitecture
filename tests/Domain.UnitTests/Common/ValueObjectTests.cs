using Domain.Common;
using System.Collections.Generic;
using Xunit;

namespace Domain.UnitTests.Common
{
    public class ValueObjectTests
    {
        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameObjectUsedForComparison()
        {
            // Arrange

            // Act

            // Assert
        }

        private class Address : ValueObject
        {
            public string Eircode { get; private set; }

            public string Street { get; private set; }

            public string County { get; private set; }

            private Address()
            {
            }

            public static Address Create(string eircode, string street, string county)
            {
                return new Address
                {
                    Eircode = eircode,
                    Street = street,
                    County = county
                };
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Eircode;
                yield return Street;
                yield return County;
            }
        }
    }
}