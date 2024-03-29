﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using People.ModelsDB;
using People.Repositories;

namespace PeopleTests
{
    public class PeopleAccessObject : IDisposable
    {
        public PersonContext peopleContext { get; }
        public IPersonRepository personRepository { get; }

        public PeopleAccessObject()
        {
            var builder = new DbContextOptionsBuilder<PersonContext>();
            builder.UseInMemoryDatabase("person");

            peopleContext = new PersonContext(builder.Options);
            personRepository = new PersonRepository(peopleContext, NullLogger<PersonRepository>.Instance);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                peopleContext.Database.EnsureDeleted();
                peopleContext?.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}