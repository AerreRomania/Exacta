using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name ="Pause")]
   public class Pause
    {
        private int _Id;
        [Column (Storage  ="_Id", DbType ="Int NOT NULL IDENTITY", CanBeNull =false,IsDbGenerated =true,IsPrimaryKey =true)]
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
         public string _StartP;
        [Column(Name  ="StartP")]
        public string StartP
        {get
            {
                return _StartP;
            }
            set
            {
                _StartP = value;
            }
        }
        public string _EndP;
        [Column(Name = "EndP")]
        public string EndP
        {
            get
            {
                return _EndP;
            }
            set
            {
                _EndP = value;
            }
        }
        public string _Descriere;

        [Column(Name = "Descriere")]
        public string Descriere 
        { get
            {
                return _Descriere;
            }
            set
            {
                _Descriere = value;
            }
        }
    }
}
