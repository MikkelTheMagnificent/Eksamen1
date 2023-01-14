namespace Models
{

public class Kunde
{
    public int Id { get; set; }
    public string Navn { get; set; }
    public string ?Email { get; set; }
    public string ?Type { get; set; }
}
}
//Kunder indeholder som minimum navn, id, email og type (erhverv eller privat).

