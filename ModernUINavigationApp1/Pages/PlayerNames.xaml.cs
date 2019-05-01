using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for PlayerNames.xaml
    /// </summary>
    public partial class PlayerNames : UserControl
    {
        public PlayerNames()
        {
            InitializeComponent();
        }

        private void PlayerSearch_Click(object sender, RoutedEventArgs e)
        {
            String sql = "Select PLAYER_NAME as Name from player" +
                " WHERE Name LIKE '" + searchFirstName.Text + "%'";
            // change visiabilty of the two tables since I will be going back and forth through them.
            list.Visibility = Visibility.Visible;
            list2.Visibility = Visibility.Collapsed;
            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);
  

            list.DataContext = dt.DefaultView;
        }
       
    }
    public class Items
    {
        public string longName { get; set; }
        public string shortName { get; set; }
    }

}
