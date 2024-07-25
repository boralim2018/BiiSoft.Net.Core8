﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BiiSoft.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || !collection.Any();
        }

    }
}
