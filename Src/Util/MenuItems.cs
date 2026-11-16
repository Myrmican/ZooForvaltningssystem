using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Zoo_forvaltningssystem.Src.Util
{
    public class MenuItems
    {
        public static string[] animalTypes = ["Løve", "Elefant", "Zebra", "Delfin"];

        static TextInfo animalTypeTextInfo = new CultureInfo("en-US", false).TextInfo;

        public static void initializeAnimals(MySqlConnection database, ComboBox selectAnimalBox)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT id, type, name FROM zoo", database))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string type = reader.GetString("type");
                        string name = reader.GetString("name");

                        string animalType = animalTypeTextInfo.ToTitleCase(type.ToLower());

                        selectAnimalBox.Items.Add(animalType + (type.EndsWith('e') ? "n '" : "en '") + name + "'");
                    }
                }
            }
        }
    }
}