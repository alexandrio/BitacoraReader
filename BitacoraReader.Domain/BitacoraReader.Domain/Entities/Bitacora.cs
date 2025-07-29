
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitacoraReader.Domain.Entities
{
    [Table("Tb_Job_Importacion_Bitacora", Schema ="Prog")]
    public class Bitacora
    {
        [Key]
        [Column("IdBitacora")]
        // C# 'int' mapea perfectamente a SQL Server 'int'.
        // El error anterior de 'Int32' a 'Int64' podría indicar que en algún otro lugar
        // del código esperabas un 'long', pero la entidad debe ser 'int'.
        public int IdBitacora { get; set; }

        [Column("idjob")]
        public int IdJob { get; set; }

        [Column("dia")]
        // SQL 'smallint' mapea mejor a C# 'short'.
        public short Dia { get; set; }

        [Column("hora")]
        // SQL 'varchar(8) NOT NULL' mapea a C# 'string' con [MaxLength(8)].
        // Necesitas asegurarte de que los valores de tiempo se manejen como strings en tu lógica de negocio.
        [MaxLength(8)]
        public string Hora { get; set; } = string.Empty; // Se inicializa para asegurar que no sea null

        [Column("nombreArchivo")]
        // SQL 'varchar(100) NOT NULL' mapea a C# 'string' con [MaxLength(100)].
        [MaxLength(100)]
        public string NombreArchivo { get; set; } = string.Empty;

        [Column("fechainicio")]
        // SQL 'datetime NOT NULL' mapea a C# 'DateTime'.
        public DateTime FechaInicio { get; set; }

        [Column("fechatermino")]
        // SQL 'datetime NULL' mapea a C# 'DateTime?'.
        public DateTime? FechaTermino { get; set; }

        [Column("resultado")]
        // SQL 'varchar(5000) NOT NULL' mapea a C# 'string' con [MaxLength(5000)].
        [MaxLength(5000)]
        public string Resultado { get; set; } = string.Empty;

        // Propiedades calculadas (estas no se mapean a la base de datos, no hay problema con ellas)
        public TimeSpan? Duracion => FechaTermino?.Subtract(FechaInicio);
        public bool EstaCompleto => FechaTermino.HasValue;
        public bool EsExitoso => string.Equals(Resultado, "SUCCESS", StringComparison.OrdinalIgnoreCase);
    }
}