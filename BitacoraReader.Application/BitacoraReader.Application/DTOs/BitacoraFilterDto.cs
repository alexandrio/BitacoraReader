namespace BitacoraReader.Application.DTOs
{
    public class BitacoraFilterDto
    {
        public int? IdJob { get; set; }
        public int? Dia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? Resultado { get; set; }
        public string? NombreArchivo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}