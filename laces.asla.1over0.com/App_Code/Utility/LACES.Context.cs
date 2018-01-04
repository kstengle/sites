﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Utility
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ASLA_Laces_ProdEntities : DbContext
    {
        public ASLA_Laces_ProdEntities()
            : base("name=ASLA_Laces_ProdEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblApprovedProvider> tblApprovedProviders { get; set; }
        public virtual DbSet<LACES_Search_Terms> LACES_Search_Terms { get; set; }
        public virtual DbSet<tblParticipantCource> tblParticipantCources { get; set; }
    
        public virtual ObjectResult<LACES_VisualizationNewProviders_Result> LACES_VisualizationNewProviders(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LACES_VisualizationNewProviders_Result>("LACES_VisualizationNewProviders", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LACES_VisualizationSubjectByDate_Result> LACES_VisualizationSubjectByDate(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LACES_VisualizationSubjectByDate_Result>("LACES_VisualizationSubjectByDate", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LACES_VisualizationSubjectByLocation_Result> LACES_VisualizationSubjectByLocation(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LACES_VisualizationSubjectByLocation_Result>("LACES_VisualizationSubjectByLocation", startDateParameter, endDateParameter);
        }
    }
}