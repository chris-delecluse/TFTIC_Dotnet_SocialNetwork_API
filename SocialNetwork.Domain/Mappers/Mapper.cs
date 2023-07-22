using System.Data;

namespace SocialNetwork.Domain.Mappers;

internal static class Mapper
{
    internal static T GetValueOrDefault<T>(IDataRecord record, string columnName)
    {
        return (record[columnName] != DBNull.Value ? (T)record[columnName] : default(T))!;
    }
}
