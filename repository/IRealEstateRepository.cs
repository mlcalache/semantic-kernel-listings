using System.Collections.Generic;

public interface IRealEstateRepository
{
    List<RealEstateListing> GetAll();
    RealEstateListing? GetById(string id);
    void Create(RealEstateListing listing);
    void Update(RealEstateListing listing);
    void Delete(string id);
}
