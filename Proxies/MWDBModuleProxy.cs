using MailWarden2.DBModule;
using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Proxies
{
    class MWDBModuleProxy : IDBModule
    {
        private string _path { get; set; }
        private IDBModule _realModule { get; set; }
        private IDBModule RealModule
        {
            get
            {
                if (_realModule == null)
                {
                    _realModule = new MWDBModule(_path);
                }
                /*
                 *  check is the database exists and init it again if it doesn't
                 *  For ex if the database was deleted for some reason !?
                 */
                if (!File.Exists(_path))
                {
                    _realModule.InitDb();
                }
                return _realModule;
            }

            set
            {
                _realModule = value;
            }
        }
        public string Path
        {
            get
            {
                return RealModule.Path;
            }

            set
            {
                RealModule.Path = value;
            }
        }
        public string ConnectionString 
        {
            get
            {
                return RealModule.Path;
            }

            set
            {
                RealModule.Path = value;
            }
        }

        public Dictionary<TablesEnum, IDBTable> Tables 
        {
            get
            {
                return RealModule.Tables;
            }

            set
            {
                RealModule.Tables = value;
            }
        }

        public MWMailTable MailTable 
        {
            get
            {
                return RealModule.MailTable;
            }

            set
            {
                RealModule.MailTable = value;
            }
        }

        public bool InitDb()
        {
            return RealModule.InitDb();
        }

        public MWDBModuleProxy(string path)
        {
            _path = path;
        }
    }
}
