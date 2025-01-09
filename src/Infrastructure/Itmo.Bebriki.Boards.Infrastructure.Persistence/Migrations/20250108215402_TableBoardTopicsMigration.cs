using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250108215402, "table board_topics")]
public sealed class TableBoardTopicsMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table board_topics (
            board_id bigint not null references boards(board_id) on delete cascade,
            topic_id bigint not null references topics(topic_id) on delete cascade,

            primary key (board_id, topic_id)
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table board_topics;
        """;
    }
}