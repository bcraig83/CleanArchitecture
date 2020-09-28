using Domain.Common;
using Shouldly;
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
            var itemUnderTest = Address.Create("ABCDEFG", "Sreet 1", "Co. Galway");

            // Act
            var result = itemUnderTest.Equals(itemUnderTest);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenObjectWithSameValuesAreUsedForComparison()
        {
            // Arrange
            var firstAddress = Address.Create("ABC456G", "Some other street", "Co. Cork");
            var secondAddress = Address.Create("ABC456G", "Some other street", "Co. Cork");

            // Act
            var result = firstAddress.Equals(secondAddress);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenNullObjectPassedIn()
        {
            // Arrange
            var itemUnderTest = Address.Create("ACEGIKM", "209 Happy Street", "Co. Louth");

            // Act
            var result = itemUnderTest.Equals(null);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentObjectTypeIsPassedIn()
        {
            // Arrange
            var itemUnderTest = Address.Create("ACEGIKM", "209 Happy Street", "Co. Louth");

            // Act
            var result = itemUnderTest.Equals(new SomeNonValueObject());

            // Assert
            result.ShouldBeFalse();
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

        private class SomeNonValueObject { }
    }
}