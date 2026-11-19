## 1. What a project looks like

Create one with `dotnet new webapi -n MyApi` and you get roughly:

```
MyApi/
├── Program.cs              ← the entire app's entry point + setup
├── MyApi.csproj            ← project file (like package.json: target framework, packages)
├── appsettings.json        ← configuration
├── appsettings.Development.json
├── Properties/launchSettings.json   ← dev ports, env vars
└── Controllers/            ← (if using controllers)
```

- Key difference from **Express**: there is no separate app.js and server.js. Program.cs is both the place where you register services and the place where you build the request pipeline.

## 2. Program.cs: two phases

Modern .NET (6+) uses **`“top-level statements”`**, so no Main method is visible. A minimal Program.cs:

```csharp
var builder = WebApplication.CreateBuilder(args);   // PHASE 1: setup

// Register services (dependency injection container)
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();                           // the line that separates phases

// PHASE 2: build the request pipeline (middleware)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();                                           // start Kestrel, block
```

- Phase 1 (before `Build(`)): configure the `builder`. Register services (`builder.Services.Add...`), configuration, logging. This is the DI container setup.

- Phase 2 (after `Build()`): configure the `app`. Add middleware (`app.Use...`) and map endpoints (`app.Map...`). The services container is now frozen; you can’t register new services after Build().

- `WebApplication.CreateBuilder` does a lot for you: sets up Kestrel (the built-in web server), loads `appsettings.json` and environment variables, configures logging, and sets up DI.

## 3. The request pipeline

Every HTTP request flows through a chain of middleware. Each middleware can:

- Do something `before` the next middleware
- Call the `next` one (or short-circuit and respond itself)
- Do something `after` the next one returns

- Think of it as Express middleware, except the “after” part is built in naturally, with no separate `error-handler` convention:

```
Request  →  [Middleware A] → [Middleware B] → [Middleware C] → Endpoint
Response ←  [Middleware A] ← [Middleware B] ← [Middleware C] ←
```

- You can write one inline:

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine($"Before: {context.Request.Path}");
    await next(context);                    // like next() in Express
    Console.WriteLine($"After: {context.Response.StatusCode}");
});
```

Here is how the three `registration` methods differ:

| Method       | Meaning                                                  |
| ------------ | -------------------------------------------------------- |
| app.Use(...) | Middleware that can call next                            |
| app.Run(...) | Terminal middleware; never calls next, ends the pipeline |
| app.Map(...) | Branches the pipeline based on path                      |

## 4. Middleware order matters

Middleware executes in the order you register it. This is the same principle as Express’s `app.use()` order, but with more built-in middleware that depends on ordering. The recommended order from Microsoft:

```
1. Exception handling       (UseExceptionHandler)   ← first, so it catches errors from everything below
2. HSTS / HTTPS redirection (UseHsts, UseHttpsRedirection)
3. Static files             (UseStaticFiles)
4. Routing                  (UseRouting)            ← implicit in minimal hosting
5. CORS                     (UseCors)
6. Authentication           (UseAuthentication)     ← who are you?
7. Authorization            (UseAuthorization)      ← are you allowed?
8. Your custom middleware
9. Endpoints                (MapControllers, MapGet...)
```

- Why order matters, with two examples:
  - `UseAuthorization()` placed before `UseAuthentication()` breaks: it tries to check permissions before knowing who the user is.
  - Exception handling placed last won’t catch exceptions thrown by middleware before it. It only wraps what comes after it, because each middleware wraps the ones below it (see the diagram above).

## 5. Endpoints are the end of the pipeline

`MapControllers()`, `MapGet()`, etc. register endpoints. In modern ASP.NET Core, routing works in two steps: the routing middleware selects which endpoint matches the URL, then later middleware (auth, CORS) can inspect that choice, and finally the endpoint executes. This is why `UseAuthorization` can know which endpoint has `[Authorize]` on it.

Quick mental model

| Express                  | ASP.NET Core                                    |
| ------------------------ | ----------------------------------------------- |
| `app.use(fn)`              | `app.Use`(...)                                    |
| `next()`                   | await `next`(context)                             |
| app.get('/x', handler)   | app.MapGet("/x", handler)                       |
| order of app.use calls   | order of app.Use... calls                       |
| `req`, `res`                 | HttpContext (context.Request, context.Response) |
| `require`/imports + config | builder.Services + builder.Configuration        |

