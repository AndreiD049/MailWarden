using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface IDBTable
    {
        IDBModule Module { get; set; }
        ISchema Schema { get; set; }
        string TableName { get; set; }
        void InitTable();
        object SelectAll();
        object Update(object item);
        object Delete(object identifier);
        object InsertNew(object item);
        object SelectOne(object id);
    }
}
