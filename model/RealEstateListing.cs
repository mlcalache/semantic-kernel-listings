using System.Text.Json.Serialization;

public class RealEstateListing
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("zip_code")]
    public string ZipCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("house_number")]
    public int HouseNumber { get; set; }

    [JsonPropertyName("additional_house_number")]
    public string AdditionalHouseNumber { get; set; }

    [JsonPropertyName("energy_label")]
    public string EnergyLabel { get; set; }

    [JsonPropertyName("bedrooms")]
    public int Bedrooms { get; set; }

    [JsonPropertyName("rooms")]
    public int Rooms { get; set; }

    [JsonPropertyName("bathrooms")]
    public int Bathrooms { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } // "apartment" or "house"

    [JsonPropertyName("detached")]
    public bool Detached { get; set; }

    [JsonPropertyName("available_for")]
    public string AvailableFor { get; set; } // "buy" or "rent"

    [JsonPropertyName("asking_price")]
    public int AskingPrice { get; set; }
}
