﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReserveAqui.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataPagamento { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}