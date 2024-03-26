namespace ItExpertTestTask.Controllers.V1.Models.SaveData;

public record SaveDataRequest(Data[] Data);

public record Data(int Code, string Value);