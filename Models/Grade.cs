using System.Text.Json.Serialization;

namespace Siemens.Internship2026.GradeBook.Models;

public class Grade
{
    //Updated fields to match the JSON format
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
    
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;
}

//The JSON contains all entries in a List called "items"
//This is needed for looking up the items inside the list and for deserialization
public class GradeJson
{
    [JsonPropertyName("items")] 
    public List<Grade> Items { get; set; } = new();
}
