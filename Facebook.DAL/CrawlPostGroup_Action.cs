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
    
    public partial class CrawlPostGroup_Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Nullable<int> CrawlPostGroup_PostId { get; set; }
    
        public virtual CrawlPostGroup_Post CrawlPostGroup_Post { get; set; }
    }
}