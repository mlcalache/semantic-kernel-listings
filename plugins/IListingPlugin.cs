public interface IListingPlugin
{
    List<RealEstateListing> GetAllListings();
    List<RealEstateListing> GetByCity(string city);
    List<RealEstateListing> GetByAvailability(string availability);
    List<RealEstateListing> GetByMaxPrice(int maxPrice);
    List<RealEstateListing> FilterListings(string city, string availability, int maxPrice);
}