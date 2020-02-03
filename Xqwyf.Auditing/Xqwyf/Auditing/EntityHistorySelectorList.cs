using System;
using System.Collections.Generic;
using System.Text;

namespace Xqwyf.Auditing
{ 
    internal class EntityHistorySelectorList : List<NamedTypeSelector>, IEntityHistorySelectorList
    {
        public bool RemoveByName(string name)
        {
            return RemoveAll(s => s.Name == name) > 0;
        }
    }
}
