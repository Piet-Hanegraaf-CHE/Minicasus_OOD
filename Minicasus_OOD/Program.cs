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
        slimmeThermostaat.ZetAan();
        slimmeThermostaat.ZetUit();
        slimmeThermostaat.ZetAan();
        slimmeThermostaat.ZetUit();
        slimmeThermostaat.BerekenHuidigVerbruik(4);
        TemperatuurSensor isHetHeetMeter = new TemperatuurSensor("ishetheetmeter",true);
        isHetHeetMeter.BerekenHuidigVerbruik(4);
        Zone zone1 = new Zone();
        Gebouw prisma = new Gebouw();
        Campus campus1 = new Campus();
        prisma.VoegZoneToe(zone1);
        campus1.VoegGebouwToe(prisma);
        List<HardwareComponent> components = new List<HardwareComponent>();
        components.Add(isHetHeetMeter);
        components.Add(slimmeThermostaat);
        CampusManager<HardwareComponent> campusManger =  new CampusManager<HardwareComponent>();
        campusManger.RunCampusDiagnose(components);
        double totaalverbruik = campusManger.BerekenTotaalEnergieverbruik(components);
        Console.WriteLine($"Totaalverbruik: {totaalverbruik}");
        Event gebeurtenis1 = new Event("zone1","geluidsensor", Event.Ernst.Critical);
        Event gebeurtenis2 = new Event("zone1", "geluidsensor", Event.Ernst.Warning);
        Logboek<Event> logboek = new Logboek<Event>();
        logboek.VoegToe(gebeurtenis1);
        logboek.VoegToe(gebeurtenis2);
        foreach (Event e in logboek.ErnstFilter(Event.Ernst.Critical))
        {
            Console.WriteLine($"Zone: {e.Zone}, Hardware:{e.Typehardware}, Ernst: {e.ErnstIndicatie},en Moment:{e.Moment}");
        }
    }
}


public class CampusManager<T> where T : HardwareComponent
{
    public void RunCampusDiagnose(List<T> lijstvansensoren)
    {
        foreach (T item in lijstvansensoren)
        {
            item.VoerDiagnoseUit();
        }
    }
    public double BerekenTotaalEnergieverbruik(List<T> lijstvanapparatenmetverbruik)
    {
        double totaalverbruik = 0;
        foreach (T item in lijstvanapparatenmetverbruik)
        {
            double verbruik = item.BerekenHuidigVerbruik(1);
            totaalverbruik = totaalverbruik + verbruik;
        }
        return totaalverbruik;
    }
}


public class Benchmark
{
    public float Temperatuur { get; set; }
    public float Beweging { get; set; }
    public float Stroomverbruik { get; set; }

    public void PasToe()
    {
        Console.WriteLine("Benchmark wordt toegepast:...");
        // todo: if else logic toevoegen voor check of een HardwareComponent voldoed aan de benschmark
        // todo: change to string interpeleation
        Console.WriteLine("in fabriekshal B mag de temperatuur nooit onder de 5°C zakken, of het stroomverbruik in zone C overschrijdt het maximale vermogen");

    }

    public Benchmark(float temperatuur, float beweging, float stroomverbruik)
    {
        Temperatuur = temperatuur;
        Beweging = beweging;
        Stroomverbruik = stroomverbruik;
    }

}
// Generics: Ontwerp een generieke klasse Logboek waarin logs van een specifiek type opgeslagen
// kunnen worden(bijv.Logboek, Logboek of Logboek).
// Zorg dat het generieke logboek methodes biedt om items toe te voegen, te filteren op ernst
// (Severity) en een chronologisch overzicht op te vragen.
public class Logboek<T> where T : Event
{
    public T EventItem { get; set; }
    private List<T> _logboek = new List<T>();


    public void VoegToe(T item)
    {
        _logboek.Add(item);
    }

    public List<T> HaalOp()
    {
        return _logboek;
    }

    public List<T> ErnstFilter(Event.Ernst ernst)
    {
        List<T> new_list = _logboek.Where(item => item.ErnstIndicatie == ernst).ToList();
        return new_list;
    }
    public List<T> TijdVolgorde()
    {
        List<T> new_list = _logboek.OrderBy(item => item.Moment).ToList();
        return new_list;
    }

}

public class Event
{
    public DateTime Moment { get; set; }
    public string Zone { get; set; }
    public string Typehardware { get; set; }
    public Ernst ErnstIndicatie { get; set; }
    // Events zijn doorzoekbaar op (Informational, Warning, Critical), zone en type hardware.
    public enum Ernst
    {
        Informational,
        Warning,
        Critical
    }

    public Event(string _zone, string _typehardware, Ernst _ernst)
    {
        Zone = _zone;
        Typehardware = _typehardware;
        ErnstIndicatie = _ernst;
        Moment = DateTime.Now;
    }
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
        Console.WriteLine("Diagnose regelbare Verlichting:");
    }

    public override double BerekenHuidigVerbruik(int aantaluren)
    {
        Console.WriteLine("Berekening huidg verbruik regelbare Verlichting:");
        double berekendverbruik = 6414.6 * (double)aantaluren;
        return berekendverbruik;
    }
}

