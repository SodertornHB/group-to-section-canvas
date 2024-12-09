
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using System;

namespace GroupToSection.Logic.Model
{
    public partial class GroupCategory : Entity
    {
        public virtual string Name { get; set; }
        public virtual DateTime? Created_at {get;set;}
        public virtual int Course_Id {get;set;}
        public virtual int? Sis_GroupCategory_Id {get;set;}
        public virtual int? Sis_Import_Id {get;set;}
      
    }
} 