using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name = "Collections")]
    public class Collections
    {
        private int _Id;
        [Column (Storage = "_Id", DbType = "Int NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = false, IsPrimaryKey = true, AutoSync = AutoSync.OnInsert)]
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
        private string _Code;
        [Column(Storage ="_Code", CanBeNull = true, IsDbGenerated =false)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
    }
}
