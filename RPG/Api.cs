namespace Api{ 
using System.Text.Json.Serialization;
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Root
    {
        [JsonPropertyName("count")]
        public int count { get; set; }


        [JsonPropertyName("probability")]
        public double probability { get; set; }
    }





}