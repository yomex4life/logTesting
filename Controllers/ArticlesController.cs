using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using logTesting.Repo;
using Microsoft.AspNetCore.Mvc;

namespace logTesting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;

        public ArticlesController(IArticleRepo articleRepo, IMapper mapper)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
        }
    }
}