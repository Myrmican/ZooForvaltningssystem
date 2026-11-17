using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
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

        public static void deleteAnimal(MySqlConnection database, ComboBox combo)
        {
            object selectedItem = combo.SelectedItem;
            if (selectedItem != null)
            {
                string query = "DELETE FROM zoo WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, database))
                {
                    command.Parameters.AddWithValue("@Id", combo.SelectedIndex);

                    try
                    {
                        database.Open();
                        command.ExecuteNonQuery();

                        combo.Items.Remove(selectedItem);

                        combo.Text = "Suc";
                    }
                    catch (Exception ex)
                    {
                        combo.Text = $"Error: {ex.Message}";
                    }
                }
            }
        }
    }
}