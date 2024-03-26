using System.ComponentModel.DataAnnotations;

namespace ItExpertTestTask.Dal.Models;

public class DataValueDb
{
    [Key]
    public int Id { get; set; }
    public int Code { get; set; }
    public string Value { get; set; }
}