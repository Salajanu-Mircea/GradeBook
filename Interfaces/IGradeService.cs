using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

// Renamed from IItemService to IGradeService
// The controller depends on this interface (service layer abstraction)
public interface IGradeService
{
    Task<Grade?> GetByIdAsync(int id);
    Task<IEnumerable<Grade>> GetAllAsync();
    
    //III Filtering the first N grades that are above 5 and active
    Task<IEnumerable<Grade>> GetNPassingActiveGradesAsync(int count);
}
