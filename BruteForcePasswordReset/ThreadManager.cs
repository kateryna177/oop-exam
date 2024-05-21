using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BruteForcePasswordReset
{
    public class ThreadManager
    {
        private int maxThreads;
        private string encryptedPassword;
        private BruteForceCracker bruteForceCracker;

        public ThreadManager(int maxThreads, string encryptedPassword, BruteForceCracker bruteForceCracker)
        {
            this.maxThreads = maxThreads;
            this.encryptedPassword = encryptedPassword;
            this.bruteForceCracker = bruteForceCracker;
        }

        public void Start()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < maxThreads; i++)
            {
                Thread thread = new Thread(new ThreadStart(BruteForce));
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                  thread.Join();
            }
        }

        private void BruteForce()
        {
            char[] charset = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            StringBuilder currentAttempt = new StringBuilder();

            while (!bruteForceCracker.IsPasswordFound())
            {
                // Logic for generating next password attempt and checking it
                string passwordAttempt = GenerateNextPassword(currentAttempt, charset);
                if (CheckPassword(passwordAttempt))
                {
                    bruteForceCracker.ReportPasswordFound(passwordAttempt);
                    break;
                }
            }
        }

        private string GenerateNextPassword(StringBuilder currentAttempt, char[] charset)
        {
            // Simplified password generation for demonstration
            if (currentAttempt.Length == 0)
            {
                currentAttempt.Append(charset[0]);
            }
            else
            {
                int index = currentAttempt.Length - 1;
                while (index >= 0)
                {
                    if (currentAttempt[index] < charset[charset.Length - 1])
                    {
                        currentAttempt[index]++;
                        break;
                    }
                    else
                    {
                        currentAttempt[index] = charset[0];
                        if (index == 0)
                        {
                            currentAttempt.Insert(0, charset[0]);
                        }
                        index--;
                    }
                }
            }

            return currentAttempt.ToString();
        }

        private bool CheckPassword(string passwordAttempt)
        {
            string hashedPasswordAttempt = EncryptionHelper.EncryptPassword(passwordAttempt, PasswordManager.Salt);
            return hashedPasswordAttempt == encryptedPassword;
        }
    }
}
