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
    
    public partial class CrawlPostGroup_Privacy
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Friends { get; set; }
        public string Allow { get; set; }
        public string Deny { get; set; }
        public Nullable<int> CrawlPostGroup_PostId { get; set; }
    
        public virtual CrawlPostGroup_Post CrawlPostGroup_Post { get; set; }
    }
}
