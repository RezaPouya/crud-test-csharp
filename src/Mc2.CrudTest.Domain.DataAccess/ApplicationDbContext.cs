using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Mc2.CrudTest.Domain.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationDbContext> _logger;
        // for testing

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IMediator mediator,
            ILogger<ApplicationDbContext> logger) : base(options)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var dbResult = await base.SaveChangesAsync(cancellationToken);
            await DispatchDomainEvents();
            return dbResult;
        }

        public override int SaveChanges()
        {
            var dbResult = base.SaveChanges();
            DispatchDomainEvents().GetAwaiter().GetResult();
            return dbResult;
        }

        private async Task DispatchDomainEvents()
        {
            var entities = this.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}