using System;

public interface IResource
{
    Type Type { get; }
    int Quantity { get; }
}
