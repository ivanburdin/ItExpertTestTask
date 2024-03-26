namespace ItExpertTestTask.Controllers.V1.Models.GetData;

public record GetDataResponse(Data[] Data);

public record Data(int Id, int Code, string Value);