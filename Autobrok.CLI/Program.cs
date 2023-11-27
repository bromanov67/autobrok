using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;


Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;
var openLoopsRepository = new OpenLoopsRepository();

{
    Console.WriteLine("Что вас беспокоит сейчас?");

    string? note;
    do
    {
        note = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(note));

    openLoopsRepository.Add(new OpenLoop
    {
        Note = note,
        CreateDate = DateTimeOffset.UtcNow
    });
}

{

    var openLoops = openLoopsRepository.Get();
    var group = openLoops.GroupBy(x => x.CreateDate);
    foreach (var groupOfOpenLoops in group)
    {
        Console.WriteLine($"Ваши заботы: {groupOfOpenLoops.Key:dd.MM.yyyy}");

        foreach (var openLoop in groupOfOpenLoops.ToArray())
        {
            Console.WriteLine(openLoop.Note);
        }
        
    }
}
