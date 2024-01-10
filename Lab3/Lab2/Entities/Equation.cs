using Lab2.Exceptions;

namespace Lab2.Entities;

public class Equation
{
    private int Degree { get; }
    private List<double> Coefficients { get; }
    public double[] Roots { get; set; }

    public Equation(int degree, List<double> coefficients)
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
        var k = Coefficients[0];
        var b = Coefficients[1];

        if (k == 0)
        {
            Roots = new double[] { };
            // throw new EquationException("k = 0 is an Incorrect value.");
            return;
        }

        var root = -b / k;
        Roots = new double[] { root };
    }

    private void SquareSolve()
    {
        var a = Coefficients[0];
        var b = Coefficients[1];
        var c = Coefficients[2];

        var discriminant = b * b - 4 * a * c;

        if (discriminant > 0)
        {
            var root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            var root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            Roots = new double[] { root1, root2 };
        }
        else if (discriminant == 0)
        {
            var root = -b / (2 * a);
            Roots = new double[] { root };
        }
        else
        {
            Roots = new double[] { };
            // throw new EquationException("The equation has no solution.");
            return;
        }
    }
    
    public override string ToString()
    {
        var equationString = $"{Coefficients[0]} * x^{Coefficients.Count}";

        for (var i = 1; i < Coefficients.Count-1; i++)
        {
            equationString += $"+ {Coefficients[i]} * x^{Coefficients.Count - i} ";
        }
        equationString += $"+ {Coefficients[^1]} ";

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