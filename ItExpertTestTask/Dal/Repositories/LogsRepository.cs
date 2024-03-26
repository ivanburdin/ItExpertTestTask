using ItExpertTestTask.Dal.Models;

namespace ItExpertTestTask.Dal.Repositories;

public class LogsRepository
{
    private readonly DataContext _dataContext;

    public LogsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task WriteToDb(string message, CancellationToken token)
    {
        await _dataContext.Logs.AddAsync(new HttpLogMessageDb{Message = message, Date = DateTime.UtcNow}, token);
        await _dataContext.SaveChangesAsync(token);
    }
}