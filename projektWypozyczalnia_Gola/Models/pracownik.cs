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
    
    public partial class pracownik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pracownik()
        {
            this.wypozyczenie_has_pracownik = new HashSet<wypozyczenie_has_pracownik>();
            this.Users = new HashSet<User>();
        }
    
        public int id { get; set; }
        public int wypozyczalnia_id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public System.DateTime data_zatr { get; set; }
        public string nr_telefonu { get; set; }
        public string ulica { get; set; }
        public string miasto { get; set; }
        public string kod { get; set; }
    
        public virtual wypozyczalnia wypozyczalnia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wypozyczenie_has_pracownik> wypozyczenie_has_pracownik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
