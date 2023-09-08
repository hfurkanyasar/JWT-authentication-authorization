using APIServiceWithMVC.myAPI.DAL.Entities;
using APIServiceWithMVC.myAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServiceWithMVC.myAPI.Mapper
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Products, ProductsDTO>();
            CreateMap<ProductsDTO, Products>();
        }
    }
}
