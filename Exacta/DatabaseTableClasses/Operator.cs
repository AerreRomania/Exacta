using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name = "Operators")]
    public class Operator
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

        private string _Name;
        [Column(Storage = "_Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private string _Surname;
        [Column(Storage = "_Surname")]
        public string Surname
        {
            get
            {
                return _Surname;
            }
            set
            {
                _Surname = value;
            }
        }

        private string _Telephone;
        [Column(Storage = "_Telephone")]
        public string Telephone
        {
            get
            {
                return _Telephone;
            }
            set
            {
                _Telephone = value;
            }
        }

        private string _Address;
        [Column(Storage = "_Address")]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        private string _Line;
        [Column(Storage = "_Line")]
        public string Line
        {
            get
            {
                return _Line;
            }
            set
            {
                _Line = value;
            }
        }
    }
}
