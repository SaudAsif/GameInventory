namespace GameStore.Api.Entities
{
    public class Game
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime RelaseDate { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; } 


    }
}
