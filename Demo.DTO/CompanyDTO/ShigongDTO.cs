using Demo.DTO.ModelData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.DTO.CompanyDTO
{
    public class Request_Shigong : ModelDTO
    {
        [Required(ErrorMessage = "BiaoduanId字段必填！")]
        [SelectField("and", "=", "Guid")]
        public Guid? BiaoduanId { get; set; }

        [Required(ErrorMessage = "RegisterPeople字段必填222222222！")]
        [SelectField("and", "=", "string")]
        public string RegisterPeople { get; set; }

        [SelectField("and", "=", "DateTime")]
        public DateTime? ShiyongTime { get; set; }

        public DateTime? BShiyongTime2 { get; set; }
        public DateTime? EShiyongTime2 { get; set; }
    }
    public class ShigongModel
    {
        public System.Guid Id { get; set; }
        public int Code { get; set; }
        public System.Guid BiaoduanId { get; set; }
        public Nullable<System.DateTime> ShiyongTime { get; set; }
        public Nullable<System.DateTime> ShiyongTime2 { get; set; }
        public string PayDuixiang { get; set; }
        public string Yongtu { get; set; }
        public decimal Money { get; set; }
        public string RegisterPeople { get; set; }
        public string Remarks { get; set; }
        public string ShenhePeople { get; set; }
    }
    public class ShigongDTO : ShigongModel
    {
        public string BiaoduanName { get; set; }
    }
}
