//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdP_Aksenova
{
    using System;
    using System.Collections.Generic;
    
    public partial class Advertisement
    {
        public int ID { get; set; }
        public System.DateTime PublicationDate { get; set; }
        public int CityID { get; set; }
        public string Vendor { get; set; }
        public int CategoryID { get; set; }
        public int AdvertisementTypeID { get; set; }
        public bool IsFinished { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Photo { get; set; }
    
        public virtual AdvertisementType AdvertisementType { get; set; }
        public virtual Category Category { get; set; }
        public virtual City City { get; set; }
        public virtual User User { get; set; }
    }
}
