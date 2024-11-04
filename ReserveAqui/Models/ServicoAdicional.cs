using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Models
{
    public class ServicoAdicional
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public double Valor { get; set; }

        public ICollection<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; } = new List<ReservaServicoAdicional>();
    }
}