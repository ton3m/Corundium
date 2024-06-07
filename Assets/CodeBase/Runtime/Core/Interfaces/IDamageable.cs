using System;

public interface IDamageable
{
    Type Type { get; }
    void ApplyDamage(float damage);
}
