namespace bg_movies.Model
{
    public class Review
    {
        public int? Id { get; set; }
        public int? PeliculaId { get; set; }
        public string? Autor { get; set; }
        public string? Comentario { get; set; }
        public int? Resenia { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
