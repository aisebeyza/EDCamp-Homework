﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId = 1,CategoryName = "Pc"},
                new Category{CategoryId = 2,CategoryName = "Phone"},
            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId = 1,CategoryId = 1,ProductName = "Hp Laptop",QuantityPerUnit = "16gb ram",UnitPrice = 10000,UnitsInStock = 5},
                new Product{ProductId = 2,CategoryId = 1,ProductName = "Acer Laptop",QuantityPerUnit = "4gb ram",UnitPrice = 8000,UnitsInStock = 3},
                new Product{ProductId = 3,CategoryId = 1,ProductName = "Asus Laptop",QuantityPerUnit = "8gb ram",UnitPrice = 8000,UnitsInStock = 2},
                new Product{ProductId = 4,CategoryId = 2,ProductName = "samsung phone",QuantityPerUnit = "4gb ram",UnitPrice = 5000,UnitsInStock =15},
                new Product{ProductId = 5,CategoryId = 2,ProductName = "sony phone",QuantityPerUnit = "4gb ram",UnitPrice = 6000,UnitsInStock =0},
            };



            // test(products);

            // GetProducts(products);

            // AnyTest(products);

            //ürün detayına gitmek için
            // FindTest(products);

            //şarta uyan tüm verileri getirir--list 
            // FindAllTest(products);

            // default asc
            // AscDescTest(products);

            // ClassicLinqTest(products);
           
            //ilişkisel tasarım

            var result = from p in products
                join c in categories on p.CategoryId equals c.CategoryId
                         where p.UnitPrice>5000
                orderby p.UnitPrice descending 
                select new ProductDto {ProductId =p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice, };

            foreach (var productDto in result)
            {
                Console.WriteLine("{0} -Price: {1}",productDto.ProductName,productDto.UnitPrice);
            }
        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products
                where p.UnitPrice > 6000
                orderby p.UnitPrice descending, p.ProductName ascending
                select new ProductDto {ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice};

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //single line query
            var result = products.Where(p => p.ProductName.Contains("top")).OrderByDescending(p => p.UnitPrice)
                .ThenByDescending(p => p.ProductName);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("top"));
            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 3);
            Console.WriteLine(result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Acer Laptop");
            Console.WriteLine(result);
        }

        private static void test(List<Product> products)
        {
            Console.WriteLine("Algoritmik***********");
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }

            Console.WriteLine("Linq**********");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filteredProducts = new List<Product>();

            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    filteredProducts.Add(product);
                }
            }

            return filteredProducts;

        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
           return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3).ToList();
        }

    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }

    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
