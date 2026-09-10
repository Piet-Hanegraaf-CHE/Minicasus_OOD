using System.Xml.Linq;

public class Apparaat
{
    private string _naam;
    private bool _aan;

    public Apparaat(string naam, bool aan)
    {
        _naam = naam;
        _aan = aan;
    }

    public string Naam => _naam;

    public bool Aan => _aan;
}

public class Slimme_thermostaat : Apparaat
{
    private double _temperatuur { get; set; }

    public Slimme_thermostaat(string naam, bool aan) : base(naam, aan) { }


    public double VeranderTemperatuur(double temperatuur)
    {
        _temperatuur = temperatuur;
        return _temperatuur;
    }
}