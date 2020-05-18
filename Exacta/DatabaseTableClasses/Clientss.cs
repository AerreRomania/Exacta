using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name = "Clientss")]
    public class Clientss
    {
        private int _Id;
        [Column(Storage = "_Id", DbType = "Int NOT NULL IDENTITY", CanBeNull = false, IsPrimaryKey = true, IsDbGenerated = true, /*Identity Specification = true,*/ AutoSync = AutoSync.OnInsert)]
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

        private string _VATNumber;
        [Column(Storage = "_VATNumber")]
        public string VATNumber
        {
            get
            {
                return _VATNumber;
            }
            set
            {
                _VATNumber= value;
            }
        }

        private string _Country;
        [Column(Storage = "_Country")]
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
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

        private string _Mail;
        [Column(Storage = "_Mail")]
        public string Mail
        {
            get
            {
                return _Mail;
            }
            set
            {
                _Mail = value;
            }
        }

        private string _Bank;
        [Column(Storage = "_Bank")]
        public string Bank
        {
            get
            {
                return _Bank;
            }
            set
            {
                _Bank = value;
            }
        }
    }
}
