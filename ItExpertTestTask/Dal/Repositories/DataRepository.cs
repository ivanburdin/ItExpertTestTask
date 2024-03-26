using AutoMapper;
using ItExpertTestTask.Bll.Models;
using ItExpertTestTask.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace ItExpertTestTask.Dal.Repositories;

public class DataRepository
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public DataRepository(IMapper mapper,
        DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }

    public async Task SaveData(DataValues data, CancellationToken token)
    {
        await _dataContext.Set<DataValueDb>().ExecuteDeleteAsync(token);

        var dbData = data.Data
            .OrderBy(d => d.Code)
            .Select(d => _mapper.Map<DataValueDb>(d));

        await _dataContext.DataValues.AddRangeAsync(dbData, token);
        await _dataContext.SaveChangesAsync(token);
    }
    
    public async Task<Data[]> GetData(int[] codes, CancellationToken token)
    {
         return await _dataContext.DataValues
            .Where(d => !codes.Any() || codes.Contains(d.Code))
            .Select(d => _mapper.Map<Data>(d))
            .ToArrayAsync(token);
    }
}