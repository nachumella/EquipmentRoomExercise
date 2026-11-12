okusing System;


class EquipmentUnavailableException : Exception
{
    public int RequestedAmount { get; }
    public int AvailableAmount { get; }

    public EquipmentUnavailableException(string message, int amount, int availableComputers) : base(message)
    {
        RequestedAmount = amount;
        AvailableAmount = amount - availableComputers;
    }
}

class EquipmentRoom
{
    private int availableComputers;

    public int AvailableComputers
    {
        get
        {
            return availableComputers;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Available computers cant be negative");
            }

            availableComputers = value;
        }
    }

    public EquipmentRoom(int amount)
    {
        AvailableComputers = amount;
    }

    public void Borrow(int amount)
    {
        if (amount > AvailableComputers)
        {
            throw new EquipmentUnavailableException(
                "Not enough computers available",
                amount,
                AvailableComputers);
        }

        AvailableComputers -= amount;
        Console.WriteLine($"{amount} computers were borrowed successfully.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        int initialComputers = 10;

        EquipmentRoom room = new EquipmentRoom(initialComputers);

        try
        {
            int negativeComputers = -5;
            EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            room.Borrow(4);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Requested: " + ex.RequestedAmount);
            Console.WriteLine("Missing: " + ex.AvailableAmount);
        }

        Console.WriteLine("ממשיכים בתוכנית ...");

        try
        {
            room.Borrow(8);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Requested: " + ex.RequestedAmount);
            Console.WriteLine("Missing: " + ex.AvailableAmount);
        }

        Console.WriteLine("מספר המחשבים הזמינים : " + room.AvailableComputers);
    }
}

