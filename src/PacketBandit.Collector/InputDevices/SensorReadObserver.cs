namespace PacketBandit.Collector.InputDevices;

public sealed class SensorReadObserver : IObserver<SensorRead>
{
    public void OnNext(SensorRead value)
    {
        Console.WriteLine($"[Sensor:{value.SensorId,4}] Read: {value.Value}");
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
        Console.Error.WriteLine(error.Message);
    }
}