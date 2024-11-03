using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReserveAqui.Models
{
    public class ServicoAdicional
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public ICollection<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; } = new List<ReservaServicoAdicional>();
    }
}