using System;
using System.Linq;
using System.Reflection;

public abstract class DomainModel<T>
{
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        // Get all properties of the class
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        return properties.All(p => 
            Equals(p.GetValue(this), p.GetValue(obj)));
    }

    public override int GetHashCode()
    {
        // Get all properties of the class
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        // Combine hash codes of all properties
        return properties
            .Select(p => p.GetValue(this)?.GetHashCode() ?? 0)
            .Aggregate(0, (acc, next) => acc ^ next);
    }

    // public static bool operator == (T left, T right)
    // {
    //     if (left is null)
    //         return right is null;

    //     return left.Equals(right);
    // }

    // public static bool operator != (T left, T right)
    // {
    //     return !(left == right);
    // }
}