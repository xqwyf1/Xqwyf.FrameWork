using System;
using System.Collections.Generic;
using System.Linq;

namespace Xqwyf
{
    public static class NamedTypeSelectorListExtensions
    {
        /// <summary>
        /// Add list of types to the list.
        /// </summary>
        /// <param name="list">List of NamedTypeSelector items</param>
        /// <param name="name">An arbitrary but unique name (can be later used to remove types from the list)</param>
        /// <param name="types"></param>
        public static void Add(this IList<NamedTypeSelector> list, string name, params Type[] types)
        {
            XqCheck.NotNull(list, nameof(list));
            XqCheck.NotNull(name, nameof(name));
            XqCheck.NotNull(types, nameof(types));

            list.Add(new NamedTypeSelector(name, type => types.Any(type.IsAssignableFrom)));
        }
    }
}
