namespace ReserveAqui.Models
{
    public class ReservaServicoAdicional
    {
        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }

        public int ServicoAdicionalId { get; set; }
        public ServicoAdicional ServicoAdicional { get; set; }
    }
}