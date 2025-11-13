using System;

Console.Write("Wat is je naam? ");
string naam = Console.ReadLine();
Console.WriteLine($"Hallo {naam}!");

Console.Write("Hoe oud ben je? ");
int leeftijd = Convert.ToInt32(Console.ReadLine());
int volgendJaarLeeftijd = leeftijd + 1;
Console.WriteLine($"Volgend jaar ben je {volgendJaarLeeftijd} jaar oud.");

Console.Write("Wat is je favoriete programmeertaal? ");
string taal = Console.ReadLine();
Console.WriteLine($"{taal} is een geweldige keuze!");
Console.WriteLine("Bedankt voor het delen van je informatie!");
Console.WriteLine("Druk op een toets om af te sluiten...");

Console.ReadKey();
Console.WriteLine(
    """
    Dit is een meerregelige string in C# 12.
    Je kunt hier meerdere regels tekst plaatsen
    zonder speciale tekens te gebruiken.
    """);
Console.WriteLine("Einde van het programma.");