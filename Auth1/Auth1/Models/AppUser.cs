using System.Text.Json.Serialization;

namespace Auth1.Models;

public class AppUser
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonPropertyName("password")] 
    public string Password { get; set; } = "";

}