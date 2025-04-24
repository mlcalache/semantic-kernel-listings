using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class RealEstateRepository : IRealEstateRepository
{
    private readonly string _filePath;
    private List<RealEstateListing> _listings;

    public RealEstateRepository(string fileName)
    {
        string baseDirectory = AppContext.BaseDirectory;
        _filePath = Path.Combine(baseDirectory, "data", fileName);
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _listings = JsonSerializer.Deserialize<List<RealEstateListing>>(json) ?? new List<RealEstateListing>();
        }
        else
        {
            _listings = new List<RealEstateListing>();
        }
    }

    private void SaveData()
    {
        string json = JsonSerializer.Serialize(_listings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<RealEstateListing> GetAll()
    {
        return _listings;
    }

    public RealEstateListing? GetById(string id)
    {
        return _listings.FirstOrDefault(l => l.Id == id);
    }

    public void Create(RealEstateListing listing)
    {
        if (_listings.Any(l => l.Id == listing.Id))
            throw new InvalidOperationException($"Listing with ID {listing.Id} already exists.");

        _listings.Add(listing);
        SaveData();
    }

    public void Update(RealEstateListing updatedListing)
    {
        var index = _listings.FindIndex(l => l.Id == updatedListing.Id);
        if (index == -1)
            throw new InvalidOperationException($"Listing with ID {updatedListing.Id} not found.");

        _listings[index] = updatedListing;
        SaveData();
    }

    public void Delete(string id)
    {
        var listing = _listings.FirstOrDefault(l => l.Id == id);
        if (listing == null)
            throw new InvalidOperationException($"Listing with ID {id} not found.");

        _listings.Remove(listing);
        SaveData();
    }
}
