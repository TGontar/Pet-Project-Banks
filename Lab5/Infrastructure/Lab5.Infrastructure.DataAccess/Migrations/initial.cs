using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
    create type operation as enum
    (
        'addMoney',
        'withdrawMoney'
    );

    create table accounts
    (
        account_id bigint primary key generated always as identity ,
        account_name text not null ,
        account_password text not null ,
        account_money double precision not null
    );

    create table admins
    (
        admin_id bigint primary key generated always as identity ,
        admin_name text not null ,
        admin_password text not null
    );

    create table history
    (
        history_id bigint primary key generated always as identity ,
        account_id bigint not null,
        operation operation not null ,
        money double precision not null
    );

    insert into admins (admin_name, admin_password)
    values ('timur', 'sigma');
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
    drop table accounts;
    drop table admins;
    drop table history;

    drop type operation;
    """;
}