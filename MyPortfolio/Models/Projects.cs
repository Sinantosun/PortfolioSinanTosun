//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPortfolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Projects
    {
        public int ProjectID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Nullable<int> Category { get; set; }
        public string Github { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    
        public virtual Categories Categories { get; set; }
    }
}
