using AutoMapper;
using BitacoraReader.Application.DTOs;
using BitacoraReader.Domain.Entities;
using BitacoraReader.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BitacoraReader.Application.Mappings
{
    public class BitacoraProfile : Profile
    {
        public BitacoraProfile()
        {
            CreateMap<Bitacora, BitacoraDto>()
                .ForMember(dest => dest.DuracionFormateada,
                          opt => opt.MapFrom(src => FormatDuration(src.Duracion)))
                .ForMember(dest => dest.EstaCompleto,
                          opt => opt.MapFrom(src => src.EstaCompleto))
                .ForMember(dest => dest.EsExitoso,
                          opt => opt.MapFrom(src => src.EsExitoso));

            CreateMap<BitacoraFilterDto, BitacoraFilter>();
        }

        private static string? FormatDuration(TimeSpan? duration)
        {
            if (!duration.HasValue)
                return null;

            var d = duration.Value;
            if (d.TotalDays >= 1)
                return $"{d.Days}d {d.Hours}h {d.Minutes}m {d.Seconds}s";
            if (d.TotalHours >= 1)
                return $"{d.Hours}h {d.Minutes}m {d.Seconds}s";
            if (d.TotalMinutes >= 1)
                return $"{d.Minutes}m {d.Seconds}s";

            return $"{d.Seconds}s";
        }
    }
}