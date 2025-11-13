using System;

namespace Morpho.Domain.ValueObjects
{
    /// <summary>
    /// Value object representing a physical address.
    /// </summary>
    public class Address : IEquatable<Address>
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        protected Address()
        {
            // For EF Core
        }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be null or empty", nameof(street));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be null or empty", nameof(city));

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be null or empty", nameof(country));

            Street = street.Trim();
            City = city.Trim();
            State = state?.Trim();
            PostalCode = postalCode?.Trim();
            Country = country.Trim();
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            return string.Equals(Street, other.Street, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(City, other.City, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(State, other.State, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(PostalCode, other.PostalCode, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Country, other.Country, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Street?.ToLowerInvariant(),
                City?.ToLowerInvariant(),
                State?.ToLowerInvariant(),
                PostalCode?.ToLowerInvariant(),
                Country?.ToLowerInvariant());
        }

        public override string ToString()
        {
            var parts = new[] { Street, City, State, PostalCode, Country };
            return string.Join(", ", Array.FindAll(parts, s => !string.IsNullOrWhiteSpace(s)));
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }
    }
}