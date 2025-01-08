using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250108215403, "table topic_tasks")]
internal sealed class TableTopicTasksMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table topic_tasks (
            topic_id bigint not null references topics(topic_id) on delete cascade,
            task_id bigint not null,
        
            primary key (topic_id, task_id)
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table topic_tasks;
        """;
    }
}