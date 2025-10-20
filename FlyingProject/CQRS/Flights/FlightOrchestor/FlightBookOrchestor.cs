using FlyingProject.CQRS.Flights.Quries;
using FlyingProject.CQRS.Passnger.Commends;
using FlyingProject.CQRS.Seats.Coomends;
using FlyingProject.CQRS.Tickets.Commends;
using FlyingProject.Project.core;
using FlyingProject.Project.core.DTOS.TicktDto;
using FlyingProject.Project.core.Entities.main;
using FlyingProject.Project.core.ServiceContrect;
using MediatR;
using System.Net.Sockets;
using static FlyingProject.CQRS.Passnger.Query.GetPassngerbyPassportNumberQuery;

namespace FlyingProject.CQRS.Flights.FlightOrchestor
{
  
    public class FlightBookOrchestor: IFlightBookOrchestor
    {
        private readonly IMediator _mediator;

        public FlightBookOrchestor(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<Flight> FlightBook(CreateTicktDto createTicktDto) 
        {

            var flight= await _mediator.Send(new GetFlightbyidQyery(createTicktDto.FlightId));

            int totalSeats = flight.Seats.Count;
            int bookedTickets = flight.Tickets.Count;
            int availableSeats = totalSeats - bookedTickets;

            if (availableSeats <= 0)
                throw new Exception("No available seats for this flight");

            var passenger = await _mediator.Send(new GetPassngerbyNumberQuery(createTicktDto.PassengerPassportNumber));

            if (passenger == null)
            {
                return null;
            }

            var seatType = string.IsNullOrWhiteSpace(createTicktDto.SeatType)
                ? "Economy"
                : createTicktDto.SeatType.Trim();

            var availableSeat = await _mediator.Send(new GetAvailableSeatQuery(flight, seatType));
            if (availableSeat == null)
                throw new Exception("No available seat found for this class");


            var Ticket = new Ticket()
            {
                FlightId = createTicktDto.FlightId,
                UserId = passenger?.Id ?? 0,
                Price = CalculatePrice(flight, availableSeat),
                Seat = availableSeat,
                SeatId = availableSeat.Id,
                Flight = flight,
                User = passenger

            };

            // 5️⃣ أضف التذكرة للـ Repo
            await _mediator.Send(new AddTicketCommend(Ticket));

            return flight;

        }

        private  decimal CalculatePrice(Flight flight, Seat seat)
        {
            decimal basePrice = 1000;
            switch (seat.Class)
            {
                case "Business":
                    return basePrice * 2m;
                case "First":
                    return basePrice * 1.4m;
                default:
                    return basePrice;

            }
            return basePrice;
        }

    }
}
