using System;

namespace uFilter.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("uFilter pre-alpha");
            //Console.WriteLine("");

            try
            {
                UniqueFilter uf = new UniqueFilter();
                uf.Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine("");
            //Console.WriteLine("Press Enter to Quit...");
            //Console.Read();
        }
    }
}
