namespace Sim.Domain.Entity;

public class EChartDual {
    public EChartDual(string label, int value1, int value2){
        Label = label;
        Value1 = value1;
        Value2 = value2;
    }

    public string Label { get; private set; }
    public int Value1 { get; private set; }
    public int Value2 { get; private set; }
}