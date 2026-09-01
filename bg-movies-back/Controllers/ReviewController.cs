using bg_movies.Model;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace bg_movies.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private string connectionString = "server=localhost;port=3306;database=bg_movies;user=root;password=MySecretPass123";

        [HttpPost("create")]
        public IActionResult CreateReview([FromBody] Review review)
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "INSERT INTO resenias (movie_id, autor, comentario, calificacion, fecha_creacion) VALUES (" +
                review.PeliculaId + ", '" + review.Autor + "', '" + review.Comentario + "', " + review.Resenia + ", NOW())";

            var command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();

            return Ok(new { message = "Reseña guardada exitosamente" });
        }

        [HttpGet("pelicula/{movieId}")]
        public IActionResult GetReviewsByMovie(int movieId)
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM resenias WHERE movie_id = " + movieId;

            var command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            var reviews = new List<Review>();
            while (reader.Read())
            {
               reviews.Add(new Review
        {
            Id = reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32("id"),
            PeliculaId =  reader.IsDBNull(reader.GetOrdinal("id")) ? (int?)null : reader.GetInt32("movie_id"),
            Autor = reader.IsDBNull(reader.GetOrdinal("autor"))
                ? null
                : reader.GetString("autor"),
            Comentario = reader.IsDBNull(reader.GetOrdinal("comentario"))
                ? null
                : reader.GetString("comentario"),
            Resenia = reader.IsDBNull(reader.GetOrdinal("calificacion"))
                ? (int?)null
                : reader.GetInt32("calificacion"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("fecha_creacion"))
                ? (DateTime?)null
                : reader.GetDateTime("fecha_creacion")
        });
            }

            connection.Close();

            return Ok(reviews);
        }
    }
}