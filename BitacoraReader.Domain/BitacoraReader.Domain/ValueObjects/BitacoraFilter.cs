namespace BitacoraReader.Domain.ValueObjects
{
    public class BitacoraFilter
    {
        public int? IdJob { get; set; }
        public int? Dia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? Resultado { get; set; }
        public string? NombreArchivo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public bool HasFilters => IdJob.HasValue ||
                                 Dia.HasValue ||
                                 FechaInicio.HasValue ||
                                 FechaTermino.HasValue ||
                                 !string.IsNullOrEmpty(Resultado) ||
                                 !string.IsNullOrEmpty(NombreArchivo);
    }
}