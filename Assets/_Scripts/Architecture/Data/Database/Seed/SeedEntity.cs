using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;
public abstract class SeedEntity
{
    public string tableName { get; protected set; }

    // Define keys for the primary key columns
    public abstract List<PropertyInfo> Keys { get; }

    public string KeyNames => string.Join(",", Keys.Select(k => k.Name));

    public string KeyNamesSQLSeparator => string.Join(" || ", Keys.Select(k => k.Name));

    public string Identifier => string.Join("", Keys.Select(k => k.GetValue(this)?.ToString()));

    public List<string> Columns => GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(p => p.Name).ToList();

    // Get the filtered properties based on the constructor parameters
    public List<PropertyInfo> FilteredMembers =>
        GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => Columns.Contains(p.Name)).ToList();

    // Method to bind properties to the SQLite statement
    public void BindStatement(SQLiteCommand command, List<PropertyInfo> filteredMembers)
    {
        foreach (var property in filteredMembers)
        {
            var value = property.GetValue(this);
            var paramName = $"@{property.Name}"; // Named parameter based on the property name

            if (value == null)
            {
                command.Bind(paramName, null); // Bind null directly
            }
            else
            {
                switch (Type.GetTypeCode(property.PropertyType))
                {
                    case TypeCode.String:
                        command.Bind(paramName, value.ToString());
                        break;

                    case TypeCode.Int32:
                        command.Bind(paramName, Convert.ToInt32(value));
                        break;

                    case TypeCode.Boolean:
                        command.Bind(paramName, (bool)value ? 1 : 0); // SQLite uses 1/0 for booleans
                        break;

                    case TypeCode.Double:
                        command.Bind(paramName, Convert.ToDouble(value));
                        break;

                    case TypeCode.Single:
                        command.Bind(paramName, Convert.ToSingle(value));
                        break;

                    case TypeCode.Int64:
                        command.Bind(paramName, Convert.ToInt64(value));
                        break;

                    default:
                        if (property.PropertyType.IsEnum)
                        {
                            command.Bind(paramName, Convert.ToInt32(value)); // Enums are stored as integers
                        }
                        else
                        {
                            // Fallback to converting the value to a string
                            command.Bind(paramName, value.ToString());
                        }
                        break;
                }
            }
        }
    }
}