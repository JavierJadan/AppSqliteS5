using AppSquiliteS5.Models;
using SQLite;

namespace AppSquiliteS5.Repositories;

public sealed class PersonRepository
{
    private readonly SQLiteAsyncConnection _database;
    private readonly Task<CreateTableResult> _initialization;

    public PersonRepository(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
        _initialization = _database.CreateTableAsync<Persona>();
    }

    public async Task<List<Persona>> GetAllAsync()
    {
        await _initialization;
        return await _database.Table<Persona>().OrderBy(persona => persona.Nombre).ToListAsync();
    }

    public async Task<int> AddAsync(string nombre)
    {
        await _initialization;
        var nombreLimpio = nombre.Trim();
        if (string.IsNullOrWhiteSpace(nombreLimpio))
            throw new ArgumentException("Ingresa el nombre de la persona.");
        if (nombreLimpio.Length > 60)
            throw new ArgumentException("El nombre no puede superar 60 caracteres.");
        return await _database.InsertAsync(new Persona { Nombre = nombreLimpio });
    }

    public async Task<int> DeleteAsync(Persona persona)
    {
        await _initialization;
        return await _database.DeleteAsync(persona);
    }
}
