using System;

namespace API.models;

public class Tarefa
{
    public int TarefaId { get; set;}
    public string? TarefaName { get; set;}
    public DateTime CriadoEm { get; set;} = DateTime.Now;

}