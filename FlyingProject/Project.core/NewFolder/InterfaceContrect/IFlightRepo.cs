using FlyingProject.Project.core.Entities.main;
using FlyingProject.Shared.Specification;

namespace FlyingProject.Project.core.NewFolder.InterfaceContrect
{
    public interface IFlightRepo:IRepo<Flight>
    {

        public Task<IEnumerable<Flight>> Getupcomingflights(ISpecification<Flight> spec);
        public Task<IEnumerable<Flight?>> GetFlights_ByFlightNumber(ISpecification<Flight> spec);




    }
}
