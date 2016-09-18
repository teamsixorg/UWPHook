﻿using System;
using System.Diagnostics;
using System.Windows;

namespace UWPHook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameModel gamesView;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gamesView = new GameModel();
            listView.ItemsSource = gamesView.games;

            var argument = Environment.GetCommandLineArgs();
            string argumentGame = "";
            for (int i = 1; i < argument.Length; i++)
            {
                argumentGame += argument[i] + " ";
            }

            if (argument != null)
            {
                foreach (Game game in gamesView.games)
                {
                    if (game.game_alias.ToLower() == argumentGame.ToLower().Trim())
                    {
                        Process.Start(@"shell:AppsFolder\" + game.game_path);
                        break;
                    }                        
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            gamesView.Add(new Game { game_alias = alias_textBox.Text, game_path = path_textBox.Text });
            gamesView.Store();
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gamesView.games.RemoveAt(listView.SelectedIndex);
            gamesView.Store();
        }
    }
}