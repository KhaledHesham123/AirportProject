using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using FlyingProject.Project.Repo.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FlyingProject.Project.Repo.Repositories
{
    public class TicketRepo : Repository<Ticket>, ITicketsrepo
    {
        public TicketRepo(AirlineDbContext dbContext) : base(dbContext)
        {

        }
        // بجيب هل في كراسي فاضيه ولا لا عدد التيكست ناقص عدد الكراسي 
        public async Task<int> TicketsCount(int flightId)
        {
            return await _dbContext.Set<Ticket>()
       .CountAsync(s => s.FlightId == flightId);

        }
        
    }
}
