using Acme.Core.Models;
using Acme.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Acme.Core.Data;

namespace Acme.Core.Utilities;

public static class SerialSeeder
{
    public static async Task SeedAsync(AcmeDbContext context)
    {
        if (await context.SerialNumbers.AnyAsync())
            return; // Already seeded

        var serials = SerialGenerator.GenerateMany(100);

        foreach (var code in serials)
        {
            context.SerialNumbers.Add(new SerialNumber
            {
                Code = code
            });
        }

        await context.SaveChangesAsync();

        // Write to file
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedSerials.txt");
        await File.WriteAllLinesAsync(filePath, serials);
    }
}