using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.DTOs;
using System.Linq;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.Palestrantes, opt =>
                {
                    opt.MapFrom(src => src.PalestranteEventos.Select(s => s.Palestrante).ToList());
                }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(dest => dest.Eventos, opt =>
                {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(s => s.Evento).ToList());
                }).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}
