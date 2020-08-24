using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class Gongdian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gongdian()
        {
            this.Jianshe = new HashSet<Jianshe>();
        }

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> BiaoduanID { get; set; }
        public string GongdianName { get; set; }

        public virtual Biaoduan Biaoduan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jianshe> Jianshe { get; set; }
    }
}
