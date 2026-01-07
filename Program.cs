using CinemaWeb;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

List<Film> films = new()
{
    new Film { Id = 1, Titre = "Inception", Realisateur = "Christopher Nolan", Annee = 2010, Genre = "Science-Fiction", Description = "Un voleur qui infiltre les reves pour voler des secrets", ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.8}} },
    new Film { Id = 2, Titre = "Le Parrain", Realisateur = "Francis Ford Coppola", Annee = 1972, Genre = "Crime", Description = "La saga d une famille mafieuse italienne a New York", ImageUrl = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",9.2}} },
    new Film { Id = 3, Titre = "Intouchables", Realisateur = "Olivier Nakache", Annee = 2011, Genre = "Comedie", Description = "Amitie improbable entre un aristocrate et son aide", ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTYxNDA3MDQwNl5BMl5BanBnXkFtZTcwNTU4Mzc1Nw@@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.5}} },
    new Film { Id = 4, Titre = "The Matrix", Realisateur = "Wachowski", Annee = 1999, Genre = "Science-Fiction", Description = "Un hacker decouvre la vraie nature de sa realite", ImageUrl = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.7}} },
    new Film { Id = 5, Titre = "Pulp Fiction", Realisateur = "Quentin Tarantino", Annee = 1994, Genre = "Crime", Description = "Histoires entremelees de criminels a Los Angeles", ImageUrl = "https://m.media-amazon.com/images/M/MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.9}} },
    new Film { Id = 6, Titre = "Forrest Gump", Realisateur = "Robert Zemeckis", Annee = 1994, Genre = "Drame", Description = "L histoire extraordinaire d un homme simple", ImageUrl = "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.8}} },
    new Film { Id = 7, Titre = "Le Roi Lion", Realisateur = "Roger Allers", Annee = 1994, Genre = "Animation", Description = "Un jeune lion doit reprendre sa place de roi", ImageUrl = "https://m.media-amazon.com/images/M/MV5BYTYxNGMyZTYtMjE3MS00MzNjLWFjNmYtMDk3N2FmM2JiM2M1XkEyXkFqcGdeQXVyNjY5NDU4NzI@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.5}} },
    new Film { Id = 8, Titre = "Titanic", Realisateur = "James Cameron", Annee = 1997, Genre = "Drame", Description = "Histoire d amour tragique sur le navire legendaire", ImageUrl = "https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",7.9}} },
    new Film { Id = 9, Titre = "Avatar", Realisateur = "James Cameron", Annee = 2009, Genre = "Science-Fiction", Description = "Un marine sur une lune extraterrestre", ImageUrl = "https://m.media-amazon.com/images/M/MV5BZDA0OGQxNTItMDZkMC00N2UyLTg3MzMtYTJmNjg3Nzk5MzRiXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",7.8}} },
    new Film { Id = 10, Titre = "Interstellar", Realisateur = "Christopher Nolan", Annee = 2014, Genre = "Science-Fiction", Description = "Des explorateurs voyagent a travers un trou de ver", ImageUrl = "https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg", Notes = new() {{"system",8.6}} }
};

Dictionary<string, string> users = new();
Dictionary<string, string> sessions = new();
Dictionary<string, List<int>> favoris = new();

var downloadsDir = Path.Combine(app.Environment.WebRootPath ?? "wwwroot", "downloads");
Directory.CreateDirectory(downloadsDir);

app.MapPost("/api/register", (UserCred cred, HttpContext ctx) =>
{
    if (string.IsNullOrWhiteSpace(cred.Username) || string.IsNullOrWhiteSpace(cred.Password))
        return Results.BadRequest(new { message = "Nom utilisateur et mot de passe requis" });
    if (users.ContainsKey(cred.Username))
        return Results.BadRequest(new { message = "Utilisateur existe deja" });
    users[cred.Username] = cred.Password;
    favoris[cred.Username] = new List<int>();
    var sessionId = Guid.NewGuid().ToString();
    sessions[sessionId] = cred.Username;
    ctx.Response.Cookies.Append("session-id", sessionId, new CookieOptions { HttpOnly = true });
    return Results.Ok(new { username = cred.Username });
});

app.MapPost("/api/login", (UserCred cred, HttpContext ctx) =>
{
    if (!users.TryGetValue(cred.Username, out var pwd) || pwd != cred.Password)
        return Results.BadRequest(new { message = "Identifiants invalides" });
    var sessionId = Guid.NewGuid().ToString();
    sessions[sessionId] = cred.Username;
    ctx.Response.Cookies.Append("session-id", sessionId, new CookieOptions { HttpOnly = true });
    return Results.Ok(new { username = cred.Username });
});

