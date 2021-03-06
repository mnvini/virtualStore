﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinicius.VirtualStore.Domain.Repository;
using Vinicius.VirtualStore.Web.Models;

namespace Vinicius.VirtualStore.Web.Controllers
{
    public class ShowcaseController : Controller
    {

        private RepositoryProducts _repository;
        public int ProductsPerPage = 5;

        public ViewResult ProductsList(string category, int page = 1)
        {
            _repository = new RepositoryProducts();
 
            ProductsViewModel model = new ProductsViewModel
            {
                Products = _repository.Produto
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductDescription)
                .Skip((page - 1) * ProductsPerPage)
                .Take(ProductsPerPage),

                Pagination = new Pagination
                {
                    CurrentPage = page,
                    ItemsPerPage = ProductsPerPage,
                    TotalItems = category == null ? _repository.Produto.Count() : _repository.Produto.Count(p => p.Category == category)
                },

                CurrentCategory = category
                
            };

            return View(model);
        }
    }
}