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
    
    public partial class MESSAGE_PHONGHOP
    {
        public int IDMES { get; set; }
        public int IDPHONGHOP { get; set; }
        public int USERID { get; set; }
        public string NOIDUNG { get; set; }
        public Nullable<System.DateTime> THOIGIAN { get; set; }
    
        public virtual PHONGHOP PHONGHOP { get; set; }
        public virtual USERR USERR { get; set; }
    }
}
