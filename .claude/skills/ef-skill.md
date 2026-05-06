---
name: Entity Framework Skill
description: Koristi se kad trebamo dodati izmjenu u EF model klasu, generirati novu migraciju ili dodati novi entitet u DbContext. Aktivira se na zahtjeve poput "dodaj novo polje u Event", "kreiraj novu tablicu", "dodaj migraciju".
---

# Entity Framework Skill — Zagreb Events

Pomaže pri svakoj radnji vezanoj uz Entity Framework Core u Zagreb Events projektu.

## Projekt struktura
```
lab3/
├── ZagrebEvents.Model/      ← Klase entiteta (Event, Venue, User...)
├── ZagrebEvents.DAL/        ← ZagrebEventsDbContext + Migrations/
└── ZagrebEvents.Web/        ← Controlleri, viewovi, appsettings.json
```

## Connection string
Definiran u `lab3/ZagrebEvents.Web/appsettings.json` pod ključem `ZagrebEventsDbContext`.
Server: `localhost\SQLEXPRESS`, baza: `ZagrebEvents`, Windows auth.

## Kad se dodaje novo polje u postojeći model

1. Dodaj property u model klasu (npr. `lab3/ZagrebEvents.Model/Event.cs`):
   ```csharp
   [MaxLength(50)]
   public string? Genre { get; set; }
   ```

2. Pravila za anotacije:
   - **Primarni ključ**: `[Key] public int Id`
   - **Strani ključ**: `[ForeignKey(nameof(Venue))] public int VenueId`
   - **Navigacija**: `public virtual Venue? Venue` (s `virtual` za lazy loading)
   - **1-N kolekcija**: `public virtual ICollection<Event> Events`
   - **Decimal za novac**: `[Column(TypeName = "decimal(10,2)")]`
   - **Computed property**: `[NotMapped]` da EF ne pokuša mapirati u DB

3. Generiraj migraciju iz developer terminala:
   ```powershell
   cd lab3/ZagrebEvents.DAL
   dotnet ef migrations add NazivIzmjene --startup-project ../ZagrebEvents.Web --context ZagrebEventsDbContext
   ```

4. Primijeni migraciju na bazu:
   ```powershell
   dotnet ef database update --startup-project ../ZagrebEvents.Web --context ZagrebEventsDbContext
   ```

## Kad se dodaje nova tablica/entitet

1. Kreiraj novu klasu u `ZagrebEvents.Model/`:
   ```csharp
   using System.ComponentModel.DataAnnotations;

   namespace ZagrebEvents.Model
   {
       public class Tag
       {
           [Key]
           public int Id { get; set; }

           [Required, MaxLength(50)]
           public string Name { get; set; } = "";

           public virtual ICollection<Event> Events { get; set; } = new List<Event>();
       }
   }
   ```

2. Dodaj `DbSet<T>` u `ZagrebEventsDbContext.cs`:
   ```csharp
   public DbSet<Tag> Tags { get; set; } = null!;
   ```

3. Ako postoji veza, konfiguriraj je u `OnModelCreating()` ili kroz anotacije.

4. Generiraj migraciju i update.

## Seed podaci

Dodaju se u `OnModelCreating()` pomoću `HasData()`:
```csharp
modelBuilder.Entity<Tag>().HasData(
    new Tag { Id = 1, Name = "Underground" },
    new Tag { Id = 2, Name = "Mainstream" }
);
```

**Važno**: pri seedanju navigation properties se NE postavljaju, samo FK ID-jevi.

## CRUD operacije u controllerima

```csharp
// READ jedan
var ev = _db.Events
    .Include(e => e.Venue)
    .Include(e => e.Reviews).ThenInclude(r => r.User)
    .FirstOrDefault(e => e.Id == id);

// READ više
var list = _db.Events.Where(e => e.IsUpcoming).ToList();

// CREATE
_db.Events.Add(newEvent);
_db.SaveChanges();

// UPDATE
var ev = _db.Events.Find(id);
ev.Name = "Novi naziv";
_db.SaveChanges();

// DELETE
_db.Events.Remove(ev);
_db.SaveChanges();
```

## Česte greške

- **Cascade delete cycle**: kad više FK-jeva ima cascade, EF baca grešku. Riješiti s `OnDelete(DeleteBehavior.Restrict)` u `OnModelCreating`.
- **N+1 queries**: koristi `Include()` i `ThenInclude()` za eager loading.
- **Migracija ne radi**: provjeri jel `--startup-project` točno postavljen i da Web projekt ima `Microsoft.EntityFrameworkCore.Design` paket.

## Što agent NE smije

- Ne smije ručno mijenjati datoteke u `Migrations/` folderu (to je auto-generirano).
- Ne smije postavljati `Id` ručno pri INSERT-u (ostavi EF da generira).
- Ne smije koristiti `_db.Database.EnsureCreated()` umjesto migracija.
