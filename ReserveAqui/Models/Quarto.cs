using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Models
{
    public class Quarto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Número de Identificação")]
        public string NumeroIdentificacao { get; set; }

        [Required]
        [Display(Name = "Capacidade Máxima")]
        public int CapacidadeMaxima { get; set; }

        [Required]
        public bool Disponibilidade { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public string ImagemUrl { get; set; } // Caminho para a imagem do quarto

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço por Noite")]
        public decimal Preco { get; set; } // Adiciona o Preço

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
