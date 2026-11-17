# LINQ = Language Integrated Query.

- It allows you to query collections and data sources using C#.

```c#
protected override void OnConfiguring(
    DbContextOptionsBuilder options)
{
    var connectionString =
        Environment.GetEnvironmentVariable(
            "ConnectionStrings__Default"
        );

    options.UseNpgsql(connectionString);
}
```

- `protected override void OnConfiguring(...)` overrides a built-in DbContext method to define which database engine and server EF Core should connect to.

- `Environment.GetEnvironmentVariable("ConnectionStrings__Default")` reads your database address, database name, username, and password from an environment variable rather than hardcoding secrets in your source code. The double underscore (__) is .NET's standard convention for representing nested configuration keys (ConnectionStrings:Default).

- `options.UseNpgsql(connectionString);` registers Npgsql as the database provider, instructing EF Core to generate SQL dialect tailored specifically for PostgreSQL.