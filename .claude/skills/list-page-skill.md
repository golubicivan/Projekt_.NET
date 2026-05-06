---
name: List Page Skill
description: Koristi se kad trebamo napraviti novu list (Index) stranicu za prikaz kolekcije entiteta iz baze. Aktivira se na zahtjeve poput "napravi listu X", "kreiraj Index stranicu za Y", "prikaži tablicu/kartice svih...".
---

# List Page Skill — Zagreb Events

Skill za kreiranje list/Index stranica koje prikazuju kolekciju entiteta iz baze.

## Predložak controllera

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class XxxController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public XxxController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: hrvatski URL za korisnike
        [Route("xxx-hrvatski")]
        public IActionResult Index()
        {
            var items = _db.Xxxs
                .Include(x => x.RelatedEntity)   // eager load relacija
                .OrderBy(x => x.Name)
                .ToList();
            return View(items);
        }
    }
}
```

## Predložak view-a (Razor)

Datoteka: `Views/Xxx/Index.cshtml`

```cshtml
@model List<Xxx>
@{
    ViewData["Title"] = "Lista Xxx";
}

@section Breadcrumbs {
<nav class="ze-breadcrumb">
    <a href="/">Karta</a> › <span>Xxx</span>
</nav>
}

<div class="ze-page-header">
    <h1>Naslov stranice</h1>
    <p class="text-muted-ze">@Model.Count zapisa</p>
</div>

<!-- Kartice (za vizualne entitete: eventi, venue) -->
<div class="card-grid">
    @foreach (var x in Model)
    {
        <a href="/xxx/@x.Id" class="card-link">
            <div class="ze-card">
                <h3>@x.Name</h3>
                <p>@x.Description</p>
            </div>
        </a>
    }
</div>

<!-- Tablica (za podatkovne entitete: korisnici, rezervacije) -->
<table class="ze-table">
    <thead>
        <tr><th>#</th><th>Naziv</th><th></th></tr>
    </thead>
    <tbody>
        @foreach (var x in Model)
        {
            <tr>
                <td>@x.Id</td>
                <td>@x.Name</td>
                <td><a href="/Xxx/Details/@x.Id" class="btn-secondary-custom">Detalji</a></td>
            </tr>
        }
    </tbody>
</table>
```

## Pravila

1. **Model na vrhu**: `@model List<Xxx>` — kolekcija
2. **Breadcrumbs section**: koristi `@section Breadcrumbs { ... }` — automatski se ubaci u layout
3. **Link na detalje**: koristi `/xxx/@x.Id` ili `/Xxx/Details/@x.Id`
4. **Sortiranje u controlleru** (`.OrderBy()`), ne u viewu
5. **Eager loading** za sve relacije koje se prikazuju (`Include()`)

## Kartice vs tablica — kada što

- **Kartice** (`grid-template-columns: repeat(auto-fill, minmax(300px, 1fr))`): vizualni entiteti s posterima, slikama (Event, Venue, Review)
- **Tablica** (`<table class="ze-table">`): podatkovni entiteti s puno polja (User, Reservation)

## Custom URL ruta

Dodati `[Route("hrvatski-naziv")]` iznad akcije za prilagođenu rutu:
- `/eventi` → `EventController.Index()`
- `/lokacije` → `VenueController.Index()`
- `/rezervacije` → `ReservationController.Index()`
- `/recenzije` → `ReviewController.Index()`
- `/korisnici` → `UserController.Index()`

Default ruta `/Xxx/Index` i dalje radi (mixed routing).

## Sidebar link

Dodati u `Views/Shared/_Layout.cshtml`:
```html
<li class="sidebar-menu-item">
    <a href="/xxx-hrvatski" class="sidebar-link">
        <span class="sidebar-icon">📦</span>
        Naslov
    </a>
</li>
```
