﻿using System;
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
    public partial class TeamWins : UserControl
    {
        public TeamWins()
        {
            InitializeComponent();
        }

        private void TeamSearch_Click(object sender, RoutedEventArgs e)
        {
            String sql = "Select Team_long_name AS team, team_short_name AS Short_Name from team" +
                " WHERE TEAM_LONG_NAME LIKE '" + search.Text + "%' OR TEAM_SHORT_NAME LIKE '" + search.Text + "%'";
            // change visiabilty of the two tables since I will be going back and forth through them.
            list.Visibility = Visibility.Visible;
            list2.Visibility = Visibility.Collapsed;
            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);
            Select.Visibility = Visibility.Visible;
            
            list.DataContext = dt.DefaultView;
            Header.Text = "Now Please select a team and hit select to see which teams have more wins then them";
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = list.SelectedItem as DataRowView;
          
            list.Visibility = Visibility.Collapsed;
            list2.Visibility = Visibility.Visible;
           
            String sql = "SELECT count(*) AS Wins, team_long_name as Team FROM ( SELECT A.team_long_name FROM( SELECT C.team_long_name, C.home_team_goal, C.away_team_goal FROM (SELECT MATCH.home_team_goal,MATCH.away_team_goal, HOME_TEAM.team_long_name FROM TEAM AS HOME_TEAM JOIN MATCH ON MATCH.home_team_api_id = HOME_TEAM.team_api_id ) AS C WHERE C.home_team_goal > C.away_team_goal ) AS A UNION ALL SELECT A.team_long_name FROM (SELECT C.team_long_name, C.home_team_goal, C.away_team_goal FROM (SELECT MATCH.home_team_goal,MATCH.away_team_goal, away_TEAM.team_long_name FROM TEAM AS AWAY_TEAM JOIN MATCH ON MATCH.AWAY_team_api_id = AWAY_TEAM.team_api_id ) AS C WHERE C.home_team_goal < C.away_team_goal ) AS A ) GROUP BY team_long_name HAVING Wins > (SELECT (SELECT COUNT(*) FROM ( SELECT TEAM_API_ID FROM TEAM WHERE TEAM_LONG_NAME = '"+ row[0] + "') AS HOME_TEAM JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) + (SELECT COUNT(*) FROM ( SELECT TEAM_API_ID FROM TEAM WHERE TEAM_LONG_NAME = '"+row[0]+"') AS AWAY_TEAM JOIN MATCH ON MATCH.AWAY_TEAM_API_ID = AWAY_TEAM.TEAM_API_ID WHERE MATCH.HOME_TEAM_GOAL < MATCH.AWAY_TEAM_GOAL) AS SumCount) ORDER by team_long_name";
            DataAccess.ExecuteSQL(sql);
            DataTable dt = DataAccess.GetDataTable(sql);
            Select.Visibility = Visibility.Visible;
            list2.DataContext = dt.DefaultView;
            String sqlTeam = "SELECT (SELECT COUNT(*) FROM ( SELECT TEAM_API_ID FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS HOME_TEAM JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) + (SELECT COUNT(*) FROM ( SELECT TEAM_API_ID FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS AWAY_TEAM JOIN MATCH ON MATCH.AWAY_TEAM_API_ID = AWAY_TEAM.TEAM_API_ID WHERE MATCH.HOME_TEAM_GOAL < MATCH.AWAY_TEAM_GOAL) AS SumCount;";
            DataAccess.ExecuteSQL(sqlTeam);
            DataTable dt1 = DataAccess.GetDataTable(sqlTeam);
            String num = dt1.Rows[0][0].ToString();
            String newHeader = row[0] + " has " + num + " games won";

            Header.Text = newHeader;



        }

        
    }
    public class Item
    {
        public string longName { get; set; }
        public string shortName { get; set; }
    }
}
