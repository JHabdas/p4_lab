using Newtonsoft.Json;

namespace p4_lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "cars.json";

            string fileData = File.ReadAllText(path);

            List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(fileData);

            LQ(cars);
        }

        static void LQ(List<Car> cars)
        {
            //Zapytanie nr.1 - Wyswietlenie liczby samochodow wedlug marki
            Console.WriteLine("ZAPYTANIE NR.1");
            var brandCount = cars.GroupBy(x => x.car_brand).Select(v => new { car_brand = v.Key, Count = v.Count() });
            Console.WriteLine("Liczba samochodow kazdej marki (Marka - Liczba samochodow): ");
            foreach (var brand in brandCount)
            {
                Console.WriteLine(brand.car_brand + " - " + brand.Count);
            }
            Console.WriteLine();

            //Zapytanie nr.2 - Znalezienie marki z najwieksza liczba pojazdow z wykorzystaniem poprzedniego zapytania
            Console.WriteLine("ZAPYTANIE NR.2");
            var bestBrand = brandCount.OrderByDescending(x => x.Count).FirstOrDefault();
            Console.WriteLine("Marka z najwieksza liczba samochodow: " + bestBrand.car_brand + "(" + bestBrand.Count + " pojazdow)");
            Console.WriteLine();

            //Zapytanie nr.3 - Wyswietlenie pojazdow o kolorze czarnym oraz wyprodukowanych przed 2000 rokiem
            Console.WriteLine("ZAPYTANIE NR.3");
            var blackOldCars = cars.Where(x => x.color == "black" && x.car_year < 2000);
            Console.WriteLine("Czarne samochody wyprodukowane przed 2000r.: ");
            foreach (var car in blackOldCars)
            {
                Console.WriteLine(car.car_brand + " " + car.car_model + " Rok: " + car.car_year + " Kolor: " + car.color);
            }
        }
    }
}
