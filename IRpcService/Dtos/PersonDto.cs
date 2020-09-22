using System;
using System.Collections.Generic;
using System.Text;

namespace IRpcService.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsMarried { get; set; }
        public long Tel { get; set; }
    }
}
