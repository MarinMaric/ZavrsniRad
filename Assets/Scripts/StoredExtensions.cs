using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class StoredExtensions
    {
        public static bool Contains(this List<StoredItem> list, string Name)
        {
            foreach (var i in list)
            {
                if (i.Name == Name)
                    return true;
            }

            return false;
        }
    }
}
