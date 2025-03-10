﻿using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();

            var trip = dbContext
                .Trips
                .Include(x => x.Activities)
                .FirstOrDefault(x => x.Id == id);

            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND);
            }

            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();

        }
    }
}
