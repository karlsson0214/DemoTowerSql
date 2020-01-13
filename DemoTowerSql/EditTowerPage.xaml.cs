using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// A page used to edit a tower.
    /// </summary>
    public sealed partial class EditTowerPage : Page
    {
        private DatabaseHandler databaseHandler;
        private Tower tower;

        public EditTowerPage()
        {
            this.InitializeComponent();
            databaseHandler = new DatabaseHandler();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is long)
            {
                long id = (long)e.Parameter;
                // display tower with this id
                tower = databaseHandler.GetTower(id);
                if (tower != null)
                {
                    Name.Text = tower.Name;
                    Height.Text = tower.Height.ToString();
                }
            }
            base.OnNavigatedTo(e);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tower != null)
            {
                tower.Name = Name.Text;
                if (int.TryParse(Height.Text, out int height))
                {
                    tower.Height = height;
                }
                databaseHandler.EditTower(tower);
                
            }
            this.Frame.Navigate(typeof(MainPage));

        }
    }
}
