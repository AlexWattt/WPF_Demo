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
    /// Interaction logic for PlayerRating.xaml
    /// </summary>
    public partial class PlayerRating : UserControl
    {
        public PlayerRating()
        {
            InitializeComponent();
        }
        private void PlayerSearch_Click(object sender, RoutedEventArgs e)
        {
            String sql = "Select P.player_name AS Name, Player_Attributes.overall_rating AS Rating FROM Player AS P" +
                " JOIN Player_Attributes ON P.player_api_id =  Player_Attributes.player_api_id" +
                " Group BY p.player_api_id Having Player_Attributes.overall_rating >= "+ searchRating.Text +
                " Order By Rating";
            // change visiabilty of the two tables since I will be going back and forth through them.
            list.Visibility = Visibility.Visible;
            list2.Visibility = Visibility.Collapsed;
            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);


            list.DataContext = dt.DefaultView;
        }

    }
    public class Items1
    {
        public string longName { get; set; }
        public string shortName { get; set; }
    }
}

