﻿namespace EMicroservice.IDP.Common.Domains
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