public class Campus // is een compositie van gebouw. Zonder gebouw geen campus.
{
    private List<Gebouw> _gebouwenopcampus = new List<Gebouw>();

    public void VoegGebouwToe(Gebouw gebouw)
    {
        _gebouwenopcampus.Add(gebouw);
    }
}

public class Gebouw // is een compositie van zones. Zonder zones geen gebouw.
{
    //
    private List<Zone> _zonesinhetgebouw = new List<Zone>();

    public void VoegZoneToe(Zone zone)
    {
        _zonesinhetgebouw.Add(zone);
    }
}

public class Zone // is een compositie van gebouw. Zonder gebouw geen zones.
{
    private string naam;
    private List<HardwareComponent> _componentenInZone = new List<HardwareComponent> ();

    public void VoegApperaatToe(HardwareComponent apperaat)
    {
        _componentenInZone.Add (apperaat);
    }
}

public abstract class HardwareComponent
{
    protected string _naam;
    protected bool _aan;

    // aanuitlogboek is voorbereiding voor de implementatie van de BerekenHuidigVerbruik functie

    private Dictionary<DateTime, bool> _aanuitlogboek =
    new Dictionary<DateTime, bool>();

    public void GeefNaam(string naam)
    {
        _naam = naam;
    }

    protected HardwareComponent(string naam, bool aan)
    {
        _naam = naam;
        _aan = aan;
    }

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

    public virtual double BerekenHuidigVerbruik(int aantaluren)
    {
        Console.WriteLine("HardwareComponet is een abstract class en heeft geen gebruik");
        double berekendverbruik = 0;
        return berekendverbruik;
    }

}

public abstract class Sensor : HardwareComponent // aggregatie
{
    // field met lijst van metingen aanmaken
    private bool _aanhetmeten;

    protected Sensor(string naam, bool aan) : base(naam, aan)
    {
    }
    
}

public class TemperatuurSensor : Sensor
{
    public double Temperatuur { get; set; }

    public TemperatuurSensor(string naam, bool aan) : base(naam, aan)
    {
    }

    public void TemperatuurMeten()
    {
        ZetAan();
        Random rnd = new Random();
        Temperatuur = rnd.Next(-50, 13);
        ZetUit();
    }

    public override void VoerDiagnoseUit()
    {
        //zodat elk type apparaat of sensor een eigen unieke diagnose - uitvoer en verbruiksberekening heeft
        Console.WriteLine("Diagnose temperatuur sensor:");
    }

    public override double BerekenHuidigVerbruik(int aantaluren)
    {
        Console.WriteLine("Berekening huidig verbruik temperatuur sensor:");
        double berekendverbruik = 685.6 * (double)aantaluren;
        return berekendverbruik;
    }
}

public class BewegingSensor : Sensor
{
    public BewegingSensor(string naam, bool aan) : base(naam, aan)
    {
    }

    public void BewegingMeten()
    {
        ZetAan();
    }

    public override void VoerDiagnoseUit()
    {
        //zodat elk type apparaat of sensor een eigen unieke diagnose - uitvoer en verbruiksberekening heeft
        Console.WriteLine("Diagnose bewegingssensor:");
    }

    public override double BerekenHuidigVerbruik(int aantaluren)
    {
        Console.WriteLine("Berekening huidig verbruik bewegingssensor:");
        double berekendverbruik = 64.6 * (double)aantaluren;
        return berekendverbruik;
    }
}

public abstract class Apparaat : HardwareComponent
{
    public Apparaat(string naam, bool aan) : base (naam, aan)
    {
    }

    public string Naam => _naam;

    public bool Aan => _aan;
}

public class Ventilatiesysteem : Apparaat
{
    private double _co2_waarde;

    private int _intensiteit_stand;

    public void VeranderIntensiteit(int stand)
    {
        _intensiteit_stand = stand;
    }
    public Ventilatiesysteem(string naam, bool aan) : base(naam, aan)
    {
    }

    public override void VoerDiagnoseUit()
    {
        Console.WriteLine("Diagnose ventilatiesysteem:...");
    }

    public override double BerekenHuidigVerbruik(int aantaluren)
    {
        Console.WriteLine("Berekening huidig verbruik ventilatiesysteem:");
        double berekendverbruik = 7764.6 * (double)aantaluren;
        return berekendverbruik;
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
        Console.WriteLine("Diagnose Slimme Thermostaat:...");
    }

    public override double BerekenHuidigVerbruik(int aantaluur)
    {
        //zodat elk type apparaat of sensor een eigen unieke verbruiksberekening heeft.
        double berekendverbruik = 4.6 * (double)aantaluur;
        return berekendverbruik;
    }
}