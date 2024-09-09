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

namespace GlobalSign
{
    /// <summary>
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    public partial class FormGroupSignTrassa : Window
    {
        private bool buttonWasClicked;

        //Список расстановки опознавательных знаков
        private List<string> DistanceOS{ get; } = new List<string>(){"500", "1000", "1500", "2000", "2500", "5000"};
        //Список смещения  знаков
        private List<string> DeltaOS { get; } = new List<string>() { "2", "4", "6", "8", "10", "25", "50", "80", "100" };
        //Список смещения  знаков
        private List<string> CountSign { get; } = new List<string>() { "1", "3" };


        public FormGroupSignTrassa()
        {
            InitializeComponent();

            ComboBox1.ItemsSource = DistanceOS;
            ComboBox1.Text = DistanceOS[0];
            ComboBox2.ItemsSource = DeltaOS;
            ComboBox2.Text = DeltaOS[0];

            ComboBox3.ItemsSource = CountSign;
            ComboBox3.Text = CountSign.First();
            ComboBox4.ItemsSource = DeltaOS;
            ComboBox4.Text = DeltaOS[0];

            ComboBox5.ItemsSource = DistanceOS;
            ComboBox5.Text = DistanceOS[3];
            ComboBox6.ItemsSource = DeltaOS;
            ComboBox6.Text = DeltaOS[0];

            ComboBox7.ItemsSource = DistanceOS;
            ComboBox7.Text = DistanceOS[1];
            ComboBox8.ItemsSource = DeltaOS;
            ComboBox8.Text = DeltaOS[0];

            ComboBox9.ItemsSource = DistanceOS;
            ComboBox9.Text = DistanceOS[5];
            ComboBox10.ItemsSource = DeltaOS;
            ComboBox10.Text = DeltaOS[6];
        }
        //Методы
        public bool ButtonWasClicked
        {
            get { return buttonWasClicked; }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            buttonWasClicked = true;
            Hide();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            buttonWasClicked = false;
            Close();
        }
    }
}
