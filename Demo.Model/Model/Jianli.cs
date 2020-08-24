using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class Jianli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jianli()
        {
            this.JianliEnclosure = new HashSet<JianliEnclosure>();
        }

        public System.Guid Id { get; set; }
        public int Code { get; set; }
        public System.Guid XianluId { get; set; }
        public System.Guid BiaoduanId { get; set; }
        public Nullable<System.DateTime> ShenheTime { get; set; }
        public string PayDuixiang { get; set; }
        public Nullable<decimal> ShendingMoney { get; set; }
        public string Yongtu { get; set; }
        public string RegisterPeople { get; set; }
        public string ShenheYijian { get; set; }
        public string ZShenheYijian { get; set; }

        public virtual Biaoduan Biaoduan { get; set; }
        public virtual Xianlu Xianlu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JianliEnclosure> JianliEnclosure { get; set; }
    }
}
