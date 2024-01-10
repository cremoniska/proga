using Lab3.Entities;
using Lab3.Exceptions;
using Lab3.Models;

namespace Lab3;

class Program
{
    static void Main()
    {
        var equationSolver = new EquationSolver();

        while (true)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\"solve\" to select the type of an equation and solve it");
            Console.WriteLine("\"find\" to get an existing solution from memory");
            Console.WriteLine("\"save\" to save equations");
            Console.WriteLine("\"load\" to load equations");
            Console.WriteLine("\"quit\" to exit");
            Console.WriteLine("-----");
            Console.Write("Input the command: ");

            string? command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "solve":
                    SolveEquation(equationSolver);
                    break;
                case "find":
                    FindSolution(equationSolver);
                    break;
                case "save":
                    SaveEquations(equationSolver);
                    break;
                case "load":
                    LoadEquations(equationSolver);
                    break;
                case "quit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid command. Try again.");
                    break;
            }
        }
    }

    static void SolveEquation(EquationSolver equationSolver)
    {
        Console.WriteLine("Select the type of equation:");
        Console.WriteLine("1 - k*x + b = 0");
        Console.WriteLine("2 - a*x^2 + b*x + c = 0");

        int equationType;
        while (!int.TryParse(Console.ReadLine(), out equationType) || (equationType != 1 && equationType != 2))
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }

        Console.WriteLine($"Input the factors for equation");

        if (equationType == 1)
        {
            Console.Write("k=");
            var k = int.Parse(Console.ReadLine());

            Console.Write("b=");
            var b = int.Parse(Console.ReadLine());

            var equation = new Equation(1, new Coefficient( k, b));
            try
            {
                equationSolver.AddEquation(equation);
            }
            catch (EquationException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine(equation);
        }
        else if (equationType == 2)
        {
            Console.Write("a=");
            var a = int.Parse(Console.ReadLine());

            Console.Write("b=");
            var b = int.Parse(Console.ReadLine());

            Console.Write("c=");
            var c = int.Parse(Console.ReadLine());

            var equation = new Equation(2, new Coefficient( a, b, c ));
            try
            {
                equationSolver.AddEquation(equation);
            }
            catch (EquationException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine(equation);
        }
    }

    static void FindSolution(EquationSolver equationSolver)
    {
        if (equationSolver.Equations.Count == 0)
        {
            Console.WriteLine("No Equations");
            return;
        }
        
        Console.Write("Input the index: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index > equationSolver.Equations.Count)
        {
            Console.WriteLine($"Invalid index. Please enter a valid index between 1 and {equationSolver.Equations.Count}.");
        }

        var equation = equationSolver.FindEquation(index - 1);
        if (equation != null)
        {
            Console.WriteLine($"#{index}: {equation}");
        }
        else
        {
            Console.WriteLine("Equation not found.");
        }
    }
    
    static void SaveEquations(EquationSolver equationSolver)
    {
        Console.WriteLine("Select the mode to save:");
        Console.WriteLine("1 - Json");
        Console.WriteLine("2 - Xml");
        Console.WriteLine("3 - Sqlite");

        int saveMode;
        while (!int.TryParse(Console.ReadLine(), out saveMode) || (saveMode < 1 || saveMode > 3))
        {
            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
        }

        switch (saveMode)
        {
            case 1:
                equationSolver.Save(SaveMode.Json);
                break;
            case 2:
                equationSolver.Save(SaveMode.Xml);
                break;
            case 3:
                equationSolver.Save(SaveMode.Sqlite);
                break;
            default:
                Console.WriteLine("Invalid save mode.");
                break;
        }
    }

    static void LoadEquations(EquationSolver equationSolver)
    {
        Console.WriteLine("Select the mode to load:");
        Console.WriteLine("1 - Json");
        Console.WriteLine("2 - Xml");
        Console.WriteLine("3 - Sqlite");

        int loadMode;
        while (!int.TryParse(Console.ReadLine(), out loadMode) || (loadMode < 1 || loadMode > 3))
        {
            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
        }

        switch (loadMode)
        {
            case 1:
                equationSolver.Load(SaveMode.Json);
                break;
            case 2:
                equationSolver.Load(SaveMode.Xml);
                break;
            case 3:
                equationSolver.Load(SaveMode.Sqlite);
                break;
            default:
                Console.WriteLine("Invalid load mode.");
                break;
        }
    }
}