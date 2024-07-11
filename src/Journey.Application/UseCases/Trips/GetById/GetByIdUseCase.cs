using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetByIdUseCase
    {
        public ResponseTripJson Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();

            var trip = dbContext.Trips
                .Include(x => x.Activities)
                .FirstOrDefault( x => x.Id == id);

            if( trip is null)
            {
                throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Id = trip.Id,
                EndDate = trip.EndDate,
                StartDate = trip.StartDate,
                Name = trip.Name,
                Activities = trip.Activities.Select(x => new ResponseActivityJson
                {
                    Date = x.Date,
                    Name = x.Name,
                    Id = x.Id,
                    Status = (Communication.Enums.ActivityStatus)x.Status
                }).ToList(),
            };
        }
    }
}
