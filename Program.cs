using System;
namespace PrivateMultipleObjects
{
    // Base class: Ship
    class Ship
    {
        private int _Id;                    // Ship id #
        private string _Name = "Ship";      // ship name
        private Nation _Nation;             // ship nationality
        private int _Speed;                 // speed factor

        // default constructor
        public Ship()
        {
            _Id = 0;
            _Name = "Ship";
            _Nation = Nation.USA; // Fixing the type conversion error
        }

        // parameterized constructor
        public Ship(int id, string name, Nation nation, int speed) 
        { 
            this._Id = id;
            this._Name = name;  
            this._Nation = nation;  
            this._Speed = speed;    
        }

        // Enum for different nationalities
        public enum Nation
        {
            Australia, Britain, France, Germany, Italy,
            Japan, Netherlands, USA, USSR
        }   
        
        //Enum for different hull designations - warships only
        protected enum Hull { BB, BC, CA, CL, CV, CVL, DD }

        public void setId(int id) { this._Id = id; }
        public int getId() { return _Id; }
        public void setName(string name) { this._Name = name; }
        public string getName() { return _Name; }

        public void setSpeed(int speed) { this._Speed = speed; }
        public int getSpeed() { return _Speed; }
        public void setNation(Nation nation) { this._Nation = nation; }
        public Nation getNation() { return _Nation; }

        public virtual void addChange()
        {
            Console.WriteLine("Enter an integer ID Number:");
            Console.Write("ID=");
            int tempId;
            while (!int.TryParse(Console.ReadLine(), out tempId) || tempId <= 0)
                Console.WriteLine("Please enter a valid positive integer");
            setId(tempId);

            Console.Write("Name=");
            setName(Console.ReadLine());
            
            Console.WriteLine("1 = Australia, 2 = Britain, 3 = France, 4 = Germany");
            Console.WriteLine("5 = Italy, 6 = Japan, 7 = Netherlands, 8 = USA 9 = USSR" );
            Console.Write("Nation=");
            setNation((Nation)int.Parse(Console.ReadLine())-1);

            Console.Write("Speed=");
            setSpeed(int.Parse(Console.ReadLine()));
        }

        public virtual void print()
        {
            Console.WriteLine($"      ID: {getId()}");
            Console.WriteLine($"    Name: {getName()}");
            Console.WriteLine($"  Nation: {getNation()}");
            Console.WriteLine($"   Speed: {getSpeed()}");
        }
    }

    class Battleship : Ship 
    {
        private int _Attack;                // attack factor
        private int _Defense;               // defense factor

        private Hull _Hull;

        // default constructor
        public Battleship() : base() 
        { 
            _Attack = 1;
            _Defense = 1;
            _Hull = Hull.BB;
        }
        // parameterized constructor
        public Battleship(int id, string name, Nation nation, 
            int speed, int attack, int defense) 
            :base(id, name, nation, speed) 
        { 
            _Attack = attack;
            _Defense = defense;
            _Hull = Hull.BB;
        }
        public override void print()
        {
            base.print();        
            Console.WriteLine($" Attack Factor: {getAttack()}" );
            Console.WriteLine($"Defense Factor: {getDefense()}");
            Console.WriteLine($"   Warship Type: {_Hull}");
        }

        public override void addChange()
        {
            base.addChange();
            Console.Write("Attack Factor = ");
            setAttack(int.Parse(Console.ReadLine()));
            Console.Write("Defense Factor = ");
            setDefense(int.Parse(Console.ReadLine()));
        }
        public void setAttack(int attack) { this._Attack = attack; }
        public int getAttack() { return _Attack; }
        public void setDefense(int defense) { this._Defense = defense; }
        public int getDefense() { return _Defense; }
    }

    internal class Program
    {
        private static int Menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Change");
            Console.WriteLine("3. Print All");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice, please enter a number between 1 and 4.");
                Console.Write("Enter your choice: ");
            }
            return choice;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("How many ships do you want to enter (0-10)?");
            int maxShips;
            while (!int.TryParse(Console.ReadLine(), out maxShips) || (maxShips < 0) || (maxShips > 10))
                Console.WriteLine("Please enter a whole number between 0 and 10");
            Ship[] ships = new Ship[maxShips];

            Console.WriteLine("How many Battleships do you want to enter?");
            int maxBattleships;
            while (!int.TryParse(Console.ReadLine(), out maxBattleships) || (maxBattleships < 0) || (maxBattleships > 10))
                Console.WriteLine("Please enter a whole number between 0 and 10");
            Battleship[] battleships = new Battleship[maxBattleships];

