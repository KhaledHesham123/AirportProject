using FlyingProject.Project.core.Entities.main;
using FlyingProject.Shared.Specification;

namespace FlyingProject.Project.core.NewFolder.InterfaceContrect
{
    public interface ITicketsrepo : IRepo<Ticket>
    {
        Task<int> TicketsCount(int id);

    }
}
