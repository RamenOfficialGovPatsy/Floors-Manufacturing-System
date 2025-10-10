using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace Master_Floor_Project.Services
{
    public class ProductService : IProductService
    {
        public async Task<List<Product>> GetProductsAsync()
        {
            using var context = new AppDbContext();
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var context = new AppDbContext();
            return await context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            using var context = new AppDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var context = new AppDbContext();
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            using var context = new AppDbContext();
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }
    }
}