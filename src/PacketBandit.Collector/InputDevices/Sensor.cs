using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PacketBandit.Collector.InputDevices;

public sealed class Sensor(int id, TimeSpan readInterval, Range range)
    : IObservable<SensorRead>, IDisposable
{
    private readonly Random _random = new();
    private readonly Subject<SensorRead> _readSubject = new();
    private IDisposable? _subscription;

    public void Start()
    {
        _subscription = Observable.Interval(readInterval).Subscribe(_ => GenerateRead());
    }

    private void GenerateRead()
    {
        int readValue = _random.Next(range.Min, range.Max);
        var sensorRead = new SensorRead(id, readValue);
        _readSubject.OnNext(sensorRead);
    }

    public void Stop()
    {
        _subscription?.Dispose();
    }

    public IDisposable Subscribe(IObserver<SensorRead> observer)
    {
        return _readSubject.Subscribe(observer);
    }

    public void Dispose()
    {
        Stop();
        _readSubject.OnCompleted();
        _readSubject.Dispose();
    }
}