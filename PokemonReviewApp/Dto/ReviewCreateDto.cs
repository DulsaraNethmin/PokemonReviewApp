namespace PokemonReviewApp.Dto
{
    public class ReviewCreateDto : ReviewDto
    {
        public int PokemonId { get; set; }
        public int ReviewerId { get; set; }
    }
}
