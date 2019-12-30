using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DemoTowerSql
{
    /// <summary>
    /// Page used to add a tower.
    /// </summary>
    public sealed partial class AddTowerPage : Page
    {
        private DatabaseHandler databaseHandler;

        public AddTowerPage()
        {
            this.InitializeComponent();
            databaseHandler = new DatabaseHandler();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Height.Text, out int height))
            {
                databaseHandler.AddTower(Name.Text, height);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var messageDialog = new MessageDialog("Height must be a number.");
                await messageDialog.ShowAsync();
            }           
        }
    }
}
