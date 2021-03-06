﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    class Plot
    {
        static Random color = new Random();
        public static void plotmap(Grid Grid2, int x, int y)
        {
            Grid2.RowDefinitions.Clear();
            Grid2.ColumnDefinitions.Clear();
            Grid2.Height = 200 * 20;
            Grid2.Width = 200 * 20;
            for (int i = 0; i < x; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                RowDefinition r = new RowDefinition();
                //c.Width = new GridLength(1, GridUnitType.Star);
                Grid2.ColumnDefinitions.Add(c);
                Grid2.RowDefinitions.Add(r);

            }

            for (int i = 0; i < x; i++)
            {                
                //int x = 0;
                for (int j = 0; j < y; j++)
                {
                    TextBlock tt = new TextBlock();
                    tt.Name = string.Format("TextBlock_{0}_{1}", i, j);
                    //Borde
                    tt.Text = "-";
                    tt.TextAlignment = TextAlignment.Center;
                    //tt.HorizontalAlignment = HorizontalAlignment.Center;
                    tt.VerticalAlignment = VerticalAlignment.Center;
                    //tt.TextAlignment = (TextAlignment)HorizontalAlignment.Center;
                    //tt.TextAlignment = System.Windows.TextAlignment.Center;
                    Grid2.Children.Add(tt);
                    Grid.SetRow(tt, i);
                    Grid.SetColumn(tt, j);
                }
            }
        }


        public static void plotStation2(Grid Grid2, DataTable name, DataTable SINR, DataTable SNR)
        {
            List<string> table = new List<string>();
            List<Color> colortab = new List<Color>();
            Color actualcolor = Color.FromArgb((byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256), 255);
            string actualname = "";
            string actualSINR = "";
            string actualSNR = "";
            for (int i = 0; i < name.Rows.Count; i++)
            {
                for (int j = 0; j < name.Columns.Count - 1; j++)
                {
                    if(name.Rows[i][j].ToString() != "")
                    {
                        var o = Grid2.Children[i * 200 + j];
                        for (int x = 0; x < table.Count; x++)
                        {
                            if (name.Rows[i][j].ToString() == table[x])
                            {
                                actualcolor = colortab[x];
                                actualname = name.Rows[i][j].ToString();
                                actualSINR = SINR.Rows[i][j].ToString();
                                actualSNR = SNR.Rows[i][j].ToString();
                                break;
                            }
                            else if(x == table.Count - 1)
                            {
                                table.Add(name.Rows[i][j].ToString());
                                actualcolor = Color.FromArgb(255, 255, (byte)color.Next(0, 256), (byte)color.Next(0, 256));
                                colortab.Add(actualcolor);
                                actualname = name.Rows[i][j].ToString();
                                actualSINR = SINR.Rows[i][j].ToString();
                                actualSNR = SNR.Rows[i][j].ToString();
                                break;
                            }
                        }
                        if(table.Count == 0)
                        {
                            table.Add(name.Rows[i][j].ToString());
                            actualcolor = Color.FromArgb((byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256), 255);
                            colortab.Add(actualcolor);
                            actualname = name.Rows[i][j].ToString();
                            actualSINR = SINR.Rows[i][j].ToString();
                            actualSNR = SNR.Rows[i][j].ToString();
                        }
                        
                        if (o is TextBlock)
                        {
                            TextBlock tt = o as TextBlock;
                            tt.Text = actualname;
                            tt.Background = new SolidColorBrush(actualcolor);
                            //tt.ToolTip = string.Format("X={0},Y={1}", x, y);
                            tt.ToolTip = string.Format("X={0}km Y={1}km SINR={2} SNR={3}", i, j, actualSINR, actualSNR);
                            //tt.ToolTip = string.Format("ID:{0} X={1},Y={2}", (Int32)data.Rows[i][0], (int)data.Rows[i][3], (int)data.Rows[i][3]);
                        }
                    }
                }
            }
        }
        public static void plotStationStart(Grid Grid2, DataTable data)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var o = Grid2.Children[(int)data.Rows[i][3] * 200 + (int)data.Rows[i][2]];
                // FindName("TextBlock_1_0");

                if (o is TextBlock)
                {
                    TextBlock tt = o as TextBlock;
                    tt.Text = "X";
                    tt.Background = Brushes.Yellow;
                    tt.ToolTip = string.Format("ID:{0} X={1},Y={2}", (Int32)data.Rows[i][0], (int)data.Rows[i][3], (int)data.Rows[i][3]);

                }
            }
        }

        public static void plotStation(Grid Grid2, int x, int y)
        {
            
            var o = Grid2.Children[y*200+x];
               // FindName("TextBlock_1_0");
            
            if(o is TextBlock)
            {
                TextBlock tt = o as TextBlock;
                tt.Text = "X";
                tt.Background = new SolidColorBrush(Color.FromArgb((byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256)));     //.Color.FromArgb((byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256), (byte)color.Next(0, 256));
                tt.ToolTip = string.Format("X={0},Y={1}",x,y);

            }          
        }

        public static void deleteStation(Grid Grid2, int x, int y)
        {

            var o = Grid2.Children[y * 200 + x];
            // FindName("TextBlock_1_0");

            if (o is TextBlock)
            {
                TextBlock tt = o as TextBlock;
                tt.Text = "-";
                tt.Background = Brushes.Gray;
                //tt.ToolTip = string.Format("X={0},Y={1}", x, y);

            }
        }

        public static void ClearMap(Grid Grid2)
        {
            for (int i = 0; i < Grid2.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < Grid2.ColumnDefinitions.Count - 1; j++)
                {
                    var o = Grid2.Children[i * 200 + j];
                    if (o is TextBlock)
                    {
                        TextBlock tt = o as TextBlock;
                        tt.Text = "-";
                        tt.Background = Brushes.Gray;
                        //tt.ToolTip = string.Format("ID:{0} X={1},Y={2}", (Int32)data.Rows[i][0], (int)data.Rows[i][3], (int)data.Rows[i][3]);
                    }
                }
            }
        }


    }
}
