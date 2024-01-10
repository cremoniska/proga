using Lab2.Entities;
using Lab2.Exceptions;

namespace Lab2.Tests;

public class EquationTests
{
    [Fact]
    public void LinearEquation_canSolveCorrectly()
    {
        var equation = new Equation(1, new List<double>{ 2, -4 });
        equation.Solve();
        
        Assert.Equal(2, equation.Roots.First());
        Assert.Single(equation.Roots);
    }

    [Fact]
    public void LinearEquation_throwsErrorWhenKIsZero()
    {
        var equation = new Equation(1, new List<double>{ 0, -4 });
        Assert.Throws<EquationException>(() => equation.Solve());
    }

    [Fact]
    public void SquareEquation_solvesWithDZeroCorrectly()
    {
        // D = 0
        var equation = new Equation(2, new List<double>{ -4, 28, -49 });
        equation.Solve();
        
        Assert.Equal(3.5, equation.Roots.First());
        Assert.Single(equation.Roots);
    }

    [Fact]
    public void SquareEquation_solvesWithDGreaterZeroCorrectly()
    {
        // D > 0
        var equation = new Equation(2, new List<double>{ 5, -6, -32 });
        equation.Solve();
        
        Assert.Equal(3.2, equation.Roots.First());
        Assert.Equal(-2, equation.Roots[1]);
        Assert.Equal(2, equation.Roots.Length);
    }
    
    [Fact]
    public void SquareEquation_throwsErrorWhenDLessZero()
    {
        // D > 0
        var equation = new Equation(2, new List<double>{ 5, 6, 2 });
        Assert.Throws<EquationException>(() => equation.Solve());
    }
}