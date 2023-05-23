using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using logTesting.Dtos;
using logTesting.Models;

namespace logTesting.Profiles
{
    public class MyLogProjProfile : Profile
    {
        public MyLogProjProfile()
        {
            // Source -> Target
            CreateMap<Article, ArticleReadDto>().ReverseMap();
            CreateMap<Author, AuthorReadDto>().ReverseMap();
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
        }
    }
}