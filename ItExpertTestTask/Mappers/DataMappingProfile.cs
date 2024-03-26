using AutoMapper;
using ItExpertTestTask.Bll.Models;
using ItExpertTestTask.Controllers.V1.Models.SaveData;
using ItExpertTestTask.Dal.Models;

namespace ItExpertTestTask.Mappers;

public class DataMappingProfile : Profile
{
    public DataMappingProfile()
    {
        CreateMap<SaveDataRequest, DataValues>();
        CreateMap<Controllers.V1.Models.SaveData.Data, Bll.Models.Data>();
        CreateMap<Controllers.V1.Models.GetData.Data, Bll.Models.Data>();
        CreateMap<Bll.Models.Data, DataValueDb>().ReverseMap();
        CreateMap<Bll.Models.Data, Controllers.V1.Models.GetData.Data>();
    }
}