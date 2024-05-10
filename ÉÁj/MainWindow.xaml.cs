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

            Letrehoz();
        }

        private void Letrehoz()
        {
            for (int i = 0; i < OSZLOPOKSZAMA; i++)
                grid.RowDefinitions.Add(new());


            for (int i = 0; i < OSZLOPOKSZAMA; i++)
                grid.ColumnDefinitions.Add(new());


            for (int i = 0; i < LEPKEDOKSZAMA; i++)
            {
            General:
                int koord = RAND.Next(OSZLOPOKSZAMA * SOROKSZAMA);
                if (AI.Foglalt.Contains(koord))
                    goto General;

                AI ai = new(koord % OSZLOPOKSZAMA, koord / OSZLOPOKSZAMA, koord);

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
            General:
                int koord = RAND.Next(OSZLOPOKSZAMA * SOROKSZAMA);
                if (AI.Foglalt.Contains(koord))
                    goto General;

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
            AI.EAjok.ForEach(ai =>
            {

            });
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
