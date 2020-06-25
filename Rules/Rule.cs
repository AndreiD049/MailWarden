using Microsoft.Office.Interop.Outlook;
using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailWarden2.Rules
{
    class Rule: IRule
    {
        public MWAction action { get; set; }
        public Regex RgSubject { get; set; }
        public Regex RgBody { get; set; }
        public Regex RgFrom { get; set; }
        public Regex RgTo { get; set; }
        public Regex RgCC { get; set; }

        public Rule(MWAction action, Dictionary<MWFieldNames, Regex> rules)
        {
            this.action = action;
            foreach (MWFieldNames name in rules.Keys)
            {
                AssignRule(name, rules[name]);
            }
        }

        private void AssignRule(MWFieldNames name, Regex rule)
        {
            switch (name)
            {
                case MWFieldNames.MWSubject:
                    RgSubject = rule;
                    break;
                case MWFieldNames.MWBody:
                    RgBody = rule;
                    break;
                case MWFieldNames.MWFrom:
                    RgFrom = rule;
                    break;
                case MWFieldNames.MWTo:
                    RgTo = rule;
                    break;
                case MWFieldNames.MWCC:
                    RgCC = rule;
                    break;
            }
        }

        public bool IsMailCompliant(MWMailItem mail)
        {
            return true;
        }
    }
}
