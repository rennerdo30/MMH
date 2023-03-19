using Application.games;
using Application.games.HS2;
using Application.ui;
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


            ModdedGame moddedGame = new HS2("../../../../test_data/HS2");

            moddedGame.findMissingMods().ForEach(mod => missingModsGrid.Items.Add(mod));
        }

        private void missingModsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addGameButton_Click(object sender, RoutedEventArgs e)
        {
            var customInputDialog = new CustomInputDialog();
            if (customInputDialog.ShowDialog() == true)
            {
                var inputText = customInputDialog.InputText;
                // Do something with the input text

                missingModsGrid.Items.Add(new GameMod(inputText, "", ""));
            }
        }

        private void removeGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameInstanceComboBox.Items.Remove(gameInstanceComboBox.SelectedItem);
        }
    }
}
