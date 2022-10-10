namespace NeuLibrary.Application.DTO
{
    public class GetReserveBooksDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservedDate { get; set; }
    }
}
