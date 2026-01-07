namespace CinemaWeb;

public class Film
{
    public int Id { get; set; }
    public string Titre { get; set; } = "";
    public string Realisateur { get; set; } = "";
    public int Annee { get; set; }
    public string Genre { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public Dictionary<string, double> Notes { get; set; } = new();
    public double NoteMoyenne => Notes.Count > 0 ? Notes.Values.Average() : 0;
}
