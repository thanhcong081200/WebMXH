//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMXH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BAIVIET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAIVIET()
        {
            this.COMMENT = new HashSet<COMMENT>();
            this.THICH = new HashSet<THICH>();
            this.USERR1 = new HashSet<USERR>();
        }
    
        public int IDBAIVIET { get; set; }
        public Nullable<int> USERID { get; set; }
        public string NOIDUNG { get; set; }
        public string HINHANH { get; set; }
        public string VIDEO { get; set; }
        public string CHEDO { get; set; }
        public Nullable<System.DateTime> NGAYDANG { get; set; }
        public Nullable<int> LUOTLIKE { get; set; }
        public Nullable<int> LUOTCMT { get; set; }
        public Nullable<int> LUOTSHARE { get; set; }
    
        public virtual USERR USERR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THICH> THICH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERR> USERR1 { get; set; }
    }
}
