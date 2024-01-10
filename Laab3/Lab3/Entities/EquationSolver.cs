using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Lab3.Context;
using Lab3.Exceptions;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab3.Entities;

[Serializable]
public class EquationSolver
{
    private EquationContext dbContext = new EquationContext(new DbContextOptionsBuilder<EquationContext>()
        .UseSqlite("Data Source=Notebook.db")
        .Options);
    
    [JsonProperty("equations")]
    [XmlArray("equations")]
    [XmlArrayItem("equation")]
    public List<Equation> Equations { get; } = new List<Equation>();

    public EquationSolver()
    {
        dbContext.Database.EnsureCreated();
    }

    public void AddEquation(Equation equation)
    {
        equation.Solve();
        Equations.Add(equation);
    }

    public Equation FindEquation(int index)
    {
        if (Equations.Count == 0) throw new EquationSolverException("No equations");
        if (index < 0 || index >= Equations.Count)
            throw new EquationSolverException($"Choose index from [0;{Equations.Count-1})");

        return Equations[index];
    }

    public void Save(SaveMode mode,
        string filePath = "/Users/zdarovayrodi/Documents/ITMO/pe-3-sem/pe-prog-y26/cremoniska/Lab3/Lab3/data")
    {
        if (mode == SaveMode.Json)
        {
            filePath += ".json";
            var jsonEquations = JsonConvert.SerializeObject(Equations);
            File.WriteAllText(filePath, jsonEquations);
        }
        else if (mode == SaveMode.Xml)
        {
            filePath += ".xml";
            var xmlSerializer = new XmlSerializer(typeof(List<Equation>));
            using var writer = new StreamWriter(filePath);
            xmlSerializer.Serialize(writer, Equations);
        }
        else if (mode == SaveMode.Sqlite)
        {
            dbContext.Equations.AddRange(Equations);
            dbContext.SaveChanges();
        }
    }

    public void Load(SaveMode mode, string filePath = "data")
    {
        switch (mode)
        {
            case SaveMode.Json:
                LoadFromJson(filePath + ".json");
                break;
            case SaveMode.Xml:
                LoadFromXml(filePath + ".xml");
                break;
            case SaveMode.Sqlite:
                LoadFromSqlite(filePath+".sqlite");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private void LoadFromJson(string filePath)
    {
        var jsonEquations = File.ReadAllText(filePath);
        Equations.Clear();
        Equations.AddRange(JsonConvert.DeserializeObject<List<Equation>>(jsonEquations));
    }

    private void LoadFromXml(string filePath)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Equation>));
        using (var reader = new StreamReader(filePath))
        {
            Equations.Clear();
            Equations.AddRange((List<Equation>)xmlSerializer.Deserialize(reader));
        }
    }

    private void LoadFromSqlite(string filePath)
    {
        var loaded = dbContext.Equations.ToList();
        Equations.Clear();
        Equations.AddRange(loaded);
    }
}