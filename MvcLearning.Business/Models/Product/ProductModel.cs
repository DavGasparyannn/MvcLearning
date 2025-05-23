﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MvcLearning.Data.Entities.Images;

namespace MvcLearning.Business.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }  
        public List<ProductImage>? Images { get; set; }
        public List<IFormFile>? UploadedImages { get; set; }

    }
}
