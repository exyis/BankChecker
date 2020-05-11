using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace BankChecker
{
    public class BankChecker
    {
        private readonly string _referenceBankCsvFilePath;
        private readonly string _crossReferenceCsvFilePath;

        public BankChecker(string referenceBankCsvFilePath, string crossReferenceCsvFilePath)
        {
            _referenceBankCsvFilePath = referenceBankCsvFilePath;
            _crossReferenceCsvFilePath = crossReferenceCsvFilePath;
        }
        
        public void DoTheThing()
        {
            Console.WriteLine($"Checking reference csv {_referenceBankCsvFilePath} against {_crossReferenceCsvFilePath}");
            var reference = BankCsvToDictionary(_referenceBankCsvFilePath);
            var crossReference = BankCsvToDictionary(_crossReferenceCsvFilePath);

            var discrepancies = CrossReferenceBankDictionaries(reference, crossReference);

            if (discrepancies.ReferenceDiscrepancies.Any())
            {
                foreach (var (source, target) in discrepancies.ReferenceDiscrepancies)
                {
                    Console.WriteLine($"Reference -> Cross");
                    Console.WriteLine(source.ToString());
                    Console.WriteLine(target.ToString());
                    Console.WriteLine("");
                }
            }
            
            if (discrepancies.CrossDiscrepancies.Any())
            {
                foreach (var (source, target) in discrepancies.CrossDiscrepancies)
                {
                    Console.WriteLine($"Cross -> Reference");
                    Console.WriteLine(source.ToString());
                    Console.WriteLine(target.ToString());
                    Console.WriteLine("");
                }
            }
        }
        
        private Dictionary<long, BankLedgerItem> BankCsvToDictionary(string filePath)
        {
            var bankDictionary = new Dictionary<long, BankLedgerItem>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    var bankLedgerItem = new BankLedgerItem(fields);
                    bankDictionary.Add(bankLedgerItem.ReferenceNumber, bankLedgerItem);
                }
            }

            return bankDictionary;
        }

        private Discrepancies CrossReferenceBankDictionaries(Dictionary<long, BankLedgerItem> referenceDict, Dictionary<long, BankLedgerItem> crossDict)
        {
            var referenceDiscrepancies = new HashSet<(BankLedgerItem Source, BankLedgerItem Target)>();
            var crossDiscrepancies = new HashSet<(BankLedgerItem Source, BankLedgerItem Target)>();
            var previousReferenceNumber = 0L;
            
            // Check reference dict against cross dict
            foreach (var keyVal in referenceDict)
            {
                var referenceNumber = keyVal.Key;
                var reference = keyVal.Value;

                if (crossDict.TryGetValue(referenceNumber, out var cross))
                {
                    if (!reference.Equals(cross))
                    {
                        referenceDiscrepancies.Add((reference, cross));
                    }
                }
                else
                {
                    Console.WriteLine($"Reference -> Cross: Unable to get reference # {referenceNumber}. Previous reference #: {previousReferenceNumber}");
                }

                previousReferenceNumber = referenceNumber;
            }
            
            // Check cross dict against reference dict
            previousReferenceNumber = 0L;
            foreach (var keyVal in crossDict)
            {
                var referenceNumber = keyVal.Key;
                var reference = keyVal.Value;

                if (referenceDict.TryGetValue(referenceNumber, out var cross))
                {
                    if (!reference.Equals(cross))
                    {
                        crossDiscrepancies.Add((reference, cross));
                    }
                }
                else
                {
                    Console.WriteLine($"Cross -> Reference: Unable to get reference # {referenceNumber}. Previous cross reference #: {previousReferenceNumber}");
                }

                previousReferenceNumber = referenceNumber;
            }
            
            return new Discrepancies
            {
                ReferenceDiscrepancies = referenceDiscrepancies,
                CrossDiscrepancies = crossDiscrepancies
            };
        }
    }
}