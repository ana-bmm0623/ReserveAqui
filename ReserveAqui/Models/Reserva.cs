using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Models
{
    public class Reserva
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int QuartoId { get; set; }

        [Required]
        public int HospedeId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } // Chave estrangeira para ApplicationUser

        public virtual ApplicationUser ApplicationUser { get; set; } // Propriedade de navegação para ApplicationUser

        [Required]
        public int QuantidadePessoas { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSaida { get; set; }

        public bool CheckInRealizado { get; set; }
        public bool CheckOutRealizado { get; set; }
        public bool Cancelada { get; set; }

        public virtual Quarto Quarto { get; set; }
        public virtual Hospede Hospede { get; set; }

        public ICollection<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; } = new List<ReservaServicoAdicional>();
    }
}

