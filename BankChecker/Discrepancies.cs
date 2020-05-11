using System.Collections.Generic;

namespace BankChecker
{
    public class Discrepancies
    {
        public HashSet<(BankLedgerItem Source, BankLedgerItem Target)> ReferenceDiscrepancies { get; set; }
        
        public HashSet<(BankLedgerItem Source, BankLedgerItem Target)> CrossDiscrepancies { get; set; }
    }
}