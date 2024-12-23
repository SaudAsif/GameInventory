namespace GameStore.Api.DTOs
{
    public record class GameSummaryDto( 
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateTime RelaseDate
        
        );
}
