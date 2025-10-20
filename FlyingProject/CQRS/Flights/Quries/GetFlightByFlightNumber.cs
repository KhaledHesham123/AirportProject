using FlyingProject.Project.core;
using FlyingProject.Project.core.DTOS.FlightsDto;
using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using FlyingProject.Project.core.ServiceContrect;
using FlyingProject.Project.Repo.Repositories;
using FlyingProject.Shared.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace FlyingProject.CQRS.Flights.Quries
{
    public record GetFlightByFlightNumber(string FlightNumber): IRequest<IEnumerable<AvailableFlightDto?>>;

    public class GetFlightByFlightNumberHandler : IRequestHandler<GetFlightByFlightNumber, IEnumerable<AvailableFlightDto?>>
    {
        private readonly IRepo<Flight> flightrepo;
        private readonly IFlightAvailabilityService flightAvailabilityService;

        public GetFlightByFlightNumberHandler(IRepo<Flight> flightrepo, IFlightAvailabilityService flightAvailabilityService)
        {
         
            this.flightrepo = flightrepo;
            this.flightAvailabilityService = flightAvailabilityService;
        }

        public async Task<IEnumerable<AvailableFlightDto?>> Handle(GetFlightByFlightNumber request, CancellationToken cancellationToken)
        {
            var flight = await flightrepo.GetAll()
                .Where(x=>x.FlightNumber==request.FlightNumber)
                .Include(f => f.Aircraft).ThenInclude(a => a.Airline)
                .Include(x=>x.Tickets).Include(x=>x.Seats)
                .ToListAsync();
            if (flight == null) return null;

            return await flightAvailabilityService.GetAvailableFlightsAsync(flight);
        }


    }
}
