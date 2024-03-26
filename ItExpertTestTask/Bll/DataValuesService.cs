using ItExpertTestTask.Bll.Models;
using ItExpertTestTask.Dal.Repositories;

namespace ItExpertTestTask.Bll;

public class DataValuesService(DataRepository dataRepository)
{
    public async Task SaveData(DataValues data, CancellationToken token)
    {
        await dataRepository.SaveData(data, token);
    }
    
    public async Task<Data[]> GetData(int[] codes, CancellationToken token)
    {
        return await dataRepository.GetData(codes, token);
    }
}