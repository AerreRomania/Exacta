using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exacta
{
    public class FaseForArticle
    {

        private string _Fase;
        public string Fase
        {
            get
            {
                return _Fase;
            }
            set
            {
                _Fase = value;
            }
        }

        private double _BucatiOra;
        public double BucatiOra
        {
            get
            {
                return _BucatiOra;
            }
            set
            {
                _BucatiOra = value;
            }
        }

        private int _Components;
        public int Components
        {
            get
            {
                return _Components;
            }
            set
            {
                _Components = value;
            }
        }

        public FaseForArticle(string fase, double bucatiOra, int components)
        {
            Fase = fase;
            BucatiOra = bucatiOra;
            Components = components;
        }
    }
}
