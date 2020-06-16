using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mobileApp.Models;

namespace mobileApp.Services
{
    public class MockDataStore : IDataStore<Student>
    {
        readonly List<Student> items;

        public MockDataStore()
        {
            items = new List<Student>()
            {
                new Student { Id = Guid.NewGuid().ToString(), Name = "First item", Surname="This is an item description." },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Second item", Surname="This is an item description." },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Third item", Surname="This is an item description." },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Surname="This is an item description." },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Surname="This is an item description." },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Surname="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            var oldItem = items.Where((Student arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Student arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Student> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}