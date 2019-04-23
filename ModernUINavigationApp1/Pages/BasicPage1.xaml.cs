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
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicPage1 : UserControl
    {
        public BasicPage1()
        {
            InitializeComponent();
        }

        private void TeamSearch_Click(object sender, RoutedEventArgs e)
        {
            String sql = "Select Team_long_name AS team, team_short_name AS Short_Name from team" +
                " WHERE TEAM_LONG_NAME LIKE '" + search.Text + "%' OR TEAM_SHORT_NAME LIKE '" + search.Text + "%'";
            //

            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);
            Select.Visibility = Visibility.Visible;
            // DataView dataView = new DataView(dt);
            list.DataContext = dt.DefaultView;
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
        }

        
    }
}
