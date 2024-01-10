using Lab2.Entities;
using Lab2.Exceptions;
using System.Web.Script.Serialization;

namespace Lab2;

class Program
{
    static void Main()
    {
        var equationSolver = new EquationSolver();

        while (true)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("Use one of commands:");
            Console.WriteLine("\"solve\" to select the type of an equation and solve it");
            Console.WriteLine("\"find\" to get an existing solution from memory");
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
            double k = double.Parse(Console.ReadLine());

            Console.Write("b=");
            double b = double.Parse(Console.ReadLine());

            var equation = new Equation(1, new List<double> { k, b });
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
            double a = double.Parse(Console.ReadLine());

            Console.Write("b=");
            double b = double.Parse(Console.ReadLine());

            Console.Write("c=");
            double c = double.Parse(Console.ReadLine());

            var equation = new Equation(2, new List<double> { a, b, c });
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
        Console.Write("Input the index: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > equationSolver.Equations.Count)
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
         var json = new JavaScriptSerializer().Serialize(obj);
        Console.WriteLine(json);
    }
}
