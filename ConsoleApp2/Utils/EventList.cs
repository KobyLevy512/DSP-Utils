using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utils
{
    public delegate void ItemAdded<T>(T item);
    public delegate void ItemsAdded<T>(IEnumerable<T> item);
    public class EventList<T>:List<T>
    {
        public event ItemAdded<T>? OnItemAdded;
        public event ItemsAdded<T>? OnItemsAdded;
        public new void Add(T item)
        {
            base.Add(item);
            OnItemAdded?.Invoke(item);
        }
        public new void AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            OnItemsAdded?.Invoke(items);
        }
    }
}
