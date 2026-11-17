using System;
using System.IO;
using Zoo_forvaltningssystem.Src.Util;
using MySql.Data.MySqlClient;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Zoo_forvaltningssystem
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Title = "Zoo Forvaltning";

            this.ApplyBackdrop();
            configureWindow();

            MySqlConnection database = Database.Connect();
            if (database == null) { return; }

            Zoo_forvaltningssystem.Src.Util.MenuItems.initializeAnimals(database, SelectAnimalBox);

            for (int i = 0; i < Zoo_forvaltningssystem.Src.Util.MenuItems.animalTypes.Length; i++)
            {
                SelectAnimalTypeBox.Items.Add(Zoo_forvaltningssystem.Src.Util.MenuItems.animalTypes[i]);
            }
        }

        private void configureWindow()
        {
            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "Icon.ico"));

            if (AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsResizable = false;
                presenter.IsMaximizable = false;
            }

            WindowPositioning.ResizeAndCenter(AppWindow, 440, 416);
        }

        private void RegisterAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAnimalButton.Visibility = Visibility.Collapsed;
            SelectAnimalBox.Visibility = Visibility.Collapsed;
            DeleteAnimalButton.Visibility = Visibility.Collapsed;
            SelectAnimalLabel.Visibility = Visibility.Collapsed;
            CreateAnimalName.Visibility = Visibility.Visible;
            SelectAnimalTypeBox.Visibility = Visibility.Visible;
        }

        private async void SelectAnimalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null)
            {
                // Access properties here
                var selectedItem = combo.SelectedItem;
                if (selectedItem != null)
                {
                    DeleteAnimalButton.Visibility = Visibility.Visible;
                }
            }
        }

        private async void CreateAnimalName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Access properties here
                var text = textBox.Text;
            }
        }

        private async void DeleteAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox combo = SelectAnimalBox;
            if (combo != null)
            {
                var selectedItem = combo.SelectedItem;
                combo.Text = "Deleting...";
                if (selectedItem != null)
                {
                    MenuItems.deleteAnimal(Database.Connect(), combo);
                }
            }
        }
    }
}
