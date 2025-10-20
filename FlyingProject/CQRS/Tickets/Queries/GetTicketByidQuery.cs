using FlyingProject.Project.core;
using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using FlyingProject.Project.Repo.Repositories;
using FlyingProject.Shared.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlyingProject.CQRS.Tickets.Queries
{
    public record GetTicketByidQuery(int? id):IRequest<Ticket>;

    public class GetTicketByidQueryHandler : IRequestHandler<GetTicketByidQuery, Ticket?>
    {
        private readonly IRepo<Ticket> ticketrepository;

        public GetTicketByidQueryHandler(IRepo<Ticket> Ticketrepository)
        {
            ticketrepository = Ticketrepository;
        }
        public async Task<Ticket?> Handle(GetTicketByidQuery request, CancellationToken cancellationToken)
       {
            if (request.id==null)
            {
                return null;
            }
            //var ticket= await _unitofWork.TicketsRepo.GetidAsync(spec);
            var ticket = await ticketrepository.
                GetByIdQueryable(request.id.Value).Include(x=>x.User).Include(x=>x.Seat).Include(x=>x.Flight).FirstOrDefaultAsync(cancellationToken);

            if (ticket == null) return null;
            return ticket;



        }
    }
}
