using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using BL;
namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBl()
        {
            return new BL.BLIMP();
        }
    }
}

