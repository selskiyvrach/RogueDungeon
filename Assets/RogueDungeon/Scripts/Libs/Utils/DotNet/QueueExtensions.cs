﻿using System.Collections.Generic;

namespace Libs.Utils.DotNet
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (var item in items) 
                queue.Enqueue(item);
        }
    }
}