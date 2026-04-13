# AI Agent Log — ZagrebEvents (.NET Lab)

Ovaj log bilježi sve interakcije s AI agentom (Claude) tijekom razvoja projekta.

---

## Sesija 1 — 13.04.2026.

### Interakcija 1
**Korisnik:** "kako ocjenujes projekt"
**Agent:** Pregledao sve datoteke projekta (Program.cs, sve klase, enume). Dao detaljnu ocjenu:
- Snage: dobar domain model, computed properties, 15 LINQ upita raznolike složenosti, use of enums
- Nedostaci: nema nullable anotacija, Program.cs prevelika, nema null provjera ni validacije
- Ocjena: ~8/10 za lab zadatak

---

### Interakcija 2
**Korisnik:** Priložio PDF zadatka (Lab-1 (1).pdf) i pitao je li zadatak ispravno ispunjen
**Agent:** Pročitao PDF, napravio detaljnu usporedbu uvjeta i implementacije:
- Objektni model: ISPUNJENO (7 klasa, 4 kompleksne, 5 enumova, DateTime, 1-N i N-N relacije)
- Punjenje podataka: ISPUNJENO (3 glavna objekta s razgranatim podacima)
- LINQ upiti: ISPUNJENO (15 smislenih upita)
- Upozorenja: folder `lab1` umjesto `lab-1`, provjera roka i javnosti GitHub repozitorija

---

### Interakcija 3
**Korisnik:** "mozes li voditi od sad pa na dalje log koristenja ai agenta"
**Agent:** Kreirao ovu log datoteku (`lab1/ai-agent-log.md`) za praćenje svih budućih interakcija.

---

## Sesija 2 — 13.04.2026. (Lab 2 — ASP.NET MVC)

### Interakcija 4
**Korisnik:** Priložio PDF zadatka (Lab_2_HTMLBinding.pdf) i zatražio nadogradnju Lab 1 projekta u ASP.NET MVC web aplikaciju
**Agent:** Pročitao PDF sa svim zahtjevima. Postavio pitanja o detaljima implementacije (entity stranice, dizajn, hamburger menu, karta).

---

### Interakcija 5
**Korisnik:** Odgovorio na sva pitanja — jedan projekt `lab2/ZagrebEvents.Web`, Google Maps-like sučelje s Leaflet.js + OpenStreetMap, tamna tema, pin ikonice po tipu eventa (🪩🎤📖🎪), hamburger sidebar s listom evenata i korisničkim profilom, kartice za prikaz, sve entity stranice (Index + Details za Event, Venue, User, Reservation, Review), hrvatska UI.
**Agent:** Kreirao UX sub-agent instruction file (`.claude/agents/ux-agent.md`) s kompletnim design systemom.

---

### Interakcija 6
**Korisnik:** (nastavak postavljanja zahtjeva — detalji po pitanjima)
**Agent:** Kreirao kompletan ASP.NET MVC projekt:
- `lab2/ZagrebEvents.Web/` — cijeli projekt struktura
- Model klase s computed properties (TypeIcon, TypeColor, Stars, StatusLabel, Initials)
- MockData static class s 3 venue-a, 9 eventa, 6 korisnika, 5 rezervacija, 5 recenzija
- Mock repositories (VenueMockRepository, EventMockRepository, UserMockRepository, ReservationMockRepository, ReviewMockRepository)
- Controllers: Home, Event, Venue, User, Reservation, Review
- `Program.cs` s DI registracijom svih repozitorija kao Singleton

---

### Interakcija 7
**Korisnik:** (automatski — build i view kreacija)
**Agent (UX sub-agent):** Kreirao `_Layout.cshtml` s navbar, hamburger sidebar, breadcrumbs, footer i `wwwroot/css/site.css` s kompletnim dark theme design systemom (CSS varijable, Inter font, kartice, pinovi).
**Agent (glavni):** Kreirao sve Razor viewove:
- `Home/Index.cshtml` — Leaflet.js interaktivna karta s custom pin ikonama, hover tooltipom, glow efektom, legendom
- `Event/Index.cshtml` + `Event/Details.cshtml` — kartice + full detalji (hero, statistike, rezervacijski form, recenzije, sidebar)
- `Venue/Index.cshtml` + `Venue/Details.cshtml`
- `User/Index.cshtml` + `User/Details.cshtml`
- `Reservation/Index.cshtml` + `Reservation/Details.cshtml`
- `Review/Index.cshtml` + `Review/Details.cshtml`
- Build rezultat: **uspješno**, 25 CS8618 upozorenja (nullable), 0 grešaka

---
