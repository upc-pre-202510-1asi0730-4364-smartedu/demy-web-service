using Microsoft.EntityFrameworkCore;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Model builder extensions for snake_case and plural naming conventions. 
/// </summary>
/// <remarks>
///     This class contains extension methods for the model builder.
///     It includes a method to use the snake case and/or plural naming convention according to an object type.
///     It also pluralizes the table names.
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies snake_case naming to all the tables, columns, keys, foreign keys, and indexes.
    /// </summary>
    /// <param name="builder">The ModelBuilder instance</param>
    /// <remarks>
    ///     This method sets the naming convention for the database tables, columns, keys, foreign keys and indexes to snake
    ///     case.
    /// </remarks>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
                entity.SetTableName(tableName.ToPlural().ToSnakeCase());

            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName))
                    key.SetName(keyName.ToSnakeCase());
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if (!string.IsNullOrEmpty(foreignKeyName))
                    foreignKey.SetConstraintName(foreignKeyName.ToSnakeCase());
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexDatabaseName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexDatabaseName))
                    index.SetDatabaseName(indexDatabaseName.ToSnakeCase());
            }
        }
    }
}