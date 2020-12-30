using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseStation Base = null;
        User user = null;
        //string x ="1";
        List<PlaceholderInfoClass> InfoList = new List<PlaceholderInfoClass>();
        

        public MainWindow()
        {
            InitializeComponent();
            //TextBlock.Tex
            //dupa.Text = x;
            RadioBaza.IsChecked = false;

            if (DataBase.Open())
            {
                RadioBaza.IsChecked = true;
                DataGridUsers.ItemsSource = DataBase.UserList().DefaultView;
                Plot.plotmap(Grid2, 200, 200);
                Plot.plotStationStart(Grid2, DataBase.UserList());
                //string Data= DataBase.UserList();
            }
            else
            {
                RadioBaza.IsChecked = false;
            }
            //DataGridUsers.DataContext = DataBase.UserList();
            //Point t = PointToScreen(new Point(ActualWidth, ActualHeight));
            //TextBoxTest.Text = t;
            

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ButtonDodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {


            if (TextBoxLokalizacjaX.Text.ToString() == "" ||
                TextBoxLokalizacjaY.Text.ToString() == "" ||
                TextBoxMocNadawcza.Text.ToString() == "" ||
                TextBoxZyskAntenyN.Text.ToString() == "" ||
                TextBoxZyskAntenyO.Text.ToString() == "" ||
                TextBoxNazwaUzytkownika.Text.ToString() == "" ||
                TextBoxACLR1.Text.ToString() == "" ||
                TextBoxACLR2.Text.ToString() == "" ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxLokalizacjaX.Text, "[^0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxLokalizacjaY.Text, "[^0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxMocNadawcza.Text, "[^.,0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxZyskAntenyN.Text, "[^.,0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxZyskAntenyO.Text, "[^.,0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxACLR1.Text, "[^.,0-9]") ||
                System.Text.RegularExpressions.Regex.IsMatch(TextBoxACLR2.Text, "[^.,0-9]"))
            {
                MessageBox.Show("Wprowadź poprawnie dane(nie zostawiaj pustych komórek). Akceptowalne są tylko liczby(0-9) w polach Lokalizacja(X,Y) i Numer kanału, w polach Moc nadawcza i Zysk anteny dodatkowo dostępne są znaki('.' i ',')");
            }
            else
            {
                double _mocNadawcza = Convert.ToDouble(TextBoxMocNadawcza.Text.Replace('.', ','));
                if (ComboBoxM.Text == "µW")
                {
                    _mocNadawcza = 10 * Math.Log10((_mocNadawcza / 1000) / 0.001);
                }
                else if (ComboBoxM.Text == "mW")
                {
                    _mocNadawcza = 10 * Math.Log10((_mocNadawcza) / 0.001);
                }
                else if (ComboBoxM.Text == "kW")
                {
                    _mocNadawcza = 10 * Math.Log10((_mocNadawcza * 1000000) / 0.001);
                }
                else if (ComboBoxM.Text == "W")
                {
                    _mocNadawcza = 10 * Math.Log10((_mocNadawcza * 1000) / 0.001);
                }
                //TextBoxTest.Text = f.ToString();

                double _zyskAntenyN = Convert.ToDouble(TextBoxZyskAntenyN.Text.Replace('.', ','));
                if (ComboBoxZN.Text == "µW")
                {
                    _zyskAntenyN = 10 * Math.Log10((_zyskAntenyN / 1000) / 0.001);
                }
                else if (ComboBoxZN.Text == "mW")
                {
                    _zyskAntenyN = 10 * Math.Log10((_zyskAntenyN) / 0.001);
                }
                else if (ComboBoxZN.Text == "kW")
                {
                    _zyskAntenyN = 10 * Math.Log10((_zyskAntenyN * 1000000) / 0.001);
                }
                else if (ComboBoxZN.Text == "W")
                {
                    _zyskAntenyN = 10 * Math.Log10((_zyskAntenyN * 1000)/0.001);
                }

                double _zyskAntenyO = Convert.ToDouble(TextBoxZyskAntenyO.Text.Replace('.', ','));
                if (ComboBoxZO.Text == "µW")
                {
                    _zyskAntenyO = 10 * Math.Log10((_zyskAntenyO / 1000) / 0.001);
                }
                else if (ComboBoxZO.Text == "mW")
                {
                    _zyskAntenyO = 10 * Math.Log10((_zyskAntenyO) / 0.001);
                }
                else if (ComboBoxZO.Text == "kW")
                {
                    _zyskAntenyO = 10 * Math.Log10((_zyskAntenyO * 1000000) / 0.001);
                }
                else if (ComboBoxZO.Text == "W")
                {
                    _zyskAntenyO = 10 * Math.Log10((_zyskAntenyO * 1000) / 0.001);
                }


                DataBase.Command(string.Format("INSERT dbo.Users(user1,x,y,moc,zysk,nrkanalu) VALUES ('{0}', {1}, {2}, {3}, {4}, {5});", TextBoxNazwaUzytkownika.Text.ToString(), int.Parse(TextBoxLokalizacjaX.Text), int.Parse(TextBoxLokalizacjaY.Text), _mocNadawcza.ToString().Replace(',', '.'), _zyskAntenyN.ToString().Replace(',', '.'), int.Parse(TextBoxNumerKanalu.Text)));
                DataGridUsers.ItemsSource = DataBase.UserList().DefaultView;
                Plot.plotStation(Grid2, int.Parse(TextBoxLokalizacjaX.Text), int.Parse(TextBoxLokalizacjaY.Text));
                //DataBase.Open();
                //DataBase.Data
            }
        }
        private void ComboBoxM_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxM.SelectedItem = "dBm";
            ComboBoxM.Items.Add("µW");
            ComboBoxM.Items.Add("mW");
            ComboBoxM.Items.Add("W");
            ComboBoxM.Items.Add("kW");
            ComboBoxM.Items.Add("dBm");
        }

        private void ComboBoxZN_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxZN.SelectedItem = "dBi";
            ComboBoxZN.Items.Add("µW");
            ComboBoxZN.Items.Add("mW");
            ComboBoxZN.Items.Add("W");
            ComboBoxZN.Items.Add("kW");
            ComboBoxZN.Items.Add("dBi");
        }

        private void ComboBoxZO_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxZO.SelectedItem = "dBi";
            ComboBoxZO.Items.Add("µW");
            ComboBoxZO.Items.Add("mW");
            ComboBoxZO.Items.Add("W");
            ComboBoxZO.Items.Add("kW");
            ComboBoxZO.Items.Add("dBi");
        }

        private void ButtonUsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsers.SelectedItem == null)
            {
                MessageBox.Show("Zaznacz wiersz w tabeli, który chcesz usunąć");
                return;
            }
            if (MessageBox.Show("Czy na pewno chcesz usunąć użytkownika z systemu?", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                if (DataGridUsers.SelectedItem != null)
                {
                    Plot.deleteStation(Grid2, int.Parse((((DataRowView)DataGridUsers.SelectedItem).Row["x"]).ToString()), int.Parse((((DataRowView)DataGridUsers.SelectedItem).Row["y"]).ToString()));
                    DataBase.Command(string.Format("delete from dbo.users where ID= '{0}' ", ((DataRowView)DataGridUsers.SelectedItem).Row["ID"].ToString()));
                    DataGridUsers.ItemsSource = DataBase.UserList().DefaultView;

                    TextBoxTest.Text = "ok";
                }
            }
            else
            {
                TextBoxTest.Text = "no";
            }
            
            

        }

        //wytłumacznie zmiennych dla kontrolera, gui i bazy danych:
        //x_b - położenie stacji w osi x (zakładamy rodzaj podanych danych 0 - 200 każdemy indeksowi odpowiada 100 metrów tj. 0 - 0m 1 - 100m, 2 - 200m
        //y_b to samo co wyżej tylko w osi y 
        // przykład
        // 0 1 2 3 4
        // 1              S- stacja bazowa - to oznaczenie oznacza, że stacja bazowa znajduje się 400 metrów od prawej krawedzi i 400 metrów od górnej krawedzi
        // 2
        // 3
        // 4       S
        // antena_gain_base - zysk anteny dla stacji nadawczej
        // power_base -moc nadawcza stacji bazowej
        private void BaseStationInitialization(int x_b, int y_b,int antena_gain_base,int power_base)
        {
            // stworzenie stacji nadawczej:
            Base = new BaseStation(x_b, y_b, antena_gain_base, power_base);
        }
        // oznaczenia te same co dla stacji 
        // channel_number - numer kanału - warto aby GUI dodało sprawdzanie wpisywanych danych bo my w żaden sposob nie sprawdzamy czy kanał znajduje się w zakresie 1-10 - jeśli nie dacie rady to dajcie znać my zrobimy mechanizm sprawdzjący
        private void UserInitialization(int x_u,int y_u, int antena_gain_user,int channel_number)
        {
            user = new User(x_u, y_u, antena_gain_user, channel_number);
            ///
        }

        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var tc = sender as TabControl;
            if (TabItemMapa != null && TabItemMapa.IsSelected)
            {
                //MessageBox.Show("ładowanie mapy");
                //Plot.plot(Grid2, 10, 50, 50);

                //Do Stuff ...
            }
        }

        private void RadioBazaChecked(object sender, RoutedEventArgs e)
        {
            if((bool)RadioBaza.IsChecked == true)
            {
                RadioBaza.Content = "Połączono";
            }
            else
            {
                RadioBaza.Content = "Nie połączono";
            }
        }

        private void PlotMap_Click(object sender, RoutedEventArgs e)
        {
            //Plot.plot(Grid2, TextBoxTest);
        }
    }
}
