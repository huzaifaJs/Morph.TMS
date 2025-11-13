using System;
using Morpho.Domain.Enums;

namespace Morpho.Domain.Common;

/// <summary>
///  Represents a physical address.
///   Contains properties for address lines, city, state/province, postal code, country code, and a flag indicating if it's the primary address.
/// </summary>
public class Address
{
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? StateProvince { get; set; }
    public string? PostalCode { get; set; }
    public string CountryCode { get; set; }
    public bool IsPrimary { get; set; }
}