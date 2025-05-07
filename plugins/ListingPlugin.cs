using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;
using System.Text;
using System.Text.Json;

public class ListingPlugin : IListingPlugin
{
    private readonly IRealEstateSearchService _searchService;

    public ListingPlugin(IRealEstateSearchService searchService)
    {
        _searchService = searchService;
    }

    [KernelFunction("get_all_listings")]
    public List<RealEstateListing> GetAllListings()
    {
        return _searchService.GetAllListings();
    }

    [KernelFunction("get_listings_by_city")]
    [Description("Returns all real estate listings in a given city")]
    public List<RealEstateListing> GetByCity(string city)
    {
        return _searchService.GetByCity(city);
    }

    [KernelFunction("get_listings_by_availability")]
    [Description("Returns listings that are either for 'buy' or 'rent'")]
    public List<RealEstateListing> GetByAvailability(string availability)
    {
        return _searchService.GetByAvailability(availability);
    }

    [KernelFunction("get_listings_by_max_price")]
    [Description("Returns listings with an asking price under or equal to the given max price")]
    public List<RealEstateListing> GetByMaxPrice(int maxPrice)
    {
        return _searchService.GetByMaxPrice(maxPrice);
    }

    [KernelFunction("get_listings_by_multi_filters")]
    [Description("Returns listings based on city, availability, and max price filters")]
    public List<RealEstateListing> FilterListings(string city, string availability, int maxPrice)
    {
        return _searchService.FilterListings(city, availability, maxPrice);
    }
}
