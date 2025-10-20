using FlyingProject.Project.core.Entities.main;
using FlyingProject.Shared.Specification;

namespace FlyingProject.Project.core.NewFolder.InterfaceContrect
{
    public interface IRepo<T> where T : BaseEntity

    {
        //Task<T> GetidAsync(ISpecification<T> spec);

        //Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec);

         IQueryable<T> GetAll();

         IQueryable<T> GetByIdQueryable(int id);

        void SaveInclude(T entity);

        Task addAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(T item);

        public Task<int> SaveChanges();

    }
}
