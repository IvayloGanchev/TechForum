namespace TechForum.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name, int? take = null, int skip = 0);

      
    }
}
