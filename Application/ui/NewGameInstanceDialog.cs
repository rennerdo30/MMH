using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Application.ui
{
    internal class NewGameInstanceDialog : Window
    {
        public string GameType { get; set; }
        public string GamePath { get; set; }

        public NewGameInstanceDialog()
        {
            Title = "Add New Game Instance";
            Width = 250;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var stackPanel = new StackPanel();

            var gameTypeCB = new ComboBox();
            gameTypeCB.Items.Add("KKS");
            gameTypeCB.Items.Add("HS2");
            stackPanel.Children.Add(gameTypeCB);



            var gamePath = new TextBox();
            stackPanel.Children.Add(gamePath);

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var okButton = new Button
            {
                Content = "OK",
                Width = 75,
                Margin = new Thickness(5)
            };
            okButton.Click += (sender, args) =>
            {
                GameType = (string)gameTypeCB.SelectedValue;
                GamePath = gamePath.Text;
                DialogResult = true;
            };
            buttonPanel.Children.Add(okButton);

            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 75,
                Margin = new Thickness(5)
            };
            cancelButton.Click += (sender, args) =>
            {
                DialogResult = false;
            };
            buttonPanel.Children.Add(cancelButton);

            stackPanel.Children.Add(buttonPanel);

            Content = stackPanel;
        }
    }
}
