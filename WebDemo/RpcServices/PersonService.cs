using IRpcService;
using IRpcService.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Filters;

namespace WebDemo.RpcServices
{
    //实现契约接口IPersonService
    public class PersonService : IPersonService
    {
        private readonly ConcurrentDictionary<int, PersonDto> persons = new ConcurrentDictionary<int, PersonDto>();
        public bool Add(PersonDto person)
        {
            return persons.TryAdd(person.Id, person);
        }

        public void Delete(int id)
        {
            persons.Remove(id, out PersonDto person);
        }

        //自定义Filter
        [CacheFilter(CacheTime = 500)]
        public PersonDto Get(int id)
        {
            return persons.GetValueOrDefault(id);
        }

        //自定义Filter
        [CacheFilter(CacheTime = 300)]
        public IEnumerable<PersonDto> GetAll()
        {
            foreach (var item in persons)
            {
                yield return item.Value;
            }
        }
    }
}
