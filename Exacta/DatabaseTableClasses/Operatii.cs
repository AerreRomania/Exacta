using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exacta
{
    [Table(Name = "Operatii")]
    public class Operatii
    {
        private int _Id;
        [Column(Storage = "_Id", IsPrimaryKey = true, IsDbGenerated = true)]
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

        private string _CodOperatie;
        [Column(Storage = "_CodOperatie")]
        public string CodOperatie
        {
            get
            {
                return _CodOperatie;
            }
            set
            {
                _CodOperatie = value;
            }
        }

        private string _Operatie;
        [Column(Storage = "_Operatie")]
        public string Operatie
        {
            get
            {
                return _Operatie;
            }
            set
            {
                _Operatie = value;
            }
        }
    }
}
