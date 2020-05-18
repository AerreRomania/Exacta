using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exacta.DatabaseTableClasses
{
    [Table(Name = "OperationParameters")]
    public class OperationParameters
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

        private int _IdArticol;
        [Column(Storage = "_IdArticol")]
        public int IdArticol
        {
            get
            {
                return _IdArticol;
            }
            set
            {
                _IdArticol = value;
            }
        }

        private int _IdOperatie;
        [Column(Storage = "_IdOperatie")]
        public int IdOperatie
        {
            get
            {
                return _IdOperatie;
            }
            set
            {
                _IdOperatie = value;
            }
        }

        private double _BucatiOra;
        [Column(Storage = "_BucatiOra")]
        public double BucatiOra
        {
            get
            {
                return _BucatiOra;
            }
            set
            {
                _BucatiOra = value;
            }
        }
        private int _Tens1;
        [Column(Storage = "_Tens1")]
        public int Tens1
        {
            get
            {
                return _Tens1;
            }
            set
            {
                _Tens1 = value;
            }
        }

        private int _Tens2;
        [Column(Storage = "_Tens2")]
        public int Tens2
        {
            get
            {
                return _Tens2;
            }
            set
            {
                _Tens2 = value;
            }
        }

        private int _Tens3;
        [Column(Storage = "_Tens3")]
        public int Tens3
        {
            get
            {
                return _Tens3;
            }
            set
            {
                _Tens3 = value;
            }
        }

        private int _P1;
        [Column(Storage = "_P1")]
        public int P1
        {
            get
            {
                return _P1;
            }
            set
            {
                _P1 = value;
            }
        }

        private int _P2;
        [Column(Storage = "_P2")]
        public int P2
        {
            get
            {
                return _P2;
            }
            set
            {
                _P2 = value;
            }
        }

        private int _Velocita;
        [Column(Storage = "_Velocita")]
        public int Velocita
        {
            get
            {
                return _Velocita;
            }
            set
            {
                _Velocita = value;
            }
        }

        private int _GapIniziale;
        [Column(Storage = "_GapIniziale")]
        public int GapIniziale
        {
            get
            {
                return _GapIniziale;
            }
            set
            {
                _GapIniziale = value;
            }
        }

        private int _TensIniziali;
        [Column(Storage = "_TensIniziali")]
        public int TensIniziali
        {
            get
            {
                return _TensIniziali;
            }
            set
            {
                _TensIniziali = value;
            }
        }

        private int _Finezza;
        [Column(Storage = "_Finezza")]
        public int Finezza
        {
            get
            {
                return _Finezza;
            }
            set
            {
                _Finezza = value;
            }
        }

        private int _GapFinale;
        [Column(Storage = "_GapFinale")]
        public int GapFinale
        {
            get
            {
                return _GapFinale;
            }
            set
            {
                _GapFinale = value;
            }
        }

        private int _TensFinale;
        [Column(Storage = "_TensFinale")]
        public int TensFinale
        {
            get
            {
                return _TensFinale;
            }
            set
            {
                _TensFinale = value;
            }
        }

        private int _PtFinali;
        [Column(Storage = "_PtFinali")]
        public int PtFinali
        {
            get
            {
                return _PtFinali;
            }
            set
            {
                _PtFinali = value;
            }
        }

        private int _AffIniziale;
        [Column(Storage = "_AffIniziale")]
        public int AffIniziale
        {
            get
            {
                return _AffIniziale;
            }
            set
            {
                _AffIniziale = value;
            }
        }

        private int _TensAff;
        [Column(Storage = "_TensAff")]
        public int TensAff
        {
            get
            {
                return _TensAff;
            }
            set
            {
                _TensAff = value;
            }
        }

        private int _AffFinale;
        [Column(Storage = "_AffFinale")]
        public int AffFinale
        {
            get
            {
                return _AffFinale;
            }
            set
            {
                _AffFinale = value;
            }
        }

        private int _Components;
        [Column(Storage = "_Components")]
        public int Components
        {
            get
            {
                return _Components;
            }
            set
            {
                _Components = value;
            }
        }
    }
}
