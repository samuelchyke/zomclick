using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    public abstract class SeedEntity
    {
        public string tableName { get; protected set; }

        // Define keys for the primary key columns
        public abstract List<PropertyInfo> Keys { get; }

        public string KeyNames => string.Join(",", Keys.Select(k => k.Name));

        public string KeyNamesSQLSeparator => string.Join(" || ", Keys.Select(k => k.Name));

        public string Identifier => string.Join("", Keys.Select(k => k.GetValue(this)?.ToString()));

        // Lazy loading the filtered members to get only properties that exist in Columns
        private readonly Lazy<List<string>> _coloumns;
        private readonly Lazy<List<PropertyInfo>> _filteredMembers;


        public SeedEntity()
        {
            _coloumns = new Lazy<List<string>>(() => 
                GetType().GetProperties()
                .Where(p => p.CanRead && p.CanWrite && !p.Name.Equals("tableName", StringComparison.OrdinalIgnoreCase))
                .Select(p => p.Name)
                .ToList());

            // Initialize the lazy-loaded _filteredMembers list
            _filteredMembers = new Lazy<List<PropertyInfo>>(() =>
                GetType().GetProperties()
                    .Where(p => _coloumns.Value.Contains(p.Name))  // Use the instance property Columns
                    .ToList());
        }


        // Columns that correspond to the database columns
        public List<string> Columns => _coloumns.Value;

        // Public property to access filteredMembers lazily
        public List<PropertyInfo> FilteredMembers => _filteredMembers.Value;

        // Method to bind properties to the SQLite statement
        public void BindStatement(SQLiteCommand command, List<PropertyInfo> filteredMembers)
        {
            foreach (var property in filteredMembers)
            {
                var value = property.GetValue(this);  // Get the value of the property
                var paramName = $"@{property.Name}";  // Create a named parameter for the property

                // Bind by parameter name
                if (value == null)
                {
                    command.Bind(paramName, null);  // Bind null directly
                }
                else
                {
                    switch (Type.GetTypeCode(property.PropertyType))
                    {
                        case TypeCode.String:
                            command.Bind(paramName, value.ToString());  // Bind string value
                            break;

                        case TypeCode.Int32:
                            command.Bind(paramName, Convert.ToInt32(value));  // Bind integer value
                            break;

                        case TypeCode.Boolean:
                            command.Bind(paramName, (bool)value ? 1 : 0);  // SQLite uses 1/0 for booleans
                            break;

                        case TypeCode.Double:
                            command.Bind(paramName, Convert.ToDouble(value));  // Bind double value
                            break;

                        case TypeCode.Single:
                            command.Bind(paramName, Convert.ToSingle(value));  // Bind float value
                            break;

                        case TypeCode.Int64:
                            command.Bind(paramName, Convert.ToInt64(value));  // Bind long value
                            break;

                        case TypeCode.DateTime:
                            command.Bind(paramName, ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));  // Bind DateTime as string
                            break;

                        default:
                            if (property.PropertyType.IsEnum)
                            {
                                command.Bind(paramName, Convert.ToInt32(value));  // Bind enum as integer
                            }
                            else
                            {
                                command.Bind(paramName, value.ToString());  // Fallback: bind other types as string
                            }
                            break;
                    }
                }
            }
        }
    }
}