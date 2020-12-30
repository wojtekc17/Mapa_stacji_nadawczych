using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    class Calculations
    {
        private double the_distance_;
        private double FSPL_;
        private double N_;
        private double SNR_;
        private double receiver_power;
        private double I_; // moc nadawcza zakłócającej stacji minus fspl = moc zakłóceń odbiornika z innej stacji nadawczej
        private double N_linear; // w watach
        private double I_linear; // w watach
        private double SINR_;


        public Calculations() { }
        public void CalculateTheDistace(double x_b, double y_b, double x_u, double y_u)
        {
            the_distance_ = Math.Sqrt(Math.Pow(x_b - x_u, 2) + Math.Pow(y_b - y_u, 2));
        }
        public void CalculateFSPL(double band)
        {
            //  FSPL_ = 32.44d + 20 * Math.Log10(the_distance_) + 20 * Math.Log10(band);
            FSPL_ = 92.45d + 20 * Math.Log10(the_distance_ / 10) + 20 * Math.Log10((band / 10) / 1000); // distance w km, band w GHz
        }

        public void CalculateReceiverPower(double transmitter_power, double transmitter_gain, double receiver_gain)
        {
            receiver_power = transmitter_power - FSPL_ + transmitter_gain + receiver_gain - 4; // 4 odpowiada NF (noise figure)
        }

        public void CalculateI_(double transmitter_interference_power) // pytanie
        {
            I_ = transmitter_interference_power - FSPL_;
        }

        public void CalculateNoise(double band)
        {
            N_ = -174 + 10 * Math.Log10((band / 10) * 1000000); // szerokośc pasma musi być w Hz 
            // N_ w dBm
        }
        public void CalculateSNR_Receiver()
        {
            SNR_ = receiver_power - N_;
            // SNR w dB
        }
        public void CalculateSINR() // ten sam kanał
        {
            N_linear = Math.Pow(10, N_ / 10) / 1000;
            I_linear = Math.Pow(10, I_ / 10) / 1000;
            double suma = 10 * Math.Log10(N_linear + I_linear) + 30;
            SINR_ = receiver_power - suma;
        }

        public void CalculateSINR_ACLR() // różne kanały, pytanie
        {
            // sprawdzanie w jakim kanale nadaje stacja zakłócająca; 
            //...
            //
            N_linear = Math.Pow(10, N_ / 10) / 1000;
            // dla ch+1
            I_linear = Math.Pow(10, (I_ - 40) / 10) / 1000;
            double suma = 10 * Math.Log10(N_linear + I_linear) + 30;
            SINR_ = receiver_power - suma;
            //dla ch+2
            I_linear = Math.Pow(10, (I_ - 60) / 10) / 1000;
            suma = 10 * Math.Log10(N_linear + I_linear) + 30;
            SINR_ = receiver_power - suma;
        }


    }
}
