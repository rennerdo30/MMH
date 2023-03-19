using Application.games;
using Application.games.HS2;
using Application.games.KKS;
using Application.ui;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

namespace Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            gameInstanceComboBox.Items.Clear();
            missingModsGrid.Columns.Clear();

            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "Name";
                textColumn.Binding = new Binding("Name");
                missingModsGrid.Columns.Add(textColumn);
            }
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "URL";
                textColumn.Binding = new Binding("URL");
                missingModsGrid.Columns.Add(textColumn);
            }
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "raw";
                textColumn.Binding = new Binding("raw");
                missingModsGrid.Columns.Add(textColumn);
            }


            gameInstanceComboBox.DisplayMemberPath = "InstallDir";

            string jsonVal = ConfigurationManager.AppSettings["games"];
            if (jsonVal != null)
            {
                List<ModdedGame> games = JsonConvert.DeserializeObject<List<ModdedGame>>(jsonVal, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                if (games != null && games.Count > 0)
                {
                    foreach (var game in games)
                    {
                        gameInstanceComboBox.Items.Add(game);
                        gameInstanceComboBox.SelectedItem = game;
                    }
                }
            }
        }

        private void missingModsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void missingModsGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            string url = ((ModdedGame)gameInstanceComboBox.SelectedItem).getURLForMod((GameMod) row.Item);
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void addGameButton_Click(object sender, RoutedEventArgs e)
        {
            var customInputDialog = new NewGameInstanceDialog();
            if (customInputDialog.ShowDialog() == true)
            {
                ModdedGame game = null;
                switch (customInputDialog.GameType)
                {
                    case "KKS":
                        game = new KKS(customInputDialog.GamePath);
                        break;
                    case "HS2":
                        game = new HS2(customInputDialog.GamePath);
                        break;
                    default:
                        break;
                }

                if (game != null)
                {
                    gameInstanceComboBox.Items.Add(game);
                    gameInstanceComboBox.SelectedItem = game;

                    //game.findMissingMods().ForEach(mod => missingModsGrid.Items.Add(mod));



                    List<ModdedGame> games = new List<ModdedGame>();
                    string jsonVal = ConfigurationManager.AppSettings["games"];
                    if (jsonVal != null)
                    {
                        games = JsonConvert.DeserializeObject<List<ModdedGame>>(jsonVal, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                    }
                    games.Add(game);

                    jsonVal = JsonConvert.SerializeObject(games, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    if (config.AppSettings.Settings["games"] != null)
                    {
                        config.AppSettings.Settings["games"].Value = jsonVal;
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("games", jsonVal);
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        private void removeGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameInstanceComboBox.Items.Remove(gameInstanceComboBox.SelectedItem);
        }

        private void gameInstanceComboBox_SelectionChanged(object box, SelectionChangedEventArgs e)
        {
            missingModsGrid.Items.Clear();

            ((ModdedGame)((ComboBox)box).SelectedItem).findMissingMods().ForEach(mod => missingModsGrid.Items.Add(mod));
        }
    }
}
