
namespace ConsoleApp2.Utils
{
    public delegate void ItemAdded<T>(T item);
    public delegate void ItemsAdded<T>(IEnumerable<T> item);

    /// <summary>
    /// Same as List<T> but with event when value is added.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
