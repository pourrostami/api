using AutoMapper;
using Stone.Core.DTOs.AdminDTO;
using Stone.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    //in order to mapping we need to add ""service.AutoMapper(typeof(StoneMapping)) to configureServices in Startup
    public class StoneMapping:Profile
    {
        public StoneMapping()
        {
            CreateMap<Product, ProductViewModelDto>().ReverseMap();
        }
    }
}
