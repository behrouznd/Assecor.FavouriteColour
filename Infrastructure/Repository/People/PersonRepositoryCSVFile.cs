using Contracts.People;
using CsvHelper;
using Entities.Models;
using System.Globalization;

namespace Repository.People;

internal class PersonRepositoryCSVFile : IPersonRepository
{
    private readonly string filePath;

    public PersonRepositoryCSVFile(string filePath)
    {
        this.filePath = filePath;
    }

    public IReadOnlyCollection<Person> GetAll()
    {
        var persons = new List<Person>();

        using (var reader = new StreamReader(filePath))
        {
            string? line;
            int id = 1;
            while ((line = reader.ReadLine()) != null)
            {
                var fields = line.Split(',');

                if (fields.Length > 3)
                {
                    var person = new Person
                    {
                        Id = id,
                        Name = GetName(fields),
                        LastName = GetLastName(fields),
                        Address = GetAdress(fields),
                        Color = int.TryParse(fields[^1]?.Trim(), out var color) ? color : 0
                    };
                    persons.Add(person);
                }
                id++;
            }
        }
        return persons;
    }

    Person IPersonRepository.Create(Person entity)
    {
        using (var writer = new StreamWriter(filePath, append: true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.NextRecord();
            csv.WriteField(entity.LastName);
            csv.WriteField(entity.Name);
            csv.WriteField(entity.Address);
            csv.WriteField(entity.Color);
        }
        entity.Id = GetRowCount();
        return entity;
    }

    private int GetRowCount()
    {
        int rowCount = 0;

        using (var reader = new StreamReader(filePath))
        {
            while (reader.ReadLine() != null)
            {
                rowCount++;
            }
        }
        return rowCount;
    }

    public Person? GetById(int id)
    {
        var lines = File.ReadLines(filePath);

        var fields = lines?.ElementAtOrDefault(id - 1)?.Split(',');

        if(fields is null)
        {
            return null;
        }

        var person = new Person
        {
            Id = id,
            Name = GetName(fields),
            LastName = GetLastName(fields),
            Address = GetAdress(fields),
            Color = int.TryParse(fields[^1]?.Trim(), out var color) ? color : 0
        };
        return person;
    }

    public IReadOnlyCollection<Person> GetByColor(int color)
    {
        var persons = new List<Person>();

        using (var reader = new StreamReader(filePath))
        {
            string? line;
            int id = 1;
            while ((line = reader.ReadLine()) != null)
            {
                var fields = line.Split(',');

                if (fields.Length > 3)
                {
                    if ((int.TryParse(fields[^1]?.Trim(), out var colorvalue) ? colorvalue : 0) == color)
                    {
                        var person = new Person
                        {
                            Id = id,
                            Name = GetName(fields),
                            LastName = GetLastName(fields),
                            Address = GetAdress(fields),
                            Color = color
                        };
                        persons.Add(person);
                    }
                    id++;
                }
            }
        }
        return persons;
    }

    private string? GetLastName(string[] row)
    {
        if (row.Length > 3)
        {
            return row[0]?.Trim();
        }
        else
        {
            return !int.TryParse(row[0]?.Trim(), out var namestr) && row[0]?.Trim().Split(' ').Length == 1 ?
                row[0]?.Trim() :
                string.Empty;
        }
    }

    private string? GetName(string[] row)
    {
        if (row.Length > 3)
        {
            return row[1]?.Trim();
        }
        else
        {
            return !int.TryParse(row[1]?.Trim(), out var namestr) && row[1]?.Trim().Split(' ').Length == 1 ?
                row[1]?.Trim() :
                string.Empty;
        }
    }

    private string? GetAdress(string[] row)
    {
        if (row.Length > 3)
        {
            return row[2].Trim();
        }
        else
        {
            foreach (string field in row)
            {
                var addressRow = field.Split(' ');
                if (addressRow.Length > 1)
                {
                    return int.TryParse(addressRow[0], out var postNum) ? field : string.Empty;
                }
            }
        }
        return null;
    }
}
