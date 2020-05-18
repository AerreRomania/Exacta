using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Exacta
{
    [Table(Name = "Articole")]
    public class Articole
    {        
        private int _Id;
        [Column(Storage = "_Id", DbType = "Int NOT NULL IDENTITY", CanBeNull = false, IsPrimaryKey = true, IsDbGenerated = true)]
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
        [Column(Storage = "_Articol")]
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

        private string _Descriere;
        [Column(Storage = "_Descriere")]
        public string Descriere
        {
            get
            {
                return _Descriere;
            }
            set
            {
                _Descriere = value;
            }
        }
        private string _Collection;
        [Column(Storage = "_Collection")]
        public string Collection
        {
            get
            {
                return _Collection;
            }
            set
            {
                _Collection = value;
            }
        }

        private string _Client;
        [Column(Storage = "_Client")]
        public string Client
        {
            get
            {
                return _Client;
            }
            set
            {
                _Client = value;
            }
        }
    }
}
