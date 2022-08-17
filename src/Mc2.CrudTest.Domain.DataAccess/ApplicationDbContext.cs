using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mc2.CrudTest.Domain.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;
        // for testing

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IMediator mediator) : base(options)
        {
            _mediator = mediator;
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
            await DispatchDomainEvents(_mediator, this);
            return dbResult;
        }

        public override int SaveChanges()
        {
            var dbResult = base.SaveChanges();
            DispatchDomainEvents(_mediator, this).GetAwaiter().GetResult();
            return dbResult;
        }

        private static async Task DispatchDomainEvents(IMediator mediator, DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}