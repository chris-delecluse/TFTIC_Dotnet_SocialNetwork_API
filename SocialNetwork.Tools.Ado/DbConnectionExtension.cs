using System.Data;
using System.Reflection;

namespace SocialNetwork.Tools.Ado;

public static class DbConnectionExtension
{
    #region ExecuteNonQuery

    public static int ExecuteNonQuery(
        this IDbConnection dbConnection,
        string query,
        bool isStoredProcedure = false,
        object? parameters = null
    )
    {
        if (dbConnection.State is not ConnectionState.Open)
            throw new InvalidOperationException("The connection must be open.");

        using IDbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters);
        return dbCommand.ExecuteNonQuery();
    }

    #endregion

    #region ExecuteScalar

    public static object? ExecuteScalar(
        this IDbConnection dbConnection,
        string query,
        bool isStoredProcedure = false,
        object? parameters = null
    )
    {
        if (dbConnection.State is not ConnectionState.Open)
            throw new InvalidOperationException("The connection must be open.");

        using IDbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters);
        object? result = dbCommand.ExecuteScalar();

        return result is DBNull ? null : result;
    }

    #endregion

    #region ExecuteReader

    public static IEnumerable<TResult> ExecuteReader<TResult>(
        this IDbConnection dbConnection,
        string query,
        Func<IDataRecord, TResult> selector,
        bool isStoredProcedure = false,
        object? parameters = null
    )
    {
        if (dbConnection.State is not ConnectionState.Open)
            throw new InvalidOperationException("The connection must be open.");

        using IDbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters);
        using IDataReader reader = dbCommand.ExecuteReader();
        
        while (reader.Read())
        {
            yield return selector(reader);
        }
    }

    #endregion

    #region CreateCommand

    private static IDbCommand CreateCommand(
        IDbConnection connection,
        string query,
        bool isStoredProcedure,
        object? parameters
    )
    {
        if (string.IsNullOrEmpty(query?.Trim())) throw new ArgumentException(nameof(query));

        IDbCommand dbCommand = connection.CreateCommand();
        dbCommand.CommandText = query;
        dbCommand.CommandType = isStoredProcedure
            ? CommandType.StoredProcedure
            : CommandType.Text;

        if (parameters is not null)
        {
            Type type = parameters.GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                IDataParameter dataParameter = dbCommand.CreateParameter();
                dataParameter.ParameterName = propertyInfo.Name;
                dataParameter.Value = propertyInfo.GetValue(parameters, null) ?? DBNull.Value;

                dbCommand.Parameters.Add(dataParameter);
            }
        }

        return dbCommand;
    }

    #endregion
}
