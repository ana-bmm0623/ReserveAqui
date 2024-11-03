using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

    namespace ReserveAqui.Models
    {
        public class Hospede
        {
            public int Id { get; set; }

            [Display(Name = "Nome Completo")]
            public string NomeCompleto { get; set; }

            public string CPF { get; set; }

            public string RG { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string Telefone { get; set; }

            public string Endereco { get; set; }

            public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        }
    }