app.MapPost("/api/logout", (HttpContext ctx) =>
{
    if (ctx.Request.Cookies.TryGetValue("session-id", out var sid))
    {
        sessions.Remove(sid);
        ctx.Response.Cookies.Delete("session-id");
    }
    return Results.Ok();
});

app.MapGet("/api/me", (HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    return Results.Ok(new { username = user });
});

app.MapGet("/api/films", () => Results.Ok(films.Select(f => new 
{ 
    f.Id, f.Titre, f.Realisateur, f.Annee, f.Genre, f.Description, f.ImageUrl, 
    noteMoyenne = Math.Round(f.NoteMoyenne, 1) 
})));

app.MapGet("/api/films/suggestions", () =>
{
    var suggestions = films.OrderByDescending(f => f.NoteMoyenne).Take(6).Select(f => new 
    { 
        f.Id, f.Titre, f.Realisateur, f.Annee, f.Genre, f.Description, f.ImageUrl,
        noteMoyenne = Math.Round(f.NoteMoyenne, 1)
    });
    return Results.Ok(suggestions);
});

app.MapGet("/api/films/search", (string q) =>
{
    var results = films.Where(f => 
        f.Titre.Contains(q, StringComparison.OrdinalIgnoreCase) || 
        f.Realisateur.Contains(q, StringComparison.OrdinalIgnoreCase) ||
        f.Genre.Contains(q, StringComparison.OrdinalIgnoreCase))
        .Select(f => new { f.Id, f.Titre, f.Realisateur, f.Annee, f.Genre, f.Description, f.ImageUrl, noteMoyenne = Math.Round(f.NoteMoyenne, 1) });
    return Results.Ok(results);
});

app.MapPost("/api/films/{id}/rate", (int id, RatingDto dto, HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    var film = films.FirstOrDefault(f => f.Id == id);
    if (film is null) return Results.NotFound();
    if (dto.Value < 0 || dto.Value > 10) 
        return Results.BadRequest(new { message = "Note entre 0 et 10" });
    film.Notes[user] = dto.Value;
    return Results.Ok(new { noteMoyenne = Math.Round(film.NoteMoyenne, 1) });
});

app.MapGet("/api/films/{id}/download", (int id, HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    var film = films.FirstOrDefault(f => f.Id == id);
    if (film is null) return Results.NotFound();
    
    var fileName = $"{film.Titre.Replace(" ", "_")}_demo.txt";
    var filePath = Path.Combine(downloadsDir, fileName);
    
    if (!File.Exists(filePath))
    {
        var content = $"FILM: {film.Titre}\nRealisateur: {film.Realisateur}\nAnnee: {film.Annee}\nGenre: {film.Genre}\n\nDescription:\n{film.Description}\n\nCeci est un fichier de demonstration.\nDans une vraie application, ce serait un fichier video.";
        File.WriteAllText(filePath, content);
    }
    
    return Results.File(filePath, "text/plain", fileName);
});

app.MapGet("/api/favoris", (HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    if (!favoris.ContainsKey(user)) favoris[user] = new List<int>();
    var userFavoris = films.Where(f => favoris[user].Contains(f.Id))
        .Select(f => new { f.Id, f.Titre, f.Realisateur, f.Annee, f.Genre, f.Description, f.ImageUrl, noteMoyenne = Math.Round(f.NoteMoyenne, 1) });
    return Results.Ok(userFavoris);
});

app.MapPost("/api/favoris/{id}", (int id, HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    if (!favoris.ContainsKey(user)) favoris[user] = new List<int>();
    if (!favoris[user].Contains(id)) favoris[user].Add(id);
    return Results.Ok();
});

app.MapDelete("/api/favoris/{id}", (int id, HttpContext ctx) =>
{
    var user = GetCurrentUser(ctx);
    if (user is null) return Results.Unauthorized();
    if (favoris.ContainsKey(user)) favoris[user].Remove(id);
    return Results.Ok();
});

string? GetCurrentUser(HttpContext ctx)
{
    if (ctx.Request.Cookies.TryGetValue("session-id", out var sid) && sessions.TryGetValue(sid, out var user))
        return user;
    return null;
}

app.Run();

public record UserCred(string Username, string Password);
public record RatingDto(double Value);
