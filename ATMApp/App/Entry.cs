using ATMApp.Domain.Data;

namespace ATMApp.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            //DBcon.CreateDatabase();
            ATMApp atmApp = new ATMApp();
            atmApp.Run();
        }
    }
}
