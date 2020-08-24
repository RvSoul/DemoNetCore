using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class Biaoduan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Biaoduan()
        {
            this.Gongdian = new HashSet<Gongdian>();
            this.Jianli = new HashSet<Jianli>();
            this.Jianshe = new HashSet<Jianshe>();
            this.Shigong = new HashSet<Shigong>();
        }

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> XianluId { get; set; }
        public string BiaoduanName { get; set; }

        public virtual Xianlu Xianlu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gongdian> Gongdian { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jianli> Jianli { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jianshe> Jianshe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shigong> Shigong { get; set; }
    }
}