            int choice, rec, type;
            int shipCount = 0, battleshipCount = 0;
            choice = Menu();
            while (choice != 4)
            {

                Console.WriteLine("Enter 1 for Ship, 2 for Battleship");
                while (!int.TryParse(Console.ReadLine(), out type) || (type != 1 && type != 2))
                    Console.WriteLine("1 for Ship, 2 for Battleship");
                try
                {
                    switch (choice)
                    {
                        case 1: // Add
                            if (type == 1) // Ship
                            {
                                if (shipCount < maxShips)
                                {
                                    ships[shipCount] = new Ship();
                                    ships[shipCount].addChange();
                                    shipCount++;
                                }
                                else
                                    Console.WriteLine("The maximum number of ships has been entered");
                            }
                            else // Battleship 
                            {
                                if (battleshipCount < maxBattleships)
                                {
                                    battleships[battleshipCount] = new Battleship();
                                    battleships[battleshipCount].addChange();
                                    battleshipCount++;
                                }
                                else
                                    Console.WriteLine("The maximum number of Battleships has been entered");
                            }
                            break;
                        case 2:     // Change
                            if (type == 1)  // Ship
                            {
                                Console.WriteLine("Ship Record Number Listing");
                                for (int i = 0; i < shipCount; i++)
                                {
                                    Console.WriteLine($"Rec#: {i + 1} " +
                                        $" | ID#: {ships[i].getId()}" +
                                        $" | Ship Name: {ships[i].getName()}" +
                                        $" | Nation: {ships[i].getNation()}" + 
                                        $" | Speed: {ships[i].getSpeed()}");
                                }
                                Console.WriteLine("----------------------------------");
                                Console.Write("Enter the record number you want to change (or 0 to quit): ");
                                while (!int.TryParse(Console.ReadLine(), out rec))
                                    Console.Write("Not an integer. Enter the record number you want to change: ");
                                while (rec > shipCount || rec < 0)
                                {
                                    Console.WriteLine("The number you entered was out of range, try again");
                                    rec = int.Parse(Console.ReadLine());
                                }
                                if (rec != 0)           // Skip it if user enters "0"
                                    ships[rec-1].addChange();
                            }
                            else    // Battleship
                            {
                                Console.WriteLine("Ship Record Number Listing");
                                for (int i = 0; i < battleshipCount; i++)
                                {
                                    Console.WriteLine($"Rec#: {i + 1} " +
                                        $" | ID#: {battleships[i].getId()}" +
                                        $" | Ship Name: {battleships[i].getName()}" +
                                        $" | Nation: {battleships[i].getNation()}" +
                                        $" | Speed: {battleships[i].getSpeed()}" +
                                        $" | Attack: {battleships[i].getAttack()}" +
                                        $" | Defense: {battleships[i].getDefense()}");
                                }
                                Console.WriteLine("----------------------------------");
                                Console.Write("Enter the record number you want to change (or 0 to quit): ");
                                while (!int.TryParse(Console.ReadLine(), out rec))
                                    Console.Write("Not an integer. Enter the record number you want to change: ");
                                while (rec > battleshipCount || rec < 0)
                                {
                                    Console.WriteLine("The number you entered was out of range, try again");
                                    rec = int.Parse(Console.ReadLine());
                                }
                                if (rec != 0)           // Skip it if user enters "0"
                                    battleships[rec-1].addChange();
                            }
                            break;
                        case 3:                 // Print All
                            if (type == 1)      // Ship
                            {
                                if (shipCount > 0)
                                {
                                    for (int i = 0; i < shipCount; i++)
                                    {
                                        Console.WriteLine($"Ship Rec # {i + 1}");
                                        ships[i].print();
                                        Console.WriteLine("..........");
                                    }
                                }
                                else Console.WriteLine("There are no Ship records to print");
                            }
                            else                // Battleship
                            {
                                if (battleshipCount > 0) 
                                {
                                    for (int i = 0; i < battleshipCount; i++) 
                                    {
                                        Console.WriteLine($"Battleship Rec # {i + 1}");
                                        battleships[i].print();
                                        Console.WriteLine("..........");
                                    }                               
                                }
                                else Console.WriteLine("There are no Battleship records to print");
                            }
                            break;
                        default:
                            Console.WriteLine("You made an invalid selection, please try again");
                            break;
                    }

                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                choice = Menu();
            }
        }
    }
}