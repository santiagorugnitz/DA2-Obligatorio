using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class CategoryHandler : ICategoryHandler
    {
        private IRepository<Category> repository;

        public CategoryHandler(IRepository<Category> categoryRepository)
        {
            repository = categoryRepository;
        }
        public List<Category> GetAll()
        {
            return repository.GetAll().ToList();
        }
    }
}
