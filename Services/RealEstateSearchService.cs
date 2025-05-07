// public class RealEstateSearchService : IRealEstateSearchService
// {
//     private readonly IRealEstateRepository _repository;

//     public RealEstateSearchService(IRealEstateRepository repository)
//     {
//         _repository = repository;
//     }

//     public IEnumerable<RealEstateListing> SearchListings(RealEstateListingFilter filter)
//     {
//         var query = _repository.GetAll().Where(listing =>
//             // City filter
//             (filter.City == null || listing.City.Equals(filter.City, StringComparison.OrdinalIgnoreCase)) &&
            
//             // Province filter
//             (filter.Province == null || listing.Province.Equals(filter.Province, StringComparison.OrdinalIgnoreCase)) &&
            
//             // ZipCode filter
//             (filter.ZipCode == null || listing.ZipCode == filter.ZipCode) &&
            
//             // Country filter
//             (filter.Country == null || listing.Country.Equals(filter.Country, StringComparison.OrdinalIgnoreCase)) &&
            
//             // Energy Label filter
//             (filter.EnergyLabel == null || listing.EnergyLabel.Equals(filter.EnergyLabel, StringComparison.OrdinalIgnoreCase)) &&
            
//             // Bedrooms filters
//             (filter.MinBedrooms == null || listing.Bedrooms >= filter.MinBedrooms) &&
//             (filter.MaxBedrooms == null || listing.Bedrooms <= filter.MaxBedrooms) &&
            
//             // Rooms filters
//             (filter.MinRooms == null || listing.Rooms >= filter.MinRooms) &&
//             (filter.MaxRooms == null || listing.Rooms <= filter.MaxRooms) &&
            
//             // Bathrooms filters
//             (filter.MinBathrooms == null || listing.Bathrooms >= filter.MinBathrooms) &&
//             (filter.MaxBathrooms == null || listing.Bathrooms <= filter.MaxBathrooms) &&
            
//             // Apartment/House filter
//             (filter.IsApartment == null || (filter.IsApartment.HasValue && listing.Type.Equals("apartment", StringComparison.OrdinalIgnoreCase))) &&
//             (filter.IsDetachedHouse == null || (filter.IsDetachedHouse.HasValue && listing.Type.Equals("house", StringComparison.OrdinalIgnoreCase))) &&
            
            
//             // Detached House filter
//             (filter.IsDetachedHouse == null || listing.Detached == filter.IsDetachedHouse) &&
            
//             // Availability to Buy or Rent filter
//             // Availability filter (convert IsAvailableToBuy / IsAvailableToRent to AvailableFor)
//             (filter.IsAvailableToBuy == null || (filter.IsAvailableToBuy.HasValue && listing.AvailableFor.Equals("buy", StringComparison.OrdinalIgnoreCase))) &&
//             (filter.IsAvailableToRent == null || (filter.IsAvailableToRent.HasValue && listing.AvailableFor.Equals("rent", StringComparison.OrdinalIgnoreCase))) &&
            
            
//             // Price filters
//             (filter.MinPrice == null || listing.AskingPrice >= filter.MinPrice) &&
//             (filter.MaxPrice == null || listing.AskingPrice <= filter.MaxPrice)
//         );

//         return query;
//     }
// }


public class RealEstateSearchService : IRealEstateSearchService
{
    private readonly IRealEstateRepository _repository;

    public RealEstateSearchService(IRealEstateRepository repository)
    {
        _repository = repository;
    }

    public List<RealEstateListing> GetAllListings()
    {
        return _repository.GetAll();
    }

    public List<RealEstateListing> GetByCity(string city)
    {
        return _repository.GetAll()
            .Where(l => l.City.ToLower() == city.ToLower())
            .ToList();
    }

    public List<RealEstateListing> GetByAvailability(string availability)
    {
        var getAll = _repository.GetAll();
        return getAll
            .Where(l => l.AvailableFor.ToLower() == availability.ToLower())
            .ToList();
    }

    public List<RealEstateListing> GetByMaxPrice(int maxPrice)
    {
        return _repository.GetAll()
            .Where(l => l.AskingPrice <= maxPrice)
            .ToList();
    }

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