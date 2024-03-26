using System.ComponentModel.DataAnnotations;

namespace ItExpertTestTask.Dal.Models;

public class HttpLogMessageDb
{
    [Key]
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Message { get; set; }
}