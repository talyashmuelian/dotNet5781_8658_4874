using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DL
{
    static class Cloning
    {
        internal static T Clone<T>(this T original)//דרך שלישית - בונוס
        {
            T target = (T)Activator.CreateInstance(original.GetType());
            //...
            return target;
        }
    }
}
