using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Reflection;
using System.ComponentModel;
using System.Windows.Threading;

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

        DataTable DTUsers;
        DataTable DTName;
        DataTable DTSINR;
        DataTable DTSNR;
        DataGridRow row;

        public bool InvokeRequired { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            //TextBlock.Tex
            //dupa.Text = x;
            RadioBaza.IsChecked = false;

            if (DataBase.Open())
            {
                RadioBaza.IsChecked = true;
                /*
                for(int i = 0; i < 10; i++)
                {
                    DataBase.ClearDB(string.Format("dbo.Name{0}", i + 1));
                    DataBase.ClearDB(string.Format("dbo.Snr{0}", i + 1));
                    DataBase.ClearDB(string.Format("dbo.Sinr{0}", i + 1));
                }
                DataBase.Command("TRUNCATE TABLE dbo.Users2");*/
                DTUsers = DataBase.BaseTable("dbo.Users2");
                DataGridUsers.ItemsSource = DTUsers.DefaultView;

                Plot.plotmap(Grid2, 200, 200);

                DTName = DataBase.BaseTable("dbo.Name1");
                DTSINR = DataBase.BaseTable("dbo.Sinr1");
                DTSNR = DataBase.BaseTable("dbo.Snr1");
                Plot.plotStation2(Grid2, DTName, DTSINR, DTSNR);
                Thread t = new Thread(new ThreadStart(UPDATEDataGrid));
                t.Start();
            }
            else
            {
                RadioBaza.IsChecked = false;
            }

            //Thre
            //DataGridUsers.DataContext = DataBase.UserList();
            //Point t = PointToScreen(new Point(ActualWidth, ActualHeight));
            //TextBoxTest.Text = t;


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
                    _zyskAntenyN = 10 * Math.Log10((_zyskAntenyN * 1000) / 0.001);
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
                var NazwaUzytkownika = TextBoxNazwaUzytkownika.Text.ToString();
                var LokalizacjaX = int.Parse(TextBoxLokalizacjaX.Text);
                var LokalizacjaY = int.Parse(TextBoxLokalizacjaY.Text);
                var MocNadawcza = _mocNadawcza.ToString().Replace(',', '.');
                var ZyskAntenyN = _zyskAntenyN.ToString().Replace(',', '.');
                var ZyskAntenyO = _zyskAntenyO.ToString().Replace(',', '.');
                var NrKanalu = int.Parse(ComboBoxNumerKanalu.Text);
                var aclr1 = Convert.ToDouble(TextBoxACLR1.Text.Replace('.', ','));
                var aclr2 = Convert.ToDouble(TextBoxACLR2.Text.Replace('.', ','));
                string status = "oczekujacy";
                DataBase.AddUser(NazwaUzytkownika, LokalizacjaX, LokalizacjaY, MocNadawcza, ZyskAntenyN, ZyskAntenyO, NrKanalu, aclr1, aclr2, status);
                DTUsers = DataBase.BaseTable("dbo.Users2");
                DataGridUsers.ItemsSource = DTUsers.DefaultView;

                //DataBase.Command(string.Format("INSERT dbo.Users(user1,x,y,moc,zysk,nrkanalu) VALUES ('{0}', {1}, {2}, {3}, {4}, {5});", TextBoxNazwaUzytkownika.Text.ToString(), int.Parse(TextBoxLokalizacjaX.Text), int.Parse(TextBoxLokalizacjaY.Text), _mocNadawcza.ToString().Replace(',', '.'), _zyskAntenyN.ToString().Replace(',', '.'), int.Parse(TextBoxNumerKanalu.Text)));
                //DataGridUsers.ItemsSource = DataBase.UserList().DefaultView;
                //Plot.plotStation(Grid2, int.Parse(TextBoxLokalizacjaX.Text), int.Parse(TextBoxLokalizacjaY.Text));
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
                    //Plot.deleteStation(Grid2, int.Parse((((DataRowView)DataGridUsers.SelectedItem).Row["x"]).ToString()), int.Parse((((DataRowView)DataGridUsers.SelectedItem).Row["y"]).ToString()));
                    //DataBase.Command(string.Format("delete from dbo.Users2 where ID= '{0}' ", ((DataRowView)DataGridUsers.SelectedItem).Row["ID"].ToString()));
                    string ID = ((DataRowView)DataGridUsers.SelectedItem).Row["ID"].ToString();
                    string kanal = ((DataRowView)DataGridUsers.SelectedItem).Row[4].ToString();
                    DataBase.Command(string.Format("UPDATE dbo.Users2 SET Status= '{0}' WHERE ID= '{1}' ", "usunięty", ID));
                    //DTUsers = DataBase.BaseTable("dbo.Users2");
                    DTName = DataBase.BaseTable(string.Format("dbo.Name{0}", kanal));
                    DTSINR = DataBase.BaseTable(string.Format("dbo.Sinr{0}", kanal));
                    DTSNR = DataBase.BaseTable(string.Format("dbo.Snr{0}", kanal));
                    for (int i = 0; i < DTName.Rows.Count; i++)
                    {
                        for (int j = 0; j < DTName.Columns.Count -1; j++)
                        {
                            if (DTName.Rows[i][j].ToString() == ID)
                            {
                                DataBase.Command(string.Format("UPDATE dbo.Name{0} SET [{1}]='' WHERE ID= '{2}' ", kanal, j+1, i+1));
                                DataBase.Command(string.Format("UPDATE dbo.SINR{0} SET [{1}]='' WHERE ID= '{2}' ", kanal, j+1, i+1));
                                DataBase.Command(string.Format("UPDATE dbo.SNR{0} SET [{1}]='' WHERE ID= '{2}' ", kanal, j+1, i+1));
                                //DTName.Rows[i][j] = "";
                                //DTSINR.Rows[i][j] = "";
                                //DTSNR.Rows[i][j] = "";
                            }
                        }
                    }
                    //DataGridUsers.ItemsSource = DTUsers.DefaultView;

                }
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
        private void BaseStationInitialization(int x_b, int y_b, int antena_gain_base, int power_base)
        {
            // stworzenie stacji nadawczej:
            Base = new BaseStation(x_b, y_b, antena_gain_base, power_base);
        }
        // oznaczenia te same co dla stacji 
        // channel_number - numer kanału - warto aby GUI dodało sprawdzanie wpisywanych danych bo my w żaden sposob nie sprawdzamy czy kanał znajduje się w zakresie 1-10 - jeśli nie dacie rady to dajcie znać my zrobimy mechanizm sprawdzjący
        private void UserInitialization(int x_u, int y_u, int antena_gain_user, int channel_number)
        {
            user = new User(x_u, y_u, antena_gain_user, channel_number);
            ///
        }

        private void RadioBazaChecked(object sender, RoutedEventArgs e)
        {
            if ((bool)RadioBaza.IsChecked == true)
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

        private void ComboBoxNumerKanaluMapa_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxNumerKanaluMapa.SelectedItem = 1;
            ComboBoxNumerKanaluMapa.Items.Add(1);
            ComboBoxNumerKanaluMapa.Items.Add(2);
            ComboBoxNumerKanaluMapa.Items.Add(3);
            ComboBoxNumerKanaluMapa.Items.Add(4);
            ComboBoxNumerKanaluMapa.Items.Add(5);
            ComboBoxNumerKanaluMapa.Items.Add(6);
            ComboBoxNumerKanaluMapa.Items.Add(7);
            ComboBoxNumerKanaluMapa.Items.Add(8);
            ComboBoxNumerKanaluMapa.Items.Add(9);
            ComboBoxNumerKanaluMapa.Items.Add(10);
        }

        private void ComboBoxNumerKanaluMapa_DropDownClosed(object sender, EventArgs e)
        {
            Plot.ClearMap(Grid2);
            if (ComboBoxNumerKanaluMapa.Text == "")
            {
                DTName = DataBase.BaseTable("dbo.Name1");
                DTSINR = DataBase.BaseTable("dbo.Sinr1");
                DTSNR = DataBase.BaseTable("dbo.Snr1");
                Plot.plotStation2(Grid2, DTName, DTSINR, DTSNR);

            }
            else
            {
                string name = "dbo.Name" + ComboBoxNumerKanaluMapa.Text;
                string SINR = "dbo.Sinr" + ComboBoxNumerKanaluMapa.Text;
                string SNR = "dbo.Snr" + ComboBoxNumerKanaluMapa.Text;
                DTName = DataBase.BaseTable(name);
                DTSINR = DataBase.BaseTable(SINR);
                DTSNR = DataBase.BaseTable(SNR);
                Plot.plotStation2(Grid2, DTName, DTSINR, DTSNR);

            }
        }

        private void ComboBoxNumerKanalu_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxNumerKanalu.SelectedItem = 1;
            ComboBoxNumerKanalu.Items.Add(1);
            ComboBoxNumerKanalu.Items.Add(2);
            ComboBoxNumerKanalu.Items.Add(3);
            ComboBoxNumerKanalu.Items.Add(4);
            ComboBoxNumerKanalu.Items.Add(5);
            ComboBoxNumerKanalu.Items.Add(6);
            ComboBoxNumerKanalu.Items.Add(7);
            ComboBoxNumerKanalu.Items.Add(8);
            ComboBoxNumerKanalu.Items.Add(9);
            ComboBoxNumerKanalu.Items.Add(10);
        }
 
        private void UPDATEDataGrid()
        {
            while (true)
            {
                {
                    try
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                        {
                            DataTable data = DataBase.BaseTable("dbo.Users2");

                            if (DTUsers.Rows.Count == data.Rows.Count)
                            {
                                for (int i = 0; i < DTUsers.Rows.Count; i++)
                                {
                                    if (DTUsers.Rows[i][10].ToString() != data.Rows[i][10].ToString())
                                    {
                                        DataGridUsers.ItemsSource = data.DefaultView;
                                        DTUsers = data;
                                    }
                                }
                            }
                            else
                            {
                                DataGridUsers.ItemsSource = data.DefaultView;
                                DTUsers = data;
                            }
                 
                         }));

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(this, ex.Message, "Error message");
                        return;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        /*
        private void DataGrid()
        {
            Thread.Sleep(5000);
            try
            {
                for (int i = 0; i < DTUsers.Rows.Count; i++)
                {
                    if (DTUsers.Rows[i][10].ToString() == "oczekujacy")
                    {
                        DataGridRow row = (DataGridRow)DataGridUsers.ItemContainerGenerator.ContainerFromIndex(i);
                        SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                        //Invoke(new Action(() => { TextBoxTest.Text = "Napis"; }));
                        row.Background = brush;
                        //DataGridUsers.Items. ;
                    }
                }
            }
            catch(InvalidCastException e) { }
            
        }
        */
    }
}
