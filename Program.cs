using System;

public class CustomArray<T>
{
    private T[] array;
    private int count;

    public CustomArray(int size = 10)
    {
        array = new T[size];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == array.Length)
        {
            Array.Resize(ref array, array.Length * 2);
        }
        array[count++] = item;
    }

    public T Search(Predicate<T> condition)
    {
        return Array.Find(array, condition);
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(array, item);
        if (index >= 0)
        {
            Array.Copy(array, index + 1, array, index, count - index - 1);
            count--;
            return true;
        }
        return false;
    }

    public void Output()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
        return array[index];
    }

    // You might also want to add a property to get the current count
    public int Count
    {
        get { return count; }
    }
}

public enum SpaceshipStatus
{
    Active,
    Inactive,
    Maintenance
}

public enum MissionType
{
    Research,
    Transport,
    Military,
    Communications
}

public class Spaceship
{
    public string Name { get; set; }
    public string Model { get; set; }
    public int CrewCapacity { get; set; }
    public double MaxSpeed { get; set; }
    public SpaceshipStatus Status { get; set; }
    public DateTime LaunchDate { get; set; }
    public MissionType MissionType { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Model}): {Status}, {MissionType} mission";
    }
}

// 2D array
public class CustomMap<T>
{
    private T[,] map;
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public CustomMap(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        map = new T[rows, columns];
    }

    public void Place(T item, int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
        {
            throw new ArgumentOutOfRangeException("Invalid row or column.");
        }
        map[row, column] = item;
    }

    public T Get(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
        {
            throw new ArgumentOutOfRangeException("Invalid row or column.");
        }
        return map[row, column];
    }

    public void Remove(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
        {
            throw new ArgumentOutOfRangeException("Invalid row or column.");
        }
        map[row, column] = default(T);
    }

    public void Display()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (map[i, j] != null)
                {
                    Console.Write("[X] ");
                }
                else
                {
                    Console.Write("[ ] ");
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        CustomArray<Spaceship> fleet = new CustomArray<Spaceship>();
        CustomMap<Spaceship> spaceMap = new CustomMap<Spaceship>(5, 5);

        // Adding some ships
        Spaceship enterprise = new Spaceship { Name = "Enterprise", Model = "NCC-1701", CrewCapacity = 430, MaxSpeed = 9.8, Status = SpaceshipStatus.Active, LaunchDate = new DateTime(2245, 4, 11), MissionType = MissionType.Research };
        Spaceship voyager = new Spaceship { Name = "Voyager", Model = "NCC-74656", CrewCapacity = 141, MaxSpeed = 9.975, Status = SpaceshipStatus.Active, LaunchDate = new DateTime(2371, 1, 14), MissionType = MissionType.Research };
        fleet.Add(enterprise);
        fleet.Add(voyager);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n===== Spaceship Management System =====");
            Console.WriteLine("1. Display Fleet");
            Console.WriteLine("2. Add Ship to Fleet");  // New Option
            Console.WriteLine("3. Place Ship on Map");
            Console.WriteLine("4. Remove Ship from Map");
            Console.WriteLine("5. Display Map");
            Console.WriteLine("6. Search for a Ship");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    fleet.Output();
                    break;
                case "2":
                    AddShipToFleet(fleet);  // New Function
                    break;
                case "3":
                    PlaceShipOnMap(fleet, spaceMap);
                    break;
                case "4":
                    RemoveShipFromMap(spaceMap);
                    break;
                case "5":
                    spaceMap.Display();
                    break;
                case "6":
                    SearchShip(fleet);
                    break;
                case "7":
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

    }

    static void AddShipToFleet(CustomArray<Spaceship> fleet)
    {
        Console.WriteLine("\n===== Add New Ship to Fleet =====");

        Console.Write("Enter Ship Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Model: ");
        string model = Console.ReadLine();

        int crewCapacity = GetValidatedInput("Enter Crew Capacity: ", 10000);  // Limit to 10000 for example
        double maxSpeed = GetValidatedDoubleInput("Enter Maximum Speed (warp factor): ");

        Console.WriteLine("Choose Status:");
        Console.WriteLine("1. Active");
        Console.WriteLine("2. Inactive");
        Console.WriteLine("3. Maintenance");
        SpaceshipStatus status = (SpaceshipStatus)GetValidatedInput("Enter status: ", 4) - 1;

        Console.WriteLine("Choose Mission Type:");
        Console.WriteLine("1. Research");
        Console.WriteLine("2. Transport");
        Console.WriteLine("3. Military");
        Console.WriteLine("4. Communications");
        MissionType missionType = (MissionType)GetValidatedInput("Enter mission type: ", 5) - 1;

        Console.Write("Enter Launch Date (yyyy-mm-dd): ");
        DateTime launchDate;
        while (!DateTime.TryParse(Console.ReadLine(), out launchDate))
        {
            Console.Write("Invalid date format. Try again (yyyy-mm-dd): ");
        }

        Spaceship newShip = new Spaceship
        {
            Name = name,
            Model = model,
            CrewCapacity = crewCapacity,
            MaxSpeed = maxSpeed,
            Status = status,
            MissionType = missionType,
            LaunchDate = launchDate
        };

        fleet.Add(newShip);
        Console.WriteLine($"{name} has been added to the fleet.");
    }

    static double GetValidatedDoubleInput(string prompt)
    {
        double input;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out input) && input > 0)
            {
                break;
            }
            Console.WriteLine("Please enter a valid positive number.");
        }
        return input;
    }


    static void PlaceShipOnMap(CustomArray<Spaceship> fleet, CustomMap<Spaceship> spaceMap)
    {
        Console.WriteLine("\nSelect a ship to place:");
        for (int i = 0; i < fleet.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {fleet.Get(i)}");
        }

        int shipIndex = GetValidatedInput("Enter the number of the ship: ", fleet.Count) - 1;

        int row = GetValidatedInput("Enter the row (0-4): ", 5);
        int column = GetValidatedInput("Enter the column (0-4): ", 5);

        spaceMap.Place(fleet.Get(shipIndex), row, column);
        Console.WriteLine($"{fleet.Get(shipIndex).Name} placed at ({row}, {column}).");
    }

    static void RemoveShipFromMap(CustomMap<Spaceship> spaceMap)
    {
        int row = GetValidatedInput("Enter the row to remove the ship from (0-4): ", 5);
        int column = GetValidatedInput("Enter the column to remove the ship from (0-4): ", 5);

        spaceMap.Remove(row, column);
        Console.WriteLine($"Ship removed from ({row}, {column}).");
    }

    static void SearchShip(CustomArray<Spaceship> fleet)
    {
        Console.Write("Enter the name of the ship to search: ");
        string name = Console.ReadLine();
        Spaceship foundShip = fleet.Search(ship => ship.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (foundShip != null)
        {
            Console.WriteLine($"Found ship: {foundShip}");
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }

    static int GetValidatedInput(string prompt, int maxValue)
    {
        int input;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < maxValue)
            {
                break;
            }
            Console.WriteLine($"Please enter a valid number between 0 and {maxValue - 1}.");
        }
        return input;
    }

}