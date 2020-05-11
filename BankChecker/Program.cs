namespace BankChecker
{
    internal static class Program
    {
        private const string Reference = "/Users/justin/Downloads/ICCU_Checking_20200510.csv";
        private const string Cross = "/Users/justin/Downloads/ICCU_Checking_TEST.csv";

        private static void Main(string[] args)
        { 
            var bankChecker = new BankChecker(Reference, Cross);
            bankChecker.DoTheThing();
        }
    }
}