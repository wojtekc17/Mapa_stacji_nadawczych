using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class PlaceholderInfoClass
    { 
        public string nazwa;
        public int x;
        public int y;
        public double moc;
        public double zysk;
        public int nrkanalu;
        public PlaceholderInfoClass(string nazwa, int x, int y, double moc, double zysk, int nrkanalu)
        {
        this.nazwa = nazwa;
        this.x = x;
        this.y = y;
        this.moc = moc;
        this.zysk = zysk;
        this.nrkanalu = nrkanalu;
        }
    }

}