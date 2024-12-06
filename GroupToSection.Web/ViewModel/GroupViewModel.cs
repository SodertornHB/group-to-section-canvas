
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using System;
using System.ComponentModel.DataAnnotations;

namespace GroupToSection.Web.ViewModel
{
    public partial class GroupViewModel : ViewModelBase
    {
        public virtual string name {get;set;}  = ""; 
        [DataType(DataType.Text)]
        public virtual DateTime? created_at {get;set;} 
        public virtual int group_category_id {get;set;} 
        public virtual int sis_group_id {get;set;} 
        public virtual int sis_import_id {get;set;} 
        public virtual string GetBackToListLink(string applicationName) => $"/{applicationName}/{GetType().Name.Replace("ViewModel","")}";
    }
} 