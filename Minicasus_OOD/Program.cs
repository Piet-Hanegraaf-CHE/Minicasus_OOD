using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Slimme_thermostaat slimmeThermostaat = new Slimme_thermostaat("Slimme Thermostaat", true);
        Console.WriteLine($"Apparaat: {slimmeThermostaat.Naam}, Aan: {slimmeThermostaat.Aan}");
        slimmeThermostaat.Temperatuurinsteller(22);
        Console.WriteLine($"Nieuwe temperatuur ingesteld op: {slimmeThermostaat.Temperatuur}°C");
    }
}

public class Benchmark
{
    private float temperatuur;
    private float beweging;

    private float stroomverbruik;

}

public class Regelbare_verlichting
{
    private decimal _helderheid; // verborgen voor de buitenwereld

    public decimal Helderheid => _helderheid; // alleen-lezen naar buiten toe

    public void Dimmer(decimal helderheid)
    {
        if (helderheid <= 0)
            throw new ArgumentException("Helderheid moet positief zijn");
        if (helderheid > 100)
            throw new ArgumentException("Helderheid moet onder de 100% zijn");
        _helderheid = helderheid;
    }

}

public class Zone // is een compositie van gebouw. Zonder gebouw geen zones.
{
    private string naam;
}

public abstract class Sensor : Apparaat
{
    private bool _aanhetmeten;

    protected Sensor(string naam, bool aan) : base(naam, aan)
    {
    }
}

public abstract class Apparaat
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

    public Slimme_thermostaat(string naam, bool aan) : base(naam, aan) { }

    private decimal _temperatuur; // verborgen voor de buitenwereld

    public decimal Temperatuur => _temperatuur; // alleen-lezen naar buiten toe

    public void Temperatuurinsteller(decimal temperatuur)
    {
        if (temperatuur <= -20)
            throw new ArgumentException("Temperatuur moet boven de -20 zijn");
        if (temperatuur > 60)
            throw new ArgumentException("Temperatuur moet onder de 60 zijn");
        _temperatuur = temperatuur;
    }


}