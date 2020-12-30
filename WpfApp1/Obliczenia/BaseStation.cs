using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    class BaseStation
    {
        private Tuple<int, int> location_;
        private int antenna_gain_;
        private int power_;
        private User[] channels_;
        private double band_;
        private double channel_band_;
        public BaseStation(int x,int y,int antenna_gain,int power,int number_of_channels=10,double band=100)
        {
            location_ = new Tuple<int, int>(x, y);
            antenna_gain_ = antenna_gain;
            power_ = power;
            channels_ = new User[number_of_channels];
            band_ = band;
            try
            {
                if (number_of_channels == 0)
                {
                    throw new System.DivideByZeroException();
                }
                else
                {
                    channel_band_ = band / number_of_channels;
                }
            }
            catch(DivideByZeroException e)
            {
                //obsługa wyjątku 
            }
        }
       public void AddUserToChannel(int number_of_channel_,User user)
        {
            channels_[number_of_channel_] = user;
        }
        
    }
}
