using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
namespace BL
{
    class BLIMP : IBL
    {
        readonly IDal dal = DLFactory.GetDal();
    }
}

