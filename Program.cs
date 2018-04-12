using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace Завдання_3__Car_
{
    class Car : IComparable<Car>
    {
        private string brand;
        private int year;
        private double cost;
        public Car()
        {
            brand = "Epmty";
            year = 0;
            cost = 0d;
        }
        public Car(string _brand, int _year, double _cost)
        {
            brand = _brand;
            year = _year;
            cost = _cost;
        }
        public override string ToString()
        {
            return "Brand: " + brand + "\nYear: " + year + "\nCost: " + cost + "$";
        }
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                brand = value;
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        public int CompareTo(Car obj)
        {
            if (brand.CompareTo(obj.brand) == 1)
            {
                return 1;
            }
            if (brand.CompareTo(obj.brand) == -1)
            {
                return -1;
            }
            else
                return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List <Car> lst = new List<Car>();
            while (true)
            {
                Menu(lst);
                Console.ReadKey();
            }
        }

        public static void Menu(List<Car> lst)
        {
            string choose;
            Console.Clear();
            Console.WriteLine("<---=== CAR ===--->\n\n MENU");
            Console.WriteLine("1. Read cars\n2. Print all cars\n3. Sort\n4. Search\n5. Exit");
            choose = Console.ReadLine();
            if (choose == "1")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("<---=== READ MENU ===--->\n\n1. Read from file\n2. Read from database");
                    choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(File.Open("Cars.txt", FileMode.Open));
                            ReadFromFile(lst, sr);
                            return;
                        }
                        catch (FileNotFoundException)
                        {
                            Console.Clear();
                            Console.WriteLine("<---=== ERROR ===--->\n\nFile not found!");
                            return;
                        }
                    }
                    if (choose == "2")
                    {
                        ReadFromDataBase(lst);
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else if (choose == "2")
            {
                Console.Clear();
                if (IsEmpty(lst))
                {
                    Console.WriteLine("<---=== ERROR ===--->\n\nEmpty!");
                    return;
                }
                Console.WriteLine("<---=== ALL CARS ===--->\n\n");
                Print(lst);
            }
            else if (choose == "3")
            {
                while (true)
                {
                    Console.Clear();
                    if (IsEmpty(lst))
                    {
                        Console.WriteLine("<---=== ERROR ===--->\n\nEmpty!");
                        return;
                    }
                    Console.WriteLine("<---=== SORT MENU ===--->\n\n1. Sort by brand\n2. Sort by cost\n3. Sort by year");
                    choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        lst.Sort();
                    }
                    else if (choose == "2")
                    {
                        SortByCost(lst);
                    }
                    else if (choose == "3")
                    {
                        SortByYear(lst);
                    }
                    else
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.WriteLine("<---=== SORTED ===--->\n\n");
                    Print(lst);
                    return;
                }
            }
            else if (choose == "4")
            {
                if (IsEmpty(lst))
                {
                    Console.Clear();
                    Console.WriteLine("<---=== ERROR ===--->\n\nEmpty!");
                    return;
                }
                string _brand;
                int _year;
                double _cost;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("<---=== SEARCH MENU ===--->\n\n1. Search by brand\n2. Search by cost\n3. Search by year");
                    choose = Console.ReadLine();
                    if (choose == "1")
                    {
                        Console.Clear();
                        Console.Write("Enter the brand: ");
                        _brand = Console.ReadLine();
                        Console.Clear();
                        if (SearchByBrand(lst, _brand).Capacity != 0)
                        {
                            Console.WriteLine("<---=== SEARCHED ===--->\n\n");
                            Print(SearchByBrand(lst, _brand));
                        }
                        else
                        {
                            Console.WriteLine("<---=== ERROR ===--->\n\nCan't find the car");
                        }
                        return;
                    }
                    else if (choose == "2")
                    {
                        Console.Clear();
                        Console.Write("Enter the cost: ");
                        try
                        {
                            _cost = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("<---=== ERROR ===--->\n\nBad value!");
                            return;
                        }
                        Console.Clear();
                        if (SearchByCost(lst, _cost).Capacity != 0)
                        {
                            Console.WriteLine("<---=== SEARCHED ===--->\n\n");
                            Print(SearchByCost(lst, _cost));
                        }
                        else
                        {
                            Console.WriteLine("<---=== ERROR ===--->\n\nCan't find the car");
                        }
                        return;
                    }
                    else if (choose == "3")
                    {
                        Console.Clear();
                        Console.Write("Enter the year: ");
                        try
                        {
                            _year = Convert.ToInt32(Console.ReadLine());
                           
                        }
                        catch(FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("<---=== ERROR ===--->\n\nBad value!");
                            return;
                        }
                        Console.Clear();
                        if (SearchByYear(lst, _year).Capacity != 0)
                        {
                            Console.WriteLine("<---=== SEARCHED ===--->\n\n");
                            Print(SearchByYear(lst, _year));
                        }
                        else
                        {
                            Console.WriteLine("<---=== ERROR ===--->\n\nCan't find the car");
                        }
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else if (choose == "5")
            {
                Environment.Exit(0);
            }
        }

        public static void ReadFromFile(List<Car> lst, StreamReader sr)
        {
            string _brand;
            int _year;
            double _cost;
            while (!sr.EndOfStream)
            {
                try
                {
                    _brand = sr.ReadLine();
                    _year = Convert.ToInt32(sr.ReadLine());
                    if (!CheckYear(_year))
                    {
                        Console.WriteLine("<---=== ERROR ===--->\n\nInvalid year, check the file!");
                        Console.ReadKey();
                        lst.Clear();
                        return;
                    }
                    _cost = Convert.ToDouble(sr.ReadLine());
                    lst.Add(new Car(_brand, _year, _cost));
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("<---=== ERROR ===--->\n\nInvalid value, check the file!");
                    lst.Clear();
                    return;
                }
            }
            Console.Clear();
            Console.WriteLine("<---=== SUCCESS ===--->\n\nUploaded!");
        }

        public static void ReadFromDataBase(List<Car> lst)
        {
            string _brand;
            int _year;
            double _cost;
            SqlConnection db = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Універ\2 курс\Виробнича практика\C#\Завдання 3 (Car)\Завдання 3 (Car)\CarDatabase.mdf"";Integrated Security=True");
            try
            {
                db.Open();
            }
            catch (SqlException)
            {
                Console.Clear();
                Console.WriteLine("<---=== ERROR ===--->\n\nCan't connect to database!");
                return;
            }
            SqlCommand command = new SqlCommand("SELECT * FROM [Table]", db);
            SqlCommand count = new SqlCommand("SELECT count(*)" + "FROM [Table]", db);
            int n = Convert.ToInt32(count.ExecuteScalar());
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            for (int i = 0; i < n; ++i)
            {
                _brand = reader.GetString(0);
                _year = reader.GetInt32(1);
                if (!CheckYear(_year))
                {
                    Console.Clear();
                    Console.WriteLine("<---=== ERROR ===--->\n\nInvalid year, check the database!");
                    db.Close();
                    lst.Clear();
                    return;
                }
                _cost = reader.GetDouble(2);
                reader.Read();
                lst.Add(new Car(_brand, _year, _cost));
            }
            Console.Clear();
            Console.WriteLine("<---=== SUCCESS ===--->\n\nUploaded!");
        }

        public static void Print(List<Car> lst)
        {
            foreach (Car i in lst)
                Console.WriteLine(i + "\n");
        }

        public static void SortByCost(List<Car> lst)
        {
            lst.Sort((Car1, Car2) => Car1.Cost.CompareTo(Car2.Cost));
        }

        public static void SortByYear(List<Car> lst)
        {
            lst.Sort((Car1, Car2) => Car1.Year.CompareTo(Car2.Year));
        }

        public static List<Car> SearchByBrand(List<Car> lst, string _brand)
        {
            return lst.FindAll(x => x.Brand.Contains(_brand));
        }

        public static List<Car> SearchByCost(List<Car> lst, double _cost)
        {
            return lst.FindAll(x => x.Cost == _cost);
        }

        public static List<Car> SearchByYear(List<Car> lst, int _year)
        {
            return lst.FindAll(x => x.Year == _year);
        }

        public static bool CheckYear(int _year)
        {
            return _year <= DateTime.Now.Year;
        }

        public static bool IsEmpty(List<Car> lst)
        {
            if (lst.Capacity == 0)
            {
                return true;
            }
            return false;
        }
    }
}
