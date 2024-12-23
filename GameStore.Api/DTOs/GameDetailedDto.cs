namespace GameStore.Api.DTOs
{
    public record class GameDetailedDto
    (
        int Id,
        string Name,
        int GenreId,
        decimal Price,
        DateTime RelaseDate

    );
}
