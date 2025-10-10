// Services/ProductService.cs
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
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                Debug.WriteLine("🟡 ProductService: Начало загрузки продуктов из БД...");
                var products = await _context.Products.ToListAsync();
                Debug.WriteLine($"🟢 ProductService: Успешно загружено {products.Count} продуктов");

                foreach (var product in products)
                {
                    Debug.WriteLine($"   - {product.Article}: {product.Name}");
                }
                return products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 ProductService: Ошибка при загрузке продуктов: {ex.Message}");
            }
            return new List<Product>();
            // return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}