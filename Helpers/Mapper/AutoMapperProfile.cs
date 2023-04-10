using AutoMapper;
using Data.Models.Usuarios;
using EvertecApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvertecApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuarios, EditarUsuariosDto>().ReverseMap();

            CreateMap<Usuarios, AgregarUsuarioDto>().ReverseMap();

            CreateMap<Usuarios, ConsultarUsuariosDto>().ReverseMap();

        }
    }
}
