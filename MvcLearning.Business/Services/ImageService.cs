using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities.Images;
using MvcLearning.Data.Entities;
using MvcLearning.Business.Models.Product;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MvcLearning.Business.Services
{
    public class ImageService
    {
        public async Task AddImageToProduct(
    ProductModel productAddingModel,
    string imagesPath,
    Product product,
    string shopId,
    CancellationToken token = default)
        {
            const int MaxWidth = 1024;

            foreach (var image in productAddingModel.UploadedImages)
            {
                if (image.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var fullPath = Path.Combine(imagesPath, fileName);

                    // Чтение и ресайз изображения
                    using var inputStream = image.OpenReadStream();
                    using var img = await Image.LoadAsync(inputStream, token);

                    if (img.Width > MaxWidth)
                    {
                        var ratio = (double)MaxWidth / img.Width;
                        var newHeight = (int)(img.Height * ratio);

                        img.Mutate(x => x.Resize(MaxWidth, newHeight));
                    }

                    // Сохранение
                    await img.SaveAsync(fullPath, token);

                    product.Images.Add(new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        Url = $"/images/products/{shopId}/{product.Id}/{fileName}"
                    });
                }
            }
        }
    }
}
