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


public class CampusManager
{
    public void RunCampusDiagnose(List<T>) 
    { }
    public void BerekenTotaalEnergieverbruik(List<T>)
    { }
}

public class T
{
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
    private List<T> _logboek;

    public Func<T, object> Severity { get; private set; }

    public void VoegToe(T item)
    {
        _logboek.Add(item);
    }

    public List<T> HaalOp()
    {
        return _logboek;
    }

    public List<T> ErnstFilter()
    {
        _logboek.OrderBy(Severity);
        return _logboek;
    }
}

public class Event()
{
    // Events zijn doorzoekbaar op (Informational, Warning, Critical), zone en type hardware.
    public enum Ernst
    {
        Informational,
        Warning,
        Critical
    }

    string _zone;

    string _typehardware;



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

    public override void VoerDiagnoseUit()
    {
        throw new NotImplementedException();
    }

    public override void BerekenHuidigVerbruik()
    {
        throw new NotImplementedException ();
    }
}

public class Campus // is een compositie van gebouw. Zonder gebouw geen campus.
{
    private List<Gebouw> _gebouwenopcampus = new List<Gebouw>();
}

public class Gebouw // is een compositie van zones. Zonder zones geen gebouw.
{
    //
    private List<Zone> _zonesinhetgebouw = new List<Zone>();
}

public class Zone // is een compositie van gebouw. Zonder gebouw geen zones.
{
    private string naam;
    private List<HardwareComponent> _apparateninhetgebouw = new List<HardwareComponent> ();
}

public abstract class HardwareComponent
{
    private string _naam;

    // aanuitlogboek is voorbereiding voor de implementatie van de BerekenHuidigVerbruik functie
    private bool _aan;

    private Dictionary<DateTime, bool> _aanuitlogboek =
    new Dictionary<DateTime, bool>();

    public void ZetAan()
    {
        _aan = true;
        _aanuitlogboek.Add(DateTime.Now, true);
    }

    public void ZetUit()
    {
        _aan = false;
        _aanuitlogboek[DateTime.Now] = false;
    }

    public Dictionary<DateTime, bool> GetAanUitLogboek()
    {
        return _aanuitlogboek;
    }

    public abstract void VoerDiagnoseUit();

    public virtual void BerekenHuidigVerbruik()
    {
        Console.WriteLine("HardwareComponet is een abstract class en heeft geen gebruik");
    }

}

public abstract class Sensor : HardwareComponent // aggregatie
{
    private bool _aanhetmeten;

    protected Sensor(string naam, bool aan)
    {
    }
    
}

public class TemperatuurSensor : Sensor
{
    //
    public TemperatuurSensor(string naam, bool aan) : base(naam, aan)
    {
    }

    public override void VoerDiagnoseUit()
    {
        //zodat elk type apparaat of sensor een eigen unieke diagnose - uitvoer en verbruiksberekening heeft
        throw new NotImplementedException();
    }
}

public class BewegingSensor : Sensor
{
    public BewegingSensor(string naam, bool aan) : base(naam, aan)
    {
    }

    public override void VoerDiagnoseUit()
    {
        //zodat elk type apparaat of sensor een eigen unieke diagnose - uitvoer en verbruiksberekening heeft
        throw new NotImplementedException();
    }

    public override void BerekenHuidigVerbruik()
    {
        throw new NotImplementedException();
    }
}

public abstract class Apparaat : HardwareComponent
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

public class Ventilatiesysteem : Apparaat
{
    public Ventilatiesysteem(string naam, bool aan) : base(naam, aan)
    {
    }

    public override void VoerDiagnoseUit()
    {
        throw new NotImplementedException();
    }
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

    public override void VoerDiagnoseUit()
    {
        //zodat elk type apparaat of sensor een eigen unieke diagnose heeft
        throw new NotImplementedException();
    }

    public override void BerekenHuidigVerbruik()
    {
        //zodat elk type apparaat of sensor een eigen unieke verbruiksberekening heeft.
        //Dit kan ik berekenen op basis van het aan uit logboek
        // Termo SlimmeThermostaat = new SlimmeThermostaat("Termo", true);
        // Termo.GetAanUitLogboek();
        throw new NotImplementedException();
    }
}