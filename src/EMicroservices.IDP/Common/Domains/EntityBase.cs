namespace EMicroservice.IDP.Common.Domains
{
    public abstract class EntityBase<Tkey> : IEntityBase<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
