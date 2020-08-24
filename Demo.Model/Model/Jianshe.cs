using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class Jianshe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jianshe()
        {
            this.JiansheEnclosure = new HashSet<JiansheEnclosure>();
        }

        public System.Guid Id { get; set; }
        public int Code { get; set; }
        public System.Guid BiaoduanId { get; set; }
        public System.Guid GongdianId { get; set; }
        public Nullable<int> QujiMonth { get; set; }
        public decimal Money { get; set; }
        public string RegisterPeople { get; set; }
        public System.DateTime RegisterTime { get; set; }

        public virtual Biaoduan Biaoduan { get; set; }
        public virtual Gongdian Gongdian { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JiansheEnclosure> JiansheEnclosure { get; set; }
    }
}
