using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgntADO
{
    internal class Program
    {
        private void menu()
        {
            
            Console.WriteLine("         Press :1     Manage Item                 ");
            Console.WriteLine("         Press :2    Manage Customres             ");
            Console.WriteLine("         Press :3      Close App                  ");
            

        }
        static void Main(string[] args)
        {
            Program program = new Program();

        repeat:
            program.menu();

            int switch_on = int.Parse(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    ItemMaster itemMaster = new ItemMaster();
                    itemMaster.ItemMasteroption();
                    goto repeat;
                case 2:
                    Customer customer = new Customer();
                    customer.option();
                    goto repeat;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choise   ");
                    goto repeat;
            }

            Console.ReadLine();
        }
    }
}