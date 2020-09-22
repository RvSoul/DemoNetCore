using IRpcService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRpcService
{
    public interface IPersonService
    {
        bool Add(PersonDto person);
        void Delete(int id);
        PersonDto Get(int id);
        IEnumerable<PersonDto> GetAll();
    }
}
