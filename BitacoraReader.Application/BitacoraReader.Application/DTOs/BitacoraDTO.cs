namespace BitacoraReader.Application.DTOs
{
    public class BitacoraDto
    {
        public long IdBitacora { get; set; }
        public int IdJob { get; set; }
        public int Dia { get; set; }
        public DateTime Hora { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string Resultado { get; set; } = string.Empty;
        public string? DuracionFormateada { get; set; }
        public bool EstaCompleto { get; set; }
        public bool EsExitoso { get; set; }
    }
}