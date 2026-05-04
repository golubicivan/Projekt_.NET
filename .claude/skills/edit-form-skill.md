---
name: Edit Form Skill
description: Koristi se kad trebamo napraviti Create ili Edit formu za uređivanje entiteta. Aktivira se na zahtjeve poput "napravi formu za dodavanje X", "edit stranica za Y", "create/update X".
---

# Edit Form Skill — Zagreb Events

Skill za kreiranje Create/Edit formi koje šalju podatke serveru kroz HTTP POST.

## Predložak Create akcije u controlleru

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

public class XxxController : Controller
{
    private readonly ZagrebEventsDbContext _db;
    public XxxController(ZagrebEventsDbContext db) => _db = db;

    // GET: prikaz prazne forme
    public IActionResult Create()
    {
        // Ako forma treba dropdown za relacije, učitaj ih i pošalji kroz ViewBag
        ViewBag.RelatedItems = _db.RelatedTable.OrderBy(r => r.Name).ToList();
        return View(new Xxx());
    }

    // POST: prima podatke forme i sprema u bazu
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Xxx model)
    {
        if (!ModelState.IsValid)
        {
            // Vrati formu s greškama validacije
            ViewBag.RelatedItems = _db.RelatedTable.OrderBy(r => r.Name).ToList();
            return View(model);
        }

        _db.Xxxs.Add(model);
        _db.SaveChanges();
        TempData["Success"] = "Uspješno spremljeno!";
        return RedirectToAction("Details", new { id = model.Id });
    }
}
```

## Predložak Edit akcije

```csharp
// GET: učitaj postojeći zapis i prikaži formu
public IActionResult Edit(int id)
{
    var item = _db.Xxxs
        .Include(x => x.Related)
        .FirstOrDefault(x => x.Id == id);
    if (item == null) return NotFound();
    return View(item);
}

// POST: ažuriraj postojeći zapis
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Xxx model)
{
    var existing = _db.Xxxs.Find(id);
    if (existing == null) return NotFound();

    existing.Name = model.Name;
    existing.Description = model.Description;
    // ... ostala polja

    _db.SaveChanges();
    TempData["Success"] = "Promjene spremljene!";
    return RedirectToAction("Details", new { id });
}
```

## Predložak forme (Razor + Tag Helpers)

```cshtml
@model Xxx
@{
    ViewData["Title"] = Model.Id == 0 ? "Novi unos" : "Uredi";
    var related = ViewBag.RelatedItems as List<Related> ?? new List<Related>();
}

<form asp-action="Create" asp-controller="Xxx" method="post" class="ze-form">
    @Html.AntiForgeryToken()

    @* TEKST: asp-for automatski generira name="Name" *@
    <div class="form-group">
        <label asp-for="Name" class="ze-label">Naziv</label>
        <input asp-for="Name" class="ze-input" required />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    @* TEKST AREA *@
    <div class="form-group">
        <label asp-for="Description" class="ze-label">Opis</label>
        <textarea asp-for="Description" class="ze-input" rows="4"></textarea>
    </div>

    @* DROPDOWN za FK relaciju *@
    <div class="form-group">
        <label asp-for="RelatedId" class="ze-label">Related entitet</label>
        <select asp-for="RelatedId" class="ze-input" required>
            <option value="">Odaberi...</option>
            @foreach (var r in related)
            {
                <option value="@r.Id">@r.Name</option>
            }
        </select>
    </div>

    @* DATETIME *@
    <input asp-for="StartTime" type="datetime-local" class="ze-input" />

    @* NUMBER *@
    <input asp-for="Price" type="number" step="0.50" min="0" class="ze-input" />

    @* HIDDEN *@
    <input type="hidden" asp-for="Id" />

    <button type="submit" class="btn-primary-custom">Spremi</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

## Tag helpers — što oni rade

| Tag helper | Što generira |
|------------|-------------|
| `asp-for="Name"` | `name="Name" id="Name" value="..."` automatski |
| `asp-action="Create"` | `action="/Xxx/Create"` |
| `asp-controller="Xxx"` | dio action URL-a |
| `asp-route-id="@id"` | dodaje `/{id}` u URL |
| `asp-validation-for="Name"` | prikazuje validation poruku za polje |

## Validacija

Anotacije na model klasi:
```csharp
[Required, MaxLength(150)]
public string Name { get; set; } = "";

[Range(1, 5)]
public int Rating { get; set; }

[EmailAddress]
public string Email { get; set; } = "";
```

Provjera u controlleru:
```csharp
if (!ModelState.IsValid) return View(model);
```

## Sigurnost

- **Antiforgery token**: `[ValidateAntiForgeryToken]` na akciji + `@Html.AntiForgeryToken()` u formi (ili automatski s `asp-action`)
- **Mass assignment**: za production aplikacije koristi DTO klase ili `[Bind]` atribut da ograničiš koja polja se mogu set-ati

## Post-Redirect-Get pattern

Nakon uspješnog POST-a, **uvijek** redirect (ne return View) — sprječava duplikate ako korisnik refresha stranicu:
```csharp
return RedirectToAction("Details", new { id = model.Id });
```

## Flash poruke

```csharp
TempData["Success"] = "Spremljeno!";
return RedirectToAction(...);
```

U layoutu ili views-u:
```cshtml
@if (TempData["Success"] != null)
{
    <div class="ze-alert-success">@TempData["Success"]</div>
}
```
