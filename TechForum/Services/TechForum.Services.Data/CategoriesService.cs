namespace TechForum.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoriesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All().Where(x => x.Name == name).To<T>().FirstOrDefault();

            return category;
        }
    }
}
