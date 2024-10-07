using System;

// Define the custom attribute for serial names
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class SerialNameAttribute : Attribute
{
    // Property to hold the value passed into the attribute
    public string Value { get; }

    // Constructor for the attribute
    public SerialNameAttribute(string value)
    {
        Value = value;
    }
}