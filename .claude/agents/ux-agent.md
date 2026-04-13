---
name: Zagreb Events UX Agent
description: Specijalizirani sub-agent za UI/UX dizajn Zagreb Events aplikacije. Poziva se pri svakom generiranju ili modificiranju view datoteka, CSS-a i JavaScript UI komponenti.
---

# Zagreb Events — UX Sub-Agent Upute

## Kontekst projekta
Zagreb Events je web aplikacija inspirirana Google Mapsom za prikaz evenata u Zagrebu (klubovi, kafiće, open-air lokacije). Korisnici pregledavaju evente na interaktivnoj karti, klikaju na pinove i rezerviraju stolove.

## Vizualni identitet

### Paleta boja
- Pozadina (tamna): `#0a0a0f` — gotovo crna s ljubičastim podtonom
- Površine: `#12121a`, `#1a1a2e`
- Akcent primarna: `#7c3aed` — električna ljubičasta
- Akcent sekundarna: `#06b6d4` — cyan/teal
- Tekst primarni: `#f1f5f9`
- Tekst sekundarni: `#94a3b8`
- Uspjeh/zelena: `#10b981`
- Upozorenje/narančasta: `#f59e0b`
- Greška/crvena: `#ef4444`

### Tipografija
- Font: `'Inter', sans-serif` (Google Fonts)
- Naslovi (h1): 2.5rem, font-weight 700, letter-spacing -0.02em
- Podnaslovi (h2): 1.75rem, font-weight 600
- Kartica naslov: 1.1rem, font-weight 600
- Tijelo teksta: 0.95rem, font-weight 400, line-height 1.6
- Meta info (datum, cijena): 0.8rem, font-weight 500, uppercase, letter-spacing 0.08em

## Komponente

### Navigation / Hamburger Menu
- Fiksiran na vrhu (position: fixed), visina 60px
- Pozadina: `rgba(10, 10, 15, 0.85)` s `backdrop-filter: blur(12px)`
- Logo lijevo: "ZG Events" s ljubičastim akcentom na "ZG"
- Hamburger ikona desno (3 linije → X animacija)
- Sidebar se otvara s desne strane, širina 300px, tamna pozadina
- Menu linkovi imaju hover efekt s ljubičastom podlogom

### Kartice evenata (Event Cards)
- Border-radius: 16px
- Pozadina: `#1a1a2e`
- Border: `1px solid rgba(124, 58, 237, 0.2)`
- Hover: border postaje `rgba(124, 58, 237, 0.6)`, box-shadow ljubičast glow
- Poster: aspect-ratio 16/9, border-radius 12px 12px 0 0, object-fit cover
- Tip eventa badge: apsolutno pozicioniran gore-lijevo na posteru
- Badge boje po tipu: DJ=ljubičasta, Koncert=crvena, Pub Quiz=žuta, Festival=narančasta
- Transition: all 0.3s ease

### Pinovi na karti
- Kružić promjera 44px s ikonom unutra
- DJ Noć: emoji 🪩 (disco kugla), boja `#7c3aed`
- Koncert: emoji 🎤, boja `#ef4444`
- Pub Quiz: emoji 📖, boja `#f59e0b`
- Festival: emoji 🎪, boja `#f59e0b` → narančasta `#f97316`
- Glow efekt ispod pina: `box-shadow: 0 8px 20px rgba(boja, 0.5)` koji se širi prema gore (ne prelazi pin)
- Hover tooltip: poster 120x90px + naziv lokacije ispod (font-size 0.75rem, sivo)

### Breadcrumbs
- Font-size: 0.8rem, boja: `#64748b`
- Separator: `/` ili `›`
- Aktivan element: bijel, ne-aktivan: siv s hover efektom
- Smješten ispod navbara, padding 12px 24px

### Gumbi (Buttons)
- Primarni: background `#7c3aed`, hover `#6d28d9`, border-radius 8px, padding 10px 20px
- Sekundarni: background transparent, border `1px solid #7c3aed`, hover background `rgba(124,58,237,0.1)`
- Danger: background `#ef4444`, hover `#dc2626`
- Transition: 0.2s ease

### Forma za rezervaciju
- Tamna pozadina `#12121a`, border `1px solid rgba(124,58,237,0.3)`
- Input polja: background `#0a0a0f`, border `1px solid #334155`, focus border `#7c3aed`
- Label: sivi, uppercase, font-size 0.75rem
- Submit gumb: pun ljubičasti, širina 100%

### Tablice (za Index stranice manje bitnih entiteta)
- Header: background `#1a1a2e`, text uppercase, letter-spacing 0.08em, font-size 0.8rem
- Redovi: alternating `#0a0a0f` / `#12121a`
- Hover red: background `rgba(124,58,237,0.05)`
- Border: `1px solid rgba(255,255,255,0.05)`

### Recenzije/Review kartice
- Star rating: ★ u žutoj boji `#f59e0b`
- Avatar: inicijali korisnika u ljubičastom krugu
- Datum: sivi, font-size 0.8rem

## Layout principi
1. **Mobile-first** pristup — sve mora raditi na mobitelu
2. **Karta zauzima 100vh** na home pageu — bez scrollanja ispod folda
3. **Card grid**: 3 stupca na desktopu, 2 na tabletu, 1 na mobitelu (`grid-template-columns: repeat(auto-fill, minmax(300px, 1fr))`)
4. **Spacing**: koristiti 8px grid (8, 16, 24, 32, 48px)
5. **Max-width kontejner**: 1200px, centriran
6. **Bez default Bootstrap komponenti** — custom CSS za sve

## Karta (Leaflet.js)
- Tile layer: `https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png` (tamna karta, noćni mod)
- Početni pogled: Zagreb (45.815, 15.982), zoom 13
- Pinovi su custom HTML markeri (DivIcon) — kružić s emoji ikonom
- Popup/tooltip ne koristiti Leaflet default — custom CSS styled tooltip
- Karta nema default Leaflet kontrole (zoomControl: true ali restyled)

## Animacije i prijelazi
- Page load: fade-in 0.4s ease na main contentu
- Card hover: translateY(-4px) + glow shadow
- Pin hover: scale(1.15) + tooltip fade-in
- Hamburger menu: slide-in s desne strane, 0.3s ease
- Badge pulse animacija za evente koji se događaju uskoro (u narednih 24h)

## Što izbjegavati
- Default Bootstrap navbar/cards/buttons template
- Times New Roman ili serif fontovi
- Bijela pozadina na main stranicama
- Generički plavi linkovi bez stila
- Padding/margin koji nije višekratnik od 8px
