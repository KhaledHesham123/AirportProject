using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using System.Collections;

namespace FlyingProject.Project.core
{
    public interface IunitofWork:IDisposable
    {
        IFlightRepo FlightRepo { get; }
        ISetsRepo SetsRepo { get; }

        ITicketsrepo TicketsRepo { get; }



        Task<int> CompleteAsync();

        public void Dispose();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
