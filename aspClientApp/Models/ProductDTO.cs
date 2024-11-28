using System.Text.Json.Serialization;

namespace aspClientApp.Models
{
    public class ProductDTO
    {

        [JsonPropertyName("productID")]
        public int ProductID { get; set; }
        [JsonPropertyName("productName")]

        public string ProductName { get; set; } = null!;

        [JsonPropertyName("price")]

        public decimal Price { get; set; }
    }
}