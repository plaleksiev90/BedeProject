using Newtonsoft.Json;

namespace BedeBackEndTestProject.Models
{
    public class BookModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }
    }
}
