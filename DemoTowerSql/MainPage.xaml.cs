using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoTowerSql
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DatabaseHandler databaseHandler;
        public MainPage()
        {
            this.InitializeComponent();

            databaseHandler = new DatabaseHandler();

            // create database table
            databaseHandler.InitializeDatabase();
            Path.Text = ApplicationData.Current.LocalFolder.Path;

            //databaseHandler.AddTower("Burj Khalifa", 830);

            List<Tower> towers = databaseHandler.GetTowers();
            Output.ItemsSource = towers;
        }


        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTowerPage));
        }

        private void EditData_Click(object sender, RoutedEventArgs e)
        {
            Tower selectedTower = (Tower)Output.SelectedValue;
            if (selectedTower != null)
            {
                this.Frame.Navigate(typeof(EditTowerPage), selectedTower.Id);
            }
        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            Tower selectedTower = (Tower)Output.SelectedValue;
            if (selectedTower != null)
            {
                this.Frame.Navigate(typeof(DeleteTowerPage), selectedTower.Id);
            }           
        }


    }
}
