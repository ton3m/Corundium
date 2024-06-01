using System;
public interface ITool
{
    //public int Index { get; } 
    public float BaseDamage { get; }
    public float CalculateDamage(Type hitObjectType);
}