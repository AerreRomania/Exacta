using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name = "Orders")]
    public class Order
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

        private int _OrderNumber;
        [Column(Storage = "_OrderNumber")]
        public int OrderNumber
        {
            get
            {
                return _OrderNumber;
            }
            set
            {
                _OrderNumber = value;
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

        private string _Article;
        [Column(Storage = "_Article")]
        public string Article
        {
            get
            {
                return _Article;
            }
            set
            {
                _Article = value;
            }
        }

        private DateTime _DateArrival;
        [Column(Storage = "_DateArrival")]
        public DateTime DateArrival
        {
            get
            {
                return _DateArrival;
            }
            set
            {
                _DateArrival = value;
            }
        }
    }
}
