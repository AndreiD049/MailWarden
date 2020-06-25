using System.Text.RegularExpressions;
using MailWarden2.MWIterfaces;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailWarden2.Rules
{
    public class RulesTable : IRulesTable
    {
        public List<IRule> Rules { get; set; }

        public RulesTable()
        {
            Rules = GetRulesTable();
        }

        /// <summary>
        /// Constructor that gets a list of rules.
        /// </summary>
        /// <param name="rules"></param>
        public RulesTable(List<IRule> rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// The order of rules is IMPORTANT.
        /// First rule that is compliant is taken into account. The rest rules are then ignored.
        /// </summary>
        /// <param name="item">Mail to be checked.</param>
        /// <returns>The action to be executed on the mail</returns>
        public MWAction IsMailToBeHandled(MWMailItem item)
        {
            if (item.ReceivedDate < DateTime.Now.AddHours(-100))
            {
                return MWAction.Exclude;
            }
            foreach (IRule rule in this.Rules)
            {
                if (rule.IsMailCompliant(item))
                    return rule.action;
            }
            // if nothing was found, exclude the mail
            return MWAction.Exclude;
        }

        /// <summary>
        /// Normally, this would call the database to extract the rules specific for current user
        /// </summary>
        /// <returns>The list of rules extracted from the DB</returns>
        public List<IRule> GetRulesTable()
        {
            /* 
             * Return fixed values for now
             */
            List<IRule> result = new List<IRule>();
            result.Add(new Rule(MWAction.Include, new Dictionary<MWFieldNames, Regex>() { { MWFieldNames.MWFrom, new Regex(@"^.*dimit.*\@.*$") } }));
            return result;
        }
    }
}
