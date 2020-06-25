using MailWarden2.DBModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface IDBModule
    {
        string Path { get; set; }
        string ConnectionString { get; set; }
        MWMailTable MailTable { get; set; }
        Dictionary<TablesEnum, IDBTable> Tables { get; set; }
        bool InitDb();

    }
}
