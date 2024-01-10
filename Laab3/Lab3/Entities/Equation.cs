using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Serialization;
using Lab3.Exceptions;
using Lab3.Models;
using Newtonsoft.Json;

namespace Lab3.Entities;

[Serializable]
public class Equation
{
    [Key]  // primary key
    public int EquationId { get; set; } 
    
    [JsonProperty("degree")]
    [XmlAttribute("degree")]
    public int Degree { get; set; }
    
    [JsonProperty("coefficients")]
    [XmlArray("coefficients")]
    [XmlArrayItem("coefficient")]
    private Coefficient Coefficients { get; set; }
    
    [NotMapped]
    public List<double> Roots { get; set; }

    public Equation() {}
    
    public Equation(int degree, Coefficient coefficients)
    {
        Degree = degree;
        Coefficients = coefficients;
    }

    public void Solve()
    {
        if (Degree == 1) LinearSolve();
        else if (Degree == 2) SquareSolve();
        else throw new EquationException("Incorrect degree");
    }

    private void LinearSolve()
    {
        var k = Coefficients.First;
        var b = Coefficients.Second;

        if (k == 0 || (b == null || k == null))
        {
            Roots = new List<double> { };
            // throw new EquationException("k = 0 is an Incorrect value.");
            return;
        }
        
        double root = (double)((double)-b / k);
        Roots = new List<double> { root };
    }

    private void SquareSolve()
    {
        var a = (int)Coefficients.First;
        var b = (int)Coefficients.Second;
        var c = (int)Coefficients.Third;

        var discriminant = b * b - 4 * a * c;

        if (discriminant > 0)
        {
            var root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            var root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            Roots = new List<double> { root1, root2 };
        }
        else if (discriminant == 0)
        {
            var root = -b / (2 * a);
            Roots = new List<double> { root };
        }
        else
        {
            Roots = new List<double> { };
            // throw new EquationException("The equation has no solution.");
            return;
        }
    }
    
    public override string ToString()
    {
        var equationString = $"{Coefficients.First} * x^{Coefficients.Count()} ";
        if (Coefficients.Second != null)
            equationString += $"+ {Coefficients.Second} ";
        if (Coefficients.Third != null)
            equationString += $"* x + {Coefficients.Third} = 0";
        else
            equationString += "= 0";

        if (!Roots.Any())
        {
            equationString += " solution: No solution.";
            return equationString;
        }
        
        equationString += $" solution: x = {string.Join("; ", Roots)}";
        return equationString;
    }
}