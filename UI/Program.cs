using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
//using BL;
namespace UI
{
    class Program
    {
        static IBL bl;
        static void Main(string[] args)
        {
            bl = BLFactory.GetBl();
        }
    }
}
