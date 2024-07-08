using AutoMapper;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands.AddProductCommands;
using CleanArch.ATG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Mappings
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<AddProductCommand , Product>();
        }
    }
}
