using PacketBandit.Collector.InputDevices;
using Range = PacketBandit.Collector.InputDevices.Range;

namespace PacketBandit.Collector;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("PacketBandit.Collector is starting up...");
        Console.WriteLine("Use <Ctrl>+C to stop the application.");

        var sensors = new List<Sensor>();
        var random = new Random();
        for (var i = 0; i < 10 * 1000; i++)
        {
            int intervalMs = random.Next(500, 10000);
            var sensor = new Sensor(i + 1, TimeSpan.FromMilliseconds(intervalMs), new Range(-20, 50));
            sensors.Add(sensor);
            sensor.Subscribe(new SensorReadObserver());
        }

        Console.WriteLine("Starting sensors...");
        sensors.ForEach(x => x.Start());

        var stopEvent = new ManualResetEventSlim(false);
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;

            Console.WriteLine("Stopping sensors...");
            sensors.ForEach(x => x.Stop());
            stopEvent.Set();
        };

        stopEvent.Wait();
    }
}