using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using static ÉÁj.CONSTS;

namespace ÉÁj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnLepes.Click += (_, _) => Lepked();
            Letrehoz();
        }

        private void Letrehoz()
        {
            var lehetseges = Enumerable.Range(0, OSZLOPOKSZAMA * SOROKSZAMA).ToList();

            for (int i = 0; i < OSZLOPOKSZAMA; i++)
                grid.RowDefinitions.Add(new());


            for (int i = 0; i < OSZLOPOKSZAMA; i++)
                grid.ColumnDefinitions.Add(new());


            for (int i = 0; i < LEPKEDOKSZAMA; i++)
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

            for (int i = 0; i < LEKEKSZAMA; i++)
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

        private void Lepked()
        {   
            for (int i = 0; i < AI.EAjok.Count; i++)
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
    }

    public static class CONSTS
    {
        public const int OSZLOPOKSZAMA = 30;
        public const int SOROKSZAMA = 15;
        public const int LEKEKSZAMA = 10;
        public const int LEPKEDOKSZAMA = 150;

        public static readonly Random RAND = new();
    }
}
