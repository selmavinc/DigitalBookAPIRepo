using AuthorService.DatabaseEntity;

namespace ReaderService.DatabaseEntity
{
    public partial class bookList
    {
        public int? BookId { get; set; }
        public string? BookName { get; set; }
        public string? CategoryName { get; set; }
        public decimal? Price { get; set; }
        public string? Publisher { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? EmailId { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
        public virtual Category? Category { get; set; }
    }
}
