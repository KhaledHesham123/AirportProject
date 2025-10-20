using FlyingProject.Project.core.DTOS.TicktDto;
using FlyingProject.Project.core.Entities.main;

namespace FlyingProject.Project.core.ServiceContrect
{
    public interface IFlightBookOrchestor
    {
       Task<Flight> FlightBook(CreateTicktDto createTicktDto);

    }
}
