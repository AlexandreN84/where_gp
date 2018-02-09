//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestaoPatrimonio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patrimonio
    {
        public Patrimonio()
        {
            this.Tag = new HashSet<Tag>();
        }
    
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Dt_Aquisicao { get; set; }
        public string Dt_InicioUso { get; set; }
        public string Tv_Depreciacao { get; set; }
        public string Valor_Original { get; set; }
        public int Id_Conta { get; set; }
        public int Id_Centro { get; set; }
        public Nullable<int> Qt_DiasCalibracao { get; set; }
        public Nullable<int> Qt_DiasDepreciacao { get; set; }
        public Nullable<int> Qt_DiasManutencao { get; set; }
        public Nullable<int> Qt_DiasTesteEletrico { get; set; }
    
        public virtual Centro Centro { get; set; }
        public virtual Conta Conta { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}