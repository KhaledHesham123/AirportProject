using FlyingProject.Project.core;
using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using FlyingProject.Project.Repo.Repositories;
using FlyingProject.Shared.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlyingProject.CQRS.Flights.Quries
{
    public record GetFlightbyidQyery(int? id):IRequest<Flight>;
    
    public class GetFlightbyidQyeryHandler : IRequestHandler<GetFlightbyidQyery, Flight?>
    {
        private readonly IRepo<Flight> flightrepo;

        public GetFlightbyidQyeryHandler(IRepo<Flight> flightrepo)
        {
            this.flightrepo = flightrepo;
        }

        public async Task<Flight?> Handle(GetFlightbyidQyery request, CancellationToken cancellationToken)
        {
            if (request?.id is null or 0)
                return null; // أو return default;

            var fligt = await flightrepo.GetByIdQueryable(request.id.Value).Include(f => f.Aircraft).ThenInclude(a => a.Airline)
                .Include(f => f.Seats).Include(f => f.Tickets).ThenInclude(t => t.User)
                .FirstOrDefaultAsync(cancellationToken);
            if (fligt == null) return null;
            return fligt;
        }
    }
}
