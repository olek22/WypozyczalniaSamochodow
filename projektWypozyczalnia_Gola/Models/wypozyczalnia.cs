//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projektWypozyczalnia_Gola.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class wypozyczalnia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wypozyczalnia()
        {
            this.pracowniks = new HashSet<pracownik>();
            this.samochods = new HashSet<samochod>();
        }
    
        public int id { get; set; }
        public string numer { get; set; }
        public string ulica { get; set; }
        public string miasto { get; set; }
        public string kod { get; set; }
        public string nazwa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pracownik> pracowniks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<samochod> samochods { get; set; }
    }
}
