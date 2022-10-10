namespace NeuLibrary.Application.DTO
{
    public class CreateBookDTO
    {
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Genre { get; set; }
        public byte[]? Thumbnails { get; set; }
    }
}
