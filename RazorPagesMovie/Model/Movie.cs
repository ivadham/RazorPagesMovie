using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseData { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}
