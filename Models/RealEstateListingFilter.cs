using System.Text.Json.Serialization;

public class RealEstateListingFilter
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("zip_code")]
    public string? ZipCode { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("province")]
    public string? Province { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("house_number")]
    public string? HouseNumber { get; set; }

    [JsonPropertyName("additional_house_number")]
    public string? AdditionalHouseNumber { get; set; }

    [JsonPropertyName("energy_label")]
    public string? EnergyLabel { get; set; }

    [JsonPropertyName("max_bedrooms")]
    public int? MaxBedrooms { get; set; }

    [JsonPropertyName("min_bedrooms")]
    public int? MinBedrooms { get; set; }

    [JsonPropertyName("max_rooms")]
    public int? MaxRooms { get; set; }

    [JsonPropertyName("min_rooms")]
    public int? MinRooms { get; set; }

    [JsonPropertyName("max_bathrooms")]
    public int? MaxBathrooms { get; set; }

    [JsonPropertyName("min_bathrooms")]
    public int? MinBathrooms { get; set; }

    [JsonPropertyName("is_apartment")]
    public bool? IsApartment { get; set; }

    [JsonPropertyName("is_detached_house")]
    public bool? IsDetachedHouse { get; set; }

    [JsonPropertyName("is_available_to_buy")]
    public bool? IsAvailableToBuy { get; set; }

    [JsonPropertyName("is_available_to_rent")]
    public bool? IsAvailableToRent { get; set; }

    [JsonPropertyName("max_price")]
    public decimal? MaxPrice { get; set; }

    [JsonPropertyName("min_price")]
    public decimal? MinPrice { get; set; }
}
