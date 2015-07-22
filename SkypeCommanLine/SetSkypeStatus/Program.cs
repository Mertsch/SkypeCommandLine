using System;
using System.Reflection;
using SKYPE4COMLib;

namespace SetSkypeStatus
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().FullName.ToString());

            if (args == null || args.Length != 1)
            {
                Console.WriteLine("Command line option not supported.");
                return;
            }

            string desiredStatusString = args[0];

            try
            {
                changeUserStatus(desiredStatusString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.ExitCode = 1;
            }
        }

        private static void changeUserStatus(string desiredStatusString)
        {
            Skype skype = new Skype();

            TUserStatus status = skype.Convert.TextToUserStatus(desiredStatusString);

            if (status == TUserStatus.cusUnknown)
            {
                Console.WriteLine("{0} can not be converted to a skype user status", desiredStatusString);
                return;
            }

            Console.WriteLine("Changing user status to {0}...", status);
            skype.ChangeUserStatus(status);
        }
    }
}