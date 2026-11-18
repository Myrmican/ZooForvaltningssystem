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

        public static void addAnimal(MySqlConnection database, ComboBox combo, string animalType, string animalName)
        {
            string query = "INSERT INTO zoo (type, name) VALUES (@Type, @Name)";
            using (MySqlCommand command = new MySqlCommand(query, database))
            {
                command.Parameters.AddWithValue("@Type", animalType);
                command.Parameters.AddWithValue("@Name", animalName);
                try
                {
                    command.ExecuteNonQuery();
                    string animalTypeFormatted = animalTypeTextInfo.ToTitleCase(animalType.ToLower());
                    combo.Items.Add(animalTypeFormatted + (animalType.EndsWith('e') ? "n '" : "en '") + animalName + "'");
                }
                catch (Exception ex)
                {
                    combo.Text = $"Error: {ex.Message}";
                }
                finally
                {
                    database.Close();
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
                        command.ExecuteNonQuery();

                        combo.Items.Remove(selectedItem);
                    }
                    catch (Exception ex)
                    {
                        combo.Text = $"Error: {ex.Message}";
                    } finally
                    {
                        database.Close();
                    }
                }
            }
        }
    }
}