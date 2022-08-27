namespace Sim.Domain.Entity;

public class EChart {
    public EChart(string label, int value, string percent){
        Label = label;
        Value = value;
        Percent = percent;
    }

    public string Label { get; private set; }
    public int Value { get; private set; }
    public string Percent { get; private set; }
}