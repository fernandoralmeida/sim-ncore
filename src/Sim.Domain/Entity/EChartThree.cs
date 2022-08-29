namespace Sim.Domain.Entity;

public class EChartThree {
    public EChartThree(string label, int value1, int value2, int value3){
        Label = label;
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
    }

    public string Label { get; private set; }
    public int Value1 { get; private set; }
    public int Value2 { get; private set; }
    public int Value3 { get; private set; }
}