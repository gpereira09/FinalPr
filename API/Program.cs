using API.models;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

// Rota de teste
app.MapGet("/", () => "Hello World!");

// Rota para cadastrar tarefas
app.MapPost("/api/tarefas/cadastrar", ([FromBody] Tarefa tarefa, [FromServices] AppDataContext ctx) =>
{
    ctx.Tarefas.Add(tarefa); 
    ctx.SaveChanges();         
    return Results.Created("", tarefa); 
});

app.MapGet("/api/tarefas/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Tarefas.Any())
    {
        return Results.Ok(ctx.Tarefas.ToList());
    } 
    return Results.NotFound();
});

app.MapGet("/api/tarefas/buscar/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(tarefa);
});


app.MapGet("/api/tarefas/deletar/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa is null)
    {
        return Results.NotFound();
    }
    ctx.Tarefas.Remove(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});


app.MapPut("/api/tarefas/alterar/{id}", ([FromRoute] int id, 
[FromBody] Tarefa tarefaAlterada, [FromServices] AppDataContext ctx) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa is null)
    {
        return Results.NotFound();
    }
    tarefa.TarefaName = tarefaAlterada.TarefaName;
    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});



app.Run();

