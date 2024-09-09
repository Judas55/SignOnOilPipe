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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>

    public partial class FormSignPK : Window
    {
        private bool buttonWasClicked;

        public FormSignPK()
        {
            InitializeComponent();

            ComboBox1.ItemsSource = SignBase.Rack;
            ComboBox1.Text = SignBase.OneRack;
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
