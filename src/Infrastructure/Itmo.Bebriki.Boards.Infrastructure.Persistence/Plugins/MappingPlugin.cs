using Itmo.Dev.Platform.Persistence.Postgres.Plugins;
using Npgsql;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Plugins;

/// <summary>
///     Plugin for configuring NpgsqlDataSource's mappings
///     ie: enums, composite types
/// </summary>
public class MappingPlugin : IPostgresDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder dataSource) { }
}