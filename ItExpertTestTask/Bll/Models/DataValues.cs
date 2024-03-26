namespace ItExpertTestTask.Bll.Models;

public record DataValues(Data[] Data);

public record Data(int Id, int Code, string Value)
{
    private const int DefaultId = 0;

    public Data(int Code, string Value) : this(DefaultId, Code, Value)
    {
    }
}