//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace University.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingener
    {
        public int Number { get; set; }
        public string Specialnost { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
