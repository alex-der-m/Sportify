using Sportify_back.Models;
using Sportify_Back.Models;
using Sportify_Back.Services;
using System.Linq;

public class EntityReportService : IEntityReportService
{
    private readonly SportifyDbContext _context;

    public EntityReportService(SportifyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<MonthlyClassReport> GenerateClassesReport()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MonthlyClassReport> GenerateClassReport()
    {   
    var report = _context.Classes
        .Where(c => c.Active) // Solo considera clases activas (si aplica)
        .GroupBy(c => c.Sched.Month) // Agrupa por mes
        .Select(group => new MonthlyClassReport
        {
            Month = group.Key,
            TotalClasses = group.Count(), // Total de clases en el mes
            TotalQuota = group.Sum(c => c.Quota), // Suma de cupos
            AverageQuota = group.Average(c => c.Quota), // Promedio de cupos
            TopTeacherId = group.GroupBy(c => c.TeachersId) // Agrupa por profesor
                                .OrderByDescending(g => g.Count()) // Ordena por clases impartidas
                                .Select(g => g.Key) // Obtén el ID del profesor
                                .FirstOrDefault() // Profesor con más clases
        })
        .OrderBy(r => r.Month) // Ordena por mes
        .ToList();

    return report;
    }



   
}
