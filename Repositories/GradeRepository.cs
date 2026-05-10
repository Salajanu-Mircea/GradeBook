using System.Text.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

// Renamed from ItemRepository to GradeRepository
public class GradeRepository : IGradeRepository
{
    private readonly HttpClient _httpClient;
    
    private const string BaseUrl = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1cee3737a681c487f7295ac82e6b/gistfile1.txt";
    
    // OLD: protected readonly List<Item> _items = new();'
    /*
    private readonly List<Grade> _grades = new()
    {
        new Grade { Id = 1, Value = 4.25m, IsActive = true },
        new Grade { Id = 2, Value = 7.50m, IsActive = true },
        new Grade { Id = 3, Value = 9.00m, IsActive = false },
        new Grade { Id = 4, Value = 5.00m, IsActive = true },
        new Grade { Id = 5, Value = 3.00m, IsActive = true }
    }; 
    */
    
    // OLD: protected int _nextId = 1;
    private int _nextId = 1; // private instead of protected

    // OLD: var item = _items.FirstOrDefault(i => i.Id == id && i.IsActive);  <- filtered by IsActive!
    //removed IsActive filter. Filtering moves to Service layer.
    public async Task<Grade?> GetByIdAsync(int id)
    {
        //var grade = _grades.FirstOrDefault(g => g.Id == id);
        //return Task.FromResult(grade);
        var grades = await LoadGradesFromExternalSourceAsync();
        return grades.FirstOrDefault(g => g.Id == id);
    }

    // OLD: var items = _items.Where(i => i.IsActive).AsEnumerable();  <- filtered by IsActive!
    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        //return Task.FromResult(_grades.AsEnumerable());
        return await LoadGradesFromExternalSourceAsync();
    }
    
    public GradeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    private async Task<List<Grade>> LoadGradesFromExternalSourceAsync()
    {
        var response = await _httpClient.GetStringAsync(BaseUrl);
        var gradeResponse = JsonSerializer.Deserialize<GradeJson>(response);
        return gradeResponse?.Items ?? new List<Grade>();
    }
}
