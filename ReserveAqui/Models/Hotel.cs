using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Endereço { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string ImagemUrl { get; set; }  // Caminho para a imagem do hotel

        public ICollection<Quarto> Quartos { get; set; } = new List<Quarto>();
    }
}
