﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? BlogMainImage { get; set; }
        public string? BlogMainText { get; set; }
        public IFormFile? Image { get; set; }

        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }

    }
}