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
