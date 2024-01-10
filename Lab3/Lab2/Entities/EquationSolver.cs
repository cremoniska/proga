using System.Runtime.CompilerServices;
using Lab2.Exceptions;

namespace Lab2.Entities;

public class EquationSolver
{
    private List<Equation> equations = new List<Equation>();
    public IReadOnlyList<Equation> Equations => equations.AsReadOnly();

    public EquationSolver()
    {
    }

    public void AddEquation(Equation equation)
    {
        equation.Solve();
        equations.Add(equation);
    }

    public Equation FindEquation(int index)
    {
        if (index < 0 || index >= equations.Count)
            throw new EquationSolverException(
                $"Wrong index {index}. The value must be in [0 to {equations.Count})");
        
        return equations[index];
    }
}