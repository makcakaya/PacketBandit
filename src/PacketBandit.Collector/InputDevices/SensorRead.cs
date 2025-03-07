namespace PacketBandit.Collector.InputDevices;

public readonly struct SensorRead(int sensorId, int value)
{
    public int SensorId { get; } = sensorId;
    public int Value { get; } = value;
}