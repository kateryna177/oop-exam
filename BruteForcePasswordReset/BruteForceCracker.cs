using System;
using System.Text;
using System.Threading;

namespace BruteForcePasswordReset
{
    public class BruteForceCracker
    {
        private volatile bool isPasswordFound;
        public string Results { get; private set; } = string.Empty;

        public void StartBruteForce(int maxThreads, string encryptedPassword)
        {
            isPasswordFound = false;
            Results = string.Empty;
            DateTime startTime = DateTime.Now;

            ThreadManager threadManager = new ThreadManager(maxThreads, encryptedPassword, this);
            threadManager.Start();

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;
            Results += $"Brute force attack completed in {duration.TotalSeconds} seconds.\n";
        }

        public void ReportPasswordFound(string password)
        {
            isPasswordFound = true;
            Results += $"Password found: {password}\n";
        }

        public bool IsPasswordFound()
        {
            return isPasswordFound;
        }
    }
}
