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
    
    public partial class PHONGHOP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONGHOP()
        {
            this.MESSAGE_PHONGHOP = new HashSet<MESSAGE_PHONGHOP>();
            this.USER_TGHOP = new HashSet<USER_TGHOP>();
            this.VIDEO_CUOCHOP = new HashSet<VIDEO_CUOCHOP>();
        }
    
        public int IDPHONGHOP { get; set; }
        public Nullable<int> USERID { get; set; }
        public Nullable<System.DateTime> THOIGIAN_BD { get; set; }
        public Nullable<System.DateTime> THOIGIAN_KT { get; set; }
        public string TENPHONGHOP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE_PHONGHOP> MESSAGE_PHONGHOP { get; set; }
        public virtual USERR USERR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_TGHOP> USER_TGHOP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIDEO_CUOCHOP> VIDEO_CUOCHOP { get; set; }
    }
}
