using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO.CompanyDTO
{
    public class ShigongEnclosureModel
    {
        public System.Guid Id { get; set; }
        public System.Guid ShigongId { get; set; }
        public string EnclosureName { get; set; }
        public string EnclosureUrl { get; set; }
    }
    public class ShigongEnclosureDTO : ShigongEnclosureModel
    {
    }
}
