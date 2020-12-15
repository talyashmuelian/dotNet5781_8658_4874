using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLAPI
{
    public static class DLFactory
    {
        public static IDal GetDal()
        {
            //switch(typeDL)
            //{
            //    case "List": return DL.Instance;
            //    //case "XML": return DLXML.Intance;
            //    default: return null;
            //}
            // get dal implementation name from config.xml according to <data> element
            string dalType = DalConfig.DalName;
            // bring package name (dll file name) for the dal name (above) from the list of packages in config.xml
            string dalPackage = DalConfig.DalPackages[dalType];
            // if package name is not found in the list - there is a problem in config.xml
            if (dalPackage == null)
                throw new DalConfigException($"Wrong DL type: {dalType}");

            try // Load into CLR the dal implementation assembly according to dll file name (taken above)
            {
                Assembly.Load(dalPackage);
            }
            catch (Exception ex)
            {
                throw new DalConfigException($"Failed loading {dalPackage}.dll", ex);
            }
            Type type = Type.GetType($"DL.{dalPackage}, {dalPackage}");
            // If the type is not found - the implementation is not correct - it looks like the class name is wrong...
            if (type == null)
                throw new DalConfigException($"Class name is not the same as Assembly Name: {dalPackage}");
            try
            {
                IDal dal = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDal;
                // If the instance property is not initialized (i.e. it does not hold a real instance reference)...
                if (dal == null)
                    throw new DalConfigException($"Class {dalPackage} instance is not initialized");
                // now it looks like we have appropriate dal implementation instance :-)
                return dal;
            }
            catch (NullReferenceException ex)
            {
                throw new DalConfigException($"Class {dalPackage} is not a singleton", ex);
            }
        }
    }
}
