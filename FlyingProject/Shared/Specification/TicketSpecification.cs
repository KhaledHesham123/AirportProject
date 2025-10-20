using FlyingProject.Project.core.Entities.main;

namespace FlyingProject.Shared.Specification
{
    public class TicketSpecification:BaseSpecification<Ticket>
    {
        public TicketSpecification(int? id):base(x=>x.Id==id)
        {

        }
        
            
        
    }
}
