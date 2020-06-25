using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using MailWarden2.Rules;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Proxies
{
    class RulesTableProxy : IRulesTable
    {
        private IRulesTable _realRulesTable { get; set; }

        private IRulesTable RealRulesTable
        {
            get
            { 
                if (_realRulesTable == null)
                {
                    _realRulesTable = new RulesTable();
                }
                return _realRulesTable;
            }

            set
            {
                _realRulesTable = value;
            }
        }

        public List<IRule> Rules
        {
            get
            {
                return RealRulesTable.Rules;
            }

            set
            {
                RealRulesTable.Rules = value;
            }
        }

        public List<IRule> GetRulesTable()
        {
            return RealRulesTable.GetRulesTable();
        }

        public MWAction IsMailToBeHandled(MWMailItem item)
        {
            return RealRulesTable.IsMailToBeHandled(item);
        }

        public RulesTableProxy()
        {

        }
    }
}
