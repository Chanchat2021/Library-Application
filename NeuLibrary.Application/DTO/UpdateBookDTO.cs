namespace NeuLibrary.Application.DTO
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsReserved{ get; set; }
        public byte[]? Thumbnails { get; set; }
    }
}
