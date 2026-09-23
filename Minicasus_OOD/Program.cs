using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    public static void Main(string[] args)
    {
        SlimmeThermostaat slimmeThermostaat = new SlimmeThermostaat("Slimme Thermostaat", true);
        Console.WriteLine($"Apparaat: {slimmeThermostaat.Naam}, Aan: {slimmeThermostaat.Aan}");
        slimmeThermostaat.SetTemperatuur(22);
        Console.WriteLine($"Nieuwe temperatuur ingesteld op: {slimmeThermostaat.Temperatuur}°C");
    }
}

public class Benchmark
{
    private float temperatuur;
    private float beweging;

    private float stroomverbruik;

}
// Generics: Ontwerp een generieke klasse Logboek waarin logs van een specifiek type opgeslagen
// kunnen worden(bijv.Logboek, Logboek of Logboek).
// Zorg dat het generieke logboek methodes biedt om items toe te voegen, te filteren op ernst
// (Severity) en een chronologisch overzicht op te vragen.
public class Logboek<T>
{
    private T _logboek;

    public void Vul(T item) => _logboek = item;
    public T Haal() => _logboek;
}



public class RegelbareVerlichting : Apparaat
{
    private decimal _helderheid; // verborgen voor de buitenwereld

    public RegelbareVerlichting(string naam, bool aan) : base(naam, aan)
    {
    }

    public decimal Helderheid => _helderheid; // alleen-lezen naar buiten toe

    public void SetDimmer(decimal helderheid)
    {
        if (helderheid < 0)
            throw new ArgumentException("Helderheid moet positief zijn");
        if (helderheid > 100)
            throw new ArgumentException("Helderheid moet onder de 100% zijn");
        _helderheid = helderheid;
    }

}

public class Camput // is een compositie van gebouw. Zonder gebouw geen campus.
{
    //
}

public class Gebouw // is een compositie van zones. Zonder zones geen gebouw.
{
    //
}

public class Zone // is een compositie van gebouw. Zonder gebouw geen zones.
{
    private string naam;
}

public abstract class HardwareComponent
{
    private string _naam;
    private bool _aan;
}

public abstract class Sensor : HardwareComponent // aggregatie
{
    private bool _aanhetmeten;

    protected Sensor(string naam, bool aan)
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

public class SlimmeThermostaat : Apparaat //aggregatie
{

    public SlimmeThermostaat(string naam, bool aan) : base(naam, aan) { }

    private decimal _temperatuur; // verborgen voor de buitenwereld

    public decimal Temperatuur => _temperatuur; // alleen-lezen naar buiten toe

    public void SetTemperatuur(decimal temperatuur)
    {
        if (temperatuur < -20)
            throw new ArgumentException("Temperatuur moet boven de -20 zijn");
        if (temperatuur > 60)
            throw new ArgumentException("Temperatuur moet onder de 60 zijn");
        _temperatuur = temperatuur;
    }


}