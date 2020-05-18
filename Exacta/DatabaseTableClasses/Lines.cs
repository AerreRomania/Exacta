using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exacta
{
    [Table(Name = "Lines")]
    public class Lines
    {
        private int _LineId;
        [Column(Storage = "_LineId", DbType = "Int NOT NULL IDENTITY", CanBeNull = false, IsPrimaryKey = true, IsDbGenerated = true)]
        public int LineId
        {
            get
            {
                return _LineId;
            }
            set
            {
                _LineId = value;
            }

        }

        private string _LineName;
        [Column(Storage = "_LineName")]
        public string LineName
        {
            get
            {
                return _LineName;
            }
            set
            {
                _LineName = value;
            }
        }

        private string _LineManager;
        [Column(Storage = "_LineManager")]
        public string LineManager
        {
            get
            {
                return _LineManager;
            }
            set
            {
                _LineManager = value;
            }
        }
    }
}
