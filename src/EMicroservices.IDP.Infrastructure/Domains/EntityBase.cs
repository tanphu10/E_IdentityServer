namespace EMicroservices.IDP.Infrastructure.Domains
{
    public abstract class EntityBase<Tkey> : IEntityBase<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
