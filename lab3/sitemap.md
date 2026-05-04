# Sitemap — Zagreb Events

Popis svih dostupnih URL-ova u aplikaciji s controllerom, akcijom i view-om koji se koriste.

## Routing

Aplikacija koristi **mixed routing**:
1. **Default routing** (Program.cs): `{controller=Home}/{action=Index}/{id?}`
2. **Custom attribute routing**: `[Route("hrvatski-naziv")]` na pojedinim akcijama

Kad se zatraži URL koji odgovara obje rute, custom ima prioritet.

---

## Home

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/` | HomeController | `Index()` | `Views/Home/Index.cshtml` | Početna — interaktivna Leaflet karta s pinovima venuea |

---

## Event

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/eventi` ⭐ | EventController | `Index()` | `Views/Event/Index.cshtml` | Custom URL — kartica grid svih evenata |
| `/Event/Index` | EventController | `Index()` | `Views/Event/Index.cshtml` | Default URL (ista akcija) |
| `/event/{id:int}` ⭐ | EventController | `Details(int id)` | `Views/Event/Details.cshtml` | Custom URL — detalji eventa |
| `/Event/Details/{id}` | EventController | `Details(int id)` | `Views/Event/Details.cshtml` | Default URL |
| `/Event/Create` | EventController | `Create()` GET | `Views/Event/Create.cshtml` | Forma za novi event |
| `/Event/Create` POST | EventController | `Create(Event)` POST | (redirect na Details) | Sprema novi event u bazu |
| `/Event/Reserve` POST | EventController | `Reserve(...)` | (redirect na Details) | Sprema novu rezervaciju |

⭐ = custom routing (koristi `[Route]` atribut)

---

## Venue

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/lokacije` ⭐ | VenueController | `Index()` | `Views/Venue/Index.cshtml` | Custom URL — kartice svih lokacija |
| `/Venue/Index` | VenueController | `Index()` | `Views/Venue/Index.cshtml` | Default URL |
| `/lokacija/{id:int}` ⭐ | VenueController | `Details(int id)` | `Views/Venue/Details.cshtml` | Custom URL — detalji lokacije |
| `/Venue/Details/{id}` | VenueController | `Details(int id)` | `Views/Venue/Details.cshtml` | Default URL |

---

## User

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/korisnici` ⭐ | UserController | `Index()` | `Views/User/Index.cshtml` | Custom URL — tablica korisnika |
| `/User/Index` | UserController | `Index()` | `Views/User/Index.cshtml` | Default URL |
| `/User/Details/{id}` | UserController | `Details(int id)` | `Views/User/Details.cshtml` | Profil korisnika |
| `/moj-profil` ⭐ | UserController | `MyProfile()` | (redirect na Details/1) | Custom URL — alias za Details ulogiranog usera |

---

## Reservation

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/rezervacije` ⭐ | ReservationController | `Index()` | `Views/Reservation/Index.cshtml` | Custom URL — tablica rezervacija |
| `/Reservation/Index` | ReservationController | `Index()` | `Views/Reservation/Index.cshtml` | Default URL |
| `/Reservation/Details/{id}` | ReservationController | `Details(int id)` | `Views/Reservation/Details.cshtml` | Detalji rezervacije |
| `/Reservation/Edit/{id}` GET | ReservationController | `Edit(int id)` | `Views/Reservation/Edit.cshtml` | Forma za uređivanje rezervacije |
| `/Reservation/Edit/{id}` POST | ReservationController | `Edit(...)` POST | (redirect na Details) | Sprema izmjene |

---

## Review

| URL | Controller | Akcija | View | Opis |
|-----|------------|--------|------|------|
| `/recenzije` ⭐ | ReviewController | `Index()` | `Views/Review/Index.cshtml` | Custom URL — kartice recenzija |
| `/Review/Index` | ReviewController | `Index()` | `Views/Review/Index.cshtml` | Default URL |
| `/Review/Details/{id}` | ReviewController | `Details(int id)` | `Views/Review/Details.cshtml` | Detalji recenzije |
| `/Review/Create` GET | ReviewController | `Create(int? eventId)` | `Views/Review/Create.cshtml` | Forma za novu recenziju |
| `/Review/Create` POST | ReviewController | `Create(Review)` POST | (redirect na Event/Details) | Sprema recenziju |

---

## Sažetak custom rute (atributno usmjeravanje)

PDF traži minimum 4 prilagođene rute. Implementirane:

| # | Custom URL | Akcija |
|---|-----------|--------|
| 1 | `/eventi` | `EventController.Index` |
| 2 | `/event/{id:int}` | `EventController.Details` |
| 3 | `/lokacije` | `VenueController.Index` |
| 4 | `/lokacija/{id:int}` | `VenueController.Details` |
| 5 | `/korisnici` | `UserController.Index` |
| 6 | `/moj-profil` | `UserController.MyProfile` |
| 7 | `/rezervacije` | `ReservationController.Index` |
| 8 | `/recenzije` | `ReviewController.Index` |

**Ukupno: 8 custom ruta** (PDF traži minimum 4).

---

## Shared views

| View | Opis |
|------|------|
| `Views/Shared/_Layout.cshtml` | Master template — navbar, sidebar, breadcrumbs wrapper, footer |
| `Views/Shared/Error.cshtml` | Error stranica (HTTP 500/404) |
| `Views/Shared/_ValidationScriptsPartial.cshtml` | Klijentska validacija (jQuery validate) |
| `Views/_ViewStart.cshtml` | Postavlja `Layout = "_Layout"` za sve stranice |
| `Views/_ViewImports.cshtml` | Globalni `@using` za sve viewove |

---

## Tijek autentikacije/autorizacije

**Trenutno**: nije implementirana — `User.Id = 1` je hardkodiran kao "ulogirani" korisnik (Ivan Golubić). Sva korisnička akcija (rezervacija, recenzija) sprema se pod tim ID-jem.

**Future**: ASP.NET Identity ili custom auth middleware.

---

## HTTP metode

| Metoda | Korištenje |
|--------|-----------|
| **GET** | Index, Details, Create (GET — prikaz forme), Edit (GET) — čitanje podataka |
| **POST** | Create (POST), Edit (POST), Reserve — slanje forme, izmjena baze |

Sve POST akcije koriste `[ValidateAntiForgeryToken]` za zaštitu od CSRF napada.
