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
    /// Логика взаимодействия для FormGroupSignCrossLW.xaml
    /// </summary>
    public partial class FormGroupSignCrossLW : Window
    {

        private bool buttonWasClicked;

        public FormGroupSignCrossLW()
        {
            InitializeComponent();
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
