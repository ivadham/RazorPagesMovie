using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.NorthwindProducts
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.NorthwindContext _context;

        public IndexModel(RazorPagesMovie.Data.NorthwindContext context)
        {
            _context = context;
        }

        //https://blog.jetbrains.com/dotnet/2023/09/21/eager-lazy-and-explicit-loading-with-entity-framework-core/

        public IList<Product> Product { get;set; } = default!;
        public IList<Product> LazyProducts { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            /* SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder], [c].[CategoryID], [c].[CategoryName], [c].[Description], [c].[Picture], [s].[SupplierID], [s].[Address], [s].[City], [s].[CompanyName], [s].[ContactName], [s].[ContactTitle], [s].[Country], [s].[Fax], [s].[HomePage], [s].[Phone], [s].[PostalCode], [s].[Region]
            FROM [Products] AS [p]
            LEFT JOIN [Categories] AS [c] ON [p].[CategoryID] = [c].[CategoryID]
            LEFT JOIN [Suppliers] AS [s] ON [p].[SupplierID] = [s].[SupplierID] */


            LazyProducts = await _context.Products.ToListAsync();
            /*SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
            FROM [Products] AS [p] */

            foreach (var lazyProduct in LazyProducts)
            {
                _context.Entry(lazyProduct).Collection(p => p.OrderDetails).Load(); //Explicit loading
                foreach (var orderdet in lazyProduct.OrderDetails)
                {
                    Console.WriteLine($"Product: {lazyProduct.ProductName}");
                    Console.WriteLine($"- OrderID: {orderdet.OrderId}");
                    Console.WriteLine($"- UnitPrice: {orderdet.UnitPrice}");
                    Console.WriteLine($"- Quantity: {orderdet.Quantity}");
                    Console.WriteLine($"- Discount: {orderdet.Discount}");
                }
            }
        }
    }
}
