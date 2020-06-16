using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace mobileApp.Web.Models
{
    public class ItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Student> items =
            new ConcurrentDictionary<string, Student>();

        public ItemRepository()
        {
            Add(new Student { Id = Guid.NewGuid().ToString(), Name = "Item 1", Surname = "This is an item description." });
            Add(new Student { Id = Guid.NewGuid().ToString(), Name = "Item 2", Surname = "This is an item description." });
            Add(new Student { Id = Guid.NewGuid().ToString(), Name = "Item 3", Surname = "This is an item description." });
        }

        public IEnumerable<Student> GetAll()
        {
            return items.Values;
        }

        public void Add(Student item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public Student Get(string id)
        {
            items.TryGetValue(id, out Student item);
            return item;
        }

        public Student Remove(string id)
        {
            items.TryRemove(id, out Student item);
            return item;
        }

        public void Update(Student item)
        {
            items[item.Id] = item;
        }
    }
}
