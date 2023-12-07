using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReportTitle { get; set; }
        public DateTime ReportDate { get; set; }
        public int PostId { get; set; }  
        public virtual Post Posts { get;}
    }
}