//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserProfilerCommon
{
    using System;
    using System.Collections.Generic;
    
    public partial class CliqueClaimRequestFeedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime AddedAt { get; set; }
        public int RequestId { get; set; }
        public Nullable<double> Score { get; set; }
    
        public virtual CliqueRequest CliqueRequest { get; set; }
        public virtual CliqueClaimRequest CliqueClaimRequest { get; set; }
    }
}
