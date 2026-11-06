using SQLite;

namespace AppSquiliteS5.Models;

[Table("personas")]
public class Persona
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, MaxLength(60)]
    public string Nombre { get; set; } = string.Empty;
}
