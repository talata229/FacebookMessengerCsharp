//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Facebook.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fb_Like_Post
    {
        public int Id { get; set; }
        public Nullable<int> IdPost { get; set; }
        public string FacebookIdPost { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Type { get; set; }
    
        public virtual Fb_Post Fb_Post { get; set; }
    }
}
