using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exacta
{
    public class ArticolTable
    {
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }

        }

        private string _Articol;
        public string Articol
        {
            get
            {
                return _Articol;
            }
            set
            {
                _Articol = value;
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        private double _CapiOra;
        public double CapiOra
        {
            get
            {
                return _CapiOra;
            }
            set
            {
                _CapiOra = value;
            }
        }

        

        public ArticolTable(int id, string articol, string descriere, double capiora)
        {
            Id = id;
            Articol = articol;
            Description = descriere;
            CapiOra = capiora;
        }
    }
}
