using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250108215400, "table boards")]
public sealed class TableBoardsMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table boards (
            board_id bigint primary key generated always as identity,
            name text not null,
            description text not null,
            updated_at timestamp with time zone not null
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table boards;
        """;
    }
}