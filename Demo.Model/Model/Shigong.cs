using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class Shigong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shigong()
        {
            this.ShigongEnclosure = new HashSet<ShigongEnclosure>();
            this.ShigongImages = new HashSet<ShigongImages>();
        }

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

        public virtual Biaoduan Biaoduan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShigongEnclosure> ShigongEnclosure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShigongImages> ShigongImages { get; set; }
    }
}
