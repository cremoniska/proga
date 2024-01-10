using Lab3.Entities;
using Lab3.Models;

namespace Lab3.Tests;

using System.Collections.Generic;
using System.IO;
using Xunit;

public class EquationSolverTests
{
    [Fact]
    public void SaveAndLoadJson_ShouldMatchOriginalEquations()
    {
        var equationSolver = new EquationSolver();
        var equation1 = new Equation(1, new Coefficient(2, 3 ));
        var equation2 = new Equation(2, new Coefficient(1, 2, 1 ));
        equationSolver.AddEquation(equation1);
        equationSolver.AddEquation(equation2);
        
        equationSolver.Save(SaveMode.Json, "test.json");
        var loadedSolver = new EquationSolver();
        loadedSolver.Load(SaveMode.Json, "test.json");
        
        Assert.Equal(equationSolver.Equations, loadedSolver.Equations);
        File.Delete("test.json"); // Clean up
    }

    [Fact]
    public void SaveAndLoadXml_ShouldMatchOriginalEquations()
    {
        var equationSolver = new EquationSolver();
        var equation1 = new Equation(1, new Coefficient(2, 3));
        var equation2 = new Equation(2, new Coefficient(1, 2, 1));
        equationSolver.AddEquation(equation1);
        equationSolver.AddEquation(equation2);
        
        equationSolver.Save(SaveMode.Xml, "test.xml");
        var loadedSolver = new EquationSolver();
        loadedSolver.Load(SaveMode.Xml, "test.xml");

        Assert.Equal(equationSolver.Equations, loadedSolver.Equations);
        File.Delete("test.xml"); // Clean up
    }

    [Fact]
    public void SaveAndLoadSqlite_ShouldMatchOriginalEquations()
    {
        var equationSolver = new EquationSolver();
        var equation1 = new Equation(1, new Coefficient(2, 3));
        var equation2 = new Equation(2, new Coefficient(1, 2, 1));
        equationSolver.AddEquation(equation1);
        equationSolver.AddEquation(equation2);
        Assert.Equal(2, equationSolver.Equations.Count);
        
        equationSolver.Save(SaveMode.Sqlite);
        var loadedSolver = new EquationSolver();
        loadedSolver.Load(SaveMode.Sqlite);
        
        Assert.Equal(equationSolver.Equations, loadedSolver.Equations);
        Assert.Equal(2, equationSolver.Equations.Count);
    }
}
