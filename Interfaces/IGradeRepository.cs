using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

// Renamed from IItemReader to IGradeRepository
public interface IGradeRepository
{
    Task<Grade?> GetByIdAsync(int id);
    Task<IEnumerable<Grade>> GetAllAsync();
}
