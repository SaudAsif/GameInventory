namespace GameStore.Api.DTOs
{
    public record class UpdateGameDto(
        string Name,
        int GenreId,
        decimal Price,
        DateTime RelaseDate
        );

}
