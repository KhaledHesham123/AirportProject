using FlyingProject.Project.core.Entities.main;
using FlyingProject.Shared.Specification;

namespace FlyingProject.Project.core.NewFolder.InterfaceContrect
{
    public interface ISetsRepo : IRepo<Seat>
    {
        Task<int> SeatsCount(int id);

    }
}
