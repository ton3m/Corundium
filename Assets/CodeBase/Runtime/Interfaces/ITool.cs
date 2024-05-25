using System;
public interface ITool
{
    //public int Index { get; } 
    public int BaseDamage { get; }
    public int CalculateDamage(Type hitObjectType);
}