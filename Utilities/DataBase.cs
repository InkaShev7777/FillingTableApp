using System.Collections.Generic;


namespace FillingTable.Utilities
{
    internal class DataBase
    {

        List<string> GetData(string keyName)
        {
            List<string> names = new List<string> { "Anna", "Oleg", "Anton", "Angelika", "Olga", "Tom", "Bob", "Sam", "Olivia", "Andrey", "Rustam", "Ivan", "Alexandr", "Valentina", "Evgeniy" };
            List<string> countries = new List<string> { "Argentina", "Armenia", "Austria", "Brazil", "China", "Croatia", "Cyprus", "Egypt", "France", "    Germany", "Greece", "Hungary", "Italy", "Latvia", "Monaco" };
            List<string> cities = new List<string> { "Mendosa", "Salta", "Sidney", "Brazil", "Pekin", "Split", "Lefcosha", "Hurgada", "Paris", "Berlin", "Afina", "Budapest", "Milan", "Riga", "Monaco" };
            List<string> streets = new List<string> { "Mykhaila Hrushevs'koho street,11", "Bankova Street,33", "Volodymyrska Street,15", "Lyuteranska Street,12", "Plehanova street,50", "Lenina street,5", "Ivana Sokhacha,20", "Lomonosova,33", "Lesi Ukrainky,11", "Kirova,21" };

            Dictionary<string, List<string>> text = new Dictionary<string, List<string>>(); text.Add("name", names); text.Add("country", countries); text.Add("city", cities); text.Add("street", streets);

            return text[keyName];
        }
    }
}
        

