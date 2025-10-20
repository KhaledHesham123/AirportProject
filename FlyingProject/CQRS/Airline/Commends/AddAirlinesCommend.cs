using FlyingProject.Project.core.DTOS.AirlineDto;
using FlyingProject.Project.core.Entities.main;
using MediatR;

namespace FlyingProject.CQRS.Airline.Commends
{
    public record AddAirlinesCommend(AirlineCreateDto AirlineCreateDto):IRequest<Airlines>;

    public class AddAirlinesCommendHandler : IRequestHandler<AddAirlinesCommend, Airlines>
    {
        public Task<Airlines> Handle(AddAirlinesCommend request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
