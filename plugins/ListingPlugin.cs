using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;

public class ListingPlugin
{
    private readonly IRealEstateRepository _repository;

    public ListingPlugin(IRealEstateRepository repository)
    {
        _repository = repository;
    }

    [KernelFunction("get_all_listings")]
    public List<RealEstateListing> GetAllListings()
    {
        return _repository.GetAll();
    }

    [KernelFunction("get_listings_by_city")]
    [Description("Returns all real estate listings in a given city")]
    public List<RealEstateListing> GetByCity(string city)
    {
        return _repository.GetAll()
            .Where(l => l.City.ToLower() == city.ToLower())
            .ToList();
    }

    [KernelFunction("get_listings_by_availability")]
    [Description("Returns listings that are either for 'buy' or 'rent'")]
    public List<RealEstateListing> GetByAvailability(string availability)
    {
        var getAll = _repository.GetAll();
        return getAll
            .Where(l => l.AvailableFor.ToLower() == availability.ToLower())
            .ToList();
    }

    [KernelFunction("get_listings_by_max_price")]
    [Description("Returns listings with an asking price under or equal to the given max price")]
    public List<RealEstateListing> GetByMaxPrice(int maxPrice)
    {
        return _repository.GetAll()
            .Where(l => l.AskingPrice <= maxPrice)
            .ToList();
    }

    [KernelFunction("get_listings_by_multi_filters")]
    [Description("Returns listings based on city, availability, and max price filters")]
    public List<RealEstateListing> FilterListings(string city, string availability, int maxPrice)
    {
        return _repository.GetAll()
            .Where(l =>
                l.City.ToLower() == city.ToLower() &&
                l.AvailableFor.ToLower() == availability.ToLower() &&
                l.AskingPrice <= maxPrice)
            .ToList();
    }
}
