using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DevelopShellbagsCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************Program Starts Here****************");
            iterateShellbags();
            //			RegistryKey rk = Registry.Users;
            //			iterateRegistry(rk, @"");
            Console.WriteLine("****************Program Ends Here****************");
            Console.ReadLine();
        }

        static string getSubkeyString(string subKey, string addOn)
        {
            return String.Format("{0}{1}{2}", subKey, subKey.Length == 0 ? "" : @"\", addOn);
        }

        static void iterateShellbags()
        {
            RegistryKey rk = Registry.Users;
            string[] subKeys = rk.GetSubKeyNames();
            foreach (string s in subKeys)
            {
                if (s.ToUpper() == ".DEFAULT")
                {
                    continue;
                }
                string sk = String.Format(@"{0}\Software\Microsoft\Windows\Shell", s);
                iterateRegistry(rk.OpenSubKey(sk), sk);
            }
        }

        static void iterateRegistry(RegistryKey rk, string subKey, int indent = 0)
        {
            if (rk == null)
            {
                return;
            }

            string[] subKeys = rk.GetSubKeyNames();
            string[] values = rk.GetValueNames();

            Console.WriteLine("**" + subKey);

            foreach (string s in subKeys)
            {
                if (s.ToUpper() == "ASSOCIATIONS")
                {
                    continue;
                }

                string sk = getSubkeyString(subKey, s);
                Console.WriteLine("{0}", sk);
                RegistryKey rkNext = null;
                try
                {
                    rkNext = rk.OpenSubKey(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                iterateRegistry(rkNext, sk, indent + 2);
            }

        }

    }
}
