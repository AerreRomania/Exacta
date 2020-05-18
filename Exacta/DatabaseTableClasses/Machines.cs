using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exacta
{
    [Table(Name = "Machines")]
    public class Machines
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

        private string _Idm;
        [Column(Storage = "_Idm")]
        public string Idm
        {
            get
            {
                return _Idm;
            }
            set
            {
                _Idm = value;
            }
        }

        private string _Description;
        [Column(Storage = "_Description")]
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

        private string _NrMatricola;
        [Column(Storage = "_NrMatricola")]
        public string NrMatricola
        {
            get
            {
                return _NrMatricola;
            }
            set
            {
                _NrMatricola = value;
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

        private DateTime _ProductionDate;
        [Column(Storage ="_ProductionDate")]
        public DateTime ProductionDate
        {
            get
            {
                return _ProductionDate;
            }
            set
            {
                _ProductionDate = value;
            }
        }

    }
}
