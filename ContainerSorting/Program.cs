using ContainerSorting.Classes;

Console.WriteLine("Welcome to the Container ship sorting");

Console.WriteLine("Enter the ship dimensions:");
Console.Write("Length: ");
int length = int.Parse(Console.ReadLine());
Console.Write("Width: ");
int width = int.Parse(Console.ReadLine());

Ship ship = new Ship(length, width);

List<Container> containersToLoad = new List<Container>
            {
                new Container(30000, ContainerType.Coolable),              
                new Container(10000, ContainerType.Coolable),
                new Container(10000, ContainerType.Coolable),
                new Container(10000, ContainerType.Standard),
                new Container(10000, ContainerType.Standard),
                new Container(10000, ContainerType.Valuable),
                new Container(10000, ContainerType.Valuable),
                new Container(10000, ContainerType.Standard),
                new Container(28000, ContainerType.Standard),
                new Container(30000, ContainerType.Valuable),
                new Container(4000, ContainerType.Valuable),
            };

foreach (var container in containersToLoad)
{
    if (!ship.LoadContainer(container))
        Console.WriteLine($"Failed to load {container} onto the ship.");
}

Console.WriteLine("\nFinal ship layout:");
ship.PrintShipLayout();
string url = ship.SerializeToUrl();

Console.WriteLine("\nShip Syntax:\n" + url);
// ship.SaveLayoutToJson("ShipLayout.json");