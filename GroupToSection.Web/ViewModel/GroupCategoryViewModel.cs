
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using System;
using System.ComponentModel.DataAnnotations;

namespace GroupToSection.Web.ViewModel
{
    public partial class GroupCategoryViewModel : ViewModelBase
    {
        public virtual string Name { get; set; }
        [DataType(DataType.Text)]
        public virtual DateTime? Created_at {get;set;} 
        public virtual int Course_Id {get;set;} 
        public virtual int Sis_GroupCategory_Id {get;set;} 
        public virtual int Sis_Import_Id {get;set;} 
        public virtual string GetBackToListLink(string applicationName) => $"/{applicationName}/{GetType().Name.Replace("ViewModel","")}";

    }
} 