using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

// Renamed from ItemService to GradeService
// Flow: GradeController -> GradeService -> GradeRepository
public class GradeService : IGradeService
{
    private readonly IGradeRepository _repository;

    public GradeService(IGradeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Grade>> GetNPassingActiveGradesAsync(int count)
    {
        var grades = await _repository.GetAllAsync();

        return grades.Where(x => x.IsActive && x.Value >= 5)
            .Take(count);
    }
}
