using System;

namespace BankChecker
{
    public class BankLedgerItem
    {
        public BankLedgerItem(string[] fields)
        {
            if (fields.Length != 13)
            {
                throw new Exception("Invalid number of fields");
            }
            
            TransactionID = fields[0];
            PostingDate = DateTime.Parse(fields[1]);
            EffectiveDate = DateTime.Parse(fields[2]);
            TransactionType = fields[3];
            Amount = decimal.Parse(fields[4]);
            CheckNumber = fields[5];
            ReferenceNumber = long.Parse(fields[6]);
            Description = fields[7];
            TransactionCategory = fields[8];
            Type = fields[9];
            Balance = decimal.Parse(fields[10]);
            Memo = fields[11];
            ExtendedDescription = fields[12];
        }

        public string TransactionID { get; }
        
        public DateTime PostingDate { get; }
        
        public DateTime EffectiveDate { get; }
        
        public string TransactionType { get; }
        
        public decimal Amount { get; }
        
        public string CheckNumber { get; }
        
        public long ReferenceNumber { get; }
        
        public string Description { get; }
        
        public string TransactionCategory { get; }
        
        public string Type { get; }
        
        public decimal Balance { get; }
        
        public string Memo { get; }
        
        public string ExtendedDescription { get; }

        public bool Equals(BankLedgerItem bankLedgerItem)
        {
            return bankLedgerItem != null &&
                   bankLedgerItem.TransactionID == TransactionID &&
                   bankLedgerItem.PostingDate == PostingDate &&
                   bankLedgerItem.EffectiveDate == EffectiveDate &&
                   bankLedgerItem.TransactionType == TransactionType &&
                   bankLedgerItem.Amount == Amount &&
                   bankLedgerItem.CheckNumber == CheckNumber &&
                   bankLedgerItem.ReferenceNumber == ReferenceNumber &&
                   bankLedgerItem.Description == Description &&
                   bankLedgerItem.TransactionCategory == TransactionCategory &&
                   bankLedgerItem.Type == Type &&
                   bankLedgerItem.Balance == Balance &&
                   bankLedgerItem.Memo == Memo &&
                   bankLedgerItem.ExtendedDescription == ExtendedDescription;
        }

        public override string ToString()
        {
            return
                $"{TransactionID}, {PostingDate:d}, {EffectiveDate:d}, {TransactionType}, {Amount:C}, {CheckNumber}, {ReferenceNumber}, {Description}, {TransactionCategory}, {Type}, {Balance:C}, {Memo}, {ExtendedDescription}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BankLedgerItem);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(TransactionID);
            hashCode.Add(PostingDate);
            hashCode.Add(EffectiveDate);
            hashCode.Add(TransactionType);
            hashCode.Add(Amount);
            hashCode.Add(CheckNumber);
            hashCode.Add(ReferenceNumber);
            hashCode.Add(Description);
            hashCode.Add(TransactionCategory);
            hashCode.Add(Type);
            hashCode.Add(Balance);
            hashCode.Add(Memo);
            hashCode.Add(ExtendedDescription);
            return hashCode.ToHashCode();
        }
    }
}