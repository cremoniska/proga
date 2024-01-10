namespace Lab3.Models;

[Serializable]
public class Coefficient
{
    public int? First { get; set; } = null;
    public int? Second { get; set; } = null;
    public int? Third { get; set; } = null;
    
    public int Count()
    {
        var counter = 0;
        if (this.First != null) counter++;
        if (this.Second != null) counter++;
        if (this.Third != null) counter++;
        return counter;
    }

    public Coefficient()
    {
    }

    public Coefficient(int k, int b)
    {
        First = k;
        Second = b;
    }
    
    public Coefficient(int a, int b, int c)
    {
        First = a;
        Second = b;
        Third = c;
    }
}