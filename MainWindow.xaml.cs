using System;
using System.IO;
using Zoo_forvaltningssystem.Src.Util;

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

            Database.Connect();
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

        }

        private async void SelectAnimalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
