using System;

[System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class TollFreeAttribute : Attribute
{
    // This is a positional argument
    public TollFreeAttribute()
    {   
    }
}