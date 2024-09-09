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
    public partial class FormGroupSignCrossPP : Window
    {
        private bool buttonWasClicked;
        private int indexsignStvor;
        private int countReper;

        public FormGroupSignCrossPP()
        {
            InitializeComponent();
            ComboBox1.ItemsSource = SignBase.DataPP;
            ComboBox1.Text = SignBase.DataPP.First();
            //ComboBox2.ItemsSource = SignBase.DataRiver;
            //ComboBox2.Text = SignBase.DataRiver.First();
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
        
        public int CountReper
        {
            get
            {
                /*
                if (ComboBox1.Text == SignBase.DataPP[0]) { countReper = 2; }
                if (ComboBox1.Text == SignBase.DataPP[1]) { countReper = 3; }
                if (ComboBox1.Text == SignBase.DataPP[2]) { countReper = 4; }
                return countReper;
                */
                return 1;
            }
        }

        public int IndexsignStvor
        {
            get
            {
                /*
                if (ComboBox2.Text == SignBase.DataRiver[0])
                { indexsignStvor = 51; }
                else
                { indexsignStvor = 50; }
                return indexsignStvor;
                */
                return 50;
            }
        }
    }
}
