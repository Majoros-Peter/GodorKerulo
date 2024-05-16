using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static ÉÁj.CONSTS;

namespace ÉÁj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(.75) };

        public MainWindow()
        {
            InitializeComponent();

            Letrehoz();

            timer.Tick += Lepked;
            btnLepes.Click += Lepked;
        }

        private void Letrehoz()
        {
            var lehetseges = Enumerable.Range(0, OSZLOPOKSZAMA * SOROKSZAMA).ToList();

            for(int i = 0; i < SOROKSZAMA; i++)
                grid.RowDefinitions.Add(new());

            for(int i = 0; i < OSZLOPOKSZAMA; i++)
                grid.ColumnDefinitions.Add(new());

            for(int i = 0; i < LEPKEDOKSZAMA; i++)
            {
                int koord = lehetseges[RAND.Next(lehetseges.Count)];
                lehetseges.Remove(koord);

                AI ai = new(koord);

                Button btn = new()
                {
                    Content = koord
                };

                grid.Children.Add(btn);

                Grid.SetColumn(btn, ai.X);
                Grid.SetRow(btn, ai.Y);
            }

            for(int i = 0; i < LEKEKSZAMA; i++)
            {
                int koord = lehetseges[RAND.Next(lehetseges.Count)];
                lehetseges.Remove(koord);

                AI.Jukak.Add(AI.IntToPoint(koord));

                Ellipse Juk = new()
                {
                    Fill = new SolidColorBrush(Colors.Coral)
                };

                grid.Children.Add(Juk);
                Grid.SetColumn(Juk, koord % OSZLOPOKSZAMA);
                Grid.SetRow(Juk, koord / OSZLOPOKSZAMA);
            }
        }

        private void Lepked(object sender, EventArgs e)
        {
            for(int i = 0; i < AI.EAjok.Count; i++)
            {
                AI ai = AI.EAjok[i];

                System.Drawing.Point honnan = ai.Coord;
                ai.Coord = ai.Lepes();

                Button btn = grid.Children.OfType<Button>().First(G => Grid.GetColumn(G) == honnan.X && Grid.GetRow(G) == honnan.Y);

                if(ai.X == -1 || ai.Y == -1)
                {
                    grid.Children.Remove(btn);
                    continue;
                }

                Grid.SetColumn(btn, ai.X);
                Grid.SetRow(btn, ai.Y);
            }

            AI.EAjok = AI.EAjok.Where(G => !(G.X == -1 || G.Y == -1)).ToList();
        }

        private void Automata_Mode(object sender, RoutedEventArgs e)
        {
            timer.Start();
            btnLepes.IsEnabled = false;
        }

        private void Manual_Mode(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            btnLepes.IsEnabled = true;
        }
    }

    public static class CONSTS
    {
        public const int OSZLOPOKSZAMA = 30;
        public const int SOROKSZAMA = 15;
        public const int LEKEKSZAMA = 20;
        public const int LEPKEDOKSZAMA = 100;

        public static readonly Random RAND = new();
    }
}
