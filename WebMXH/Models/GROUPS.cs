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
    
    public partial class GROUPS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GROUPS()
        {
            this.CALL = new HashSet<CALL>();
            this.USERR1 = new HashSet<USERR>();
        }
    
        public int IDGROUP { get; set; }
        public string GROUP_NAME { get; set; }
        public Nullable<System.DateTime> THOIGIAN_KHOITAO { get; set; }
        public Nullable<int> USERID { get; set; }
        public string ANHNEN { get; set; }
        public string ANHDAIDIEN { get; set; }
        public Nullable<int> SOTHANHVIEN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALL { get; set; }
        public virtual USERR USERR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERR> USERR1 { get; set; }
    }
}
