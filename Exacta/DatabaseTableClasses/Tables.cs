using System.Data.Linq;            

namespace Exacta.DatabaseTableClasses
{
    public class Tables
    {  
        public static Table<Lines> TblLines => Menu.db.GetTable<Lines>();
        public static Table<Machines> TblMachines => Menu.db.GetTable<Machines>();
        public static Table<Articole> TblArticole => Menu.db.GetTable<Articole>();
        public static Table<Operatii> TblOperatii => Menu.db.GetTable<Operatii>();
        public static Table<OperationParameters> TblOperatiiArticole => Menu.db.GetTable<OperationParameters>();
        public static Table<Clientss> TblClients => Menu.db.GetTable<Clientss>();
        public static Table<Collections> TblCollections => Menu.db.GetTable<Collections>();
        public static Table<Order> TblOrder => Menu.db.GetTable<Order>();
        public static Table<Operator> TblOperator => Menu.db.GetTable<Operator>();
        public static Table<Pause> TblPauses => Menu.db.GetTable<Pause>();
    }
}
