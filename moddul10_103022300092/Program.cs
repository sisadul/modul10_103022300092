
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class Movie
{ 
    public string Title { get; set; }
    public string Directory { get; set; }
    public List<string> Stars { get; set; }
    public string Description { get; set; }

    public Movie() 
    {
        this.Title = Title;
        this.Directory = Directory;
        this.Stars = new List<string>();
        this.Description = Description;
    }
}
[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private static List<Movie> daftarFilm = new List<Movie>
    {
            new Movie{Title = " The Shawshank Redemption",Directory ="Frank Darabont",
                Stars = [" Tim Robbins","Morgan Freeman","Bob Gunton"],
                Description = "A banker convicted of uxoricide forms a friendship over a " +
                "quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion." },
            new Movie{Title = " The Godfather",Directory ="Francis Ford Coppola",
                Stars = [" Marlon Brando","AI Pacino","James Caan"],
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son" },
            new Movie{Title = " The Dark Knight",Directory ="Christopher Nolan",
                Stars = [" Christian Bale", "Heath Ledger","Aaron Eckhart"],
                Description = "When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, " +
                "James Gordon and Harvey Dent must work together to put an end to the madness." }
    };
    [HttpGet]
    public ActionResult<List<Movie>> GetMovie()
    {
        return Ok(daftarFilm);
    }

    [HttpGet("{idx}")]
    public ActionResult<Movie> GetIdxMovie(int idx)
    {
        if (idx >= daftarFilm.Count || idx < 0)
        {
            return NotFound();
        }
        return Ok(daftarFilm[idx]);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Movie film)
    {
        daftarFilm.Add(film);
        return Ok();
    }

    [HttpDelete("{idx}")]

    public ActionResult Delete(int idx)
    {
        if (idx >= daftarFilm.Count || idx < 0)
        {
            return NotFound();
        }
        daftarFilm.RemoveAt(idx);
        return Ok();
    }
}
