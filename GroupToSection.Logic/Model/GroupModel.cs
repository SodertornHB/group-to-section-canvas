
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using System;

namespace GroupToSection.Logic.Model
{
    public partial class Group : Entity
    {
        public virtual string name {get;set;}
        public virtual DateTime? created_at {get;set;}
        public virtual int group_category_id {get;set;}
        public virtual int sis_group_id {get;set;}
        public virtual int sis_import_id {get;set;}
      
    }
} 