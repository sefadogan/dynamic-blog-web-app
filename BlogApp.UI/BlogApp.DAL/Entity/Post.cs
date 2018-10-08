//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogApp.DAL.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.PostTags = new HashSet<PostTag>();
        }
    
        public int PostId { get; set; }
        public string Title { get; set; }
        public string BodySummary { get; set; }
        public string Body { get; set; }
        public Nullable<short> TimesDisplayed { get; set; }
        public System.DateTime InsertedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string MetaKeywords { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
