using System;
namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
// Define the custom attribute for serial names
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SerialNameAttribute : Attribute
    {
        // Property to hold the value passed into the attribute
        public string value { get; }

        // Constructor for the attribute
        public SerialNameAttribute(string value)
        {
            this.value = value;
        }
    }
}