# Semantic DB Model — Zagreb Events

Sažeti popis svih klasa/tablica, glavnih svojstava i veza među njima.

## Tablice (entiteti)

### 1. Users
Korisnici aplikacije (gosti, vlasnici venuea, administratori).

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| FirstName | string | [Required, MaxLength(60)] |
| LastName | string | [Required, MaxLength(60)] |
| DateOfBirth | DateTime | — |
| Email | string | [Required, MaxLength(120)] |
| PhoneNumber | string | [MaxLength(30)] |
| Role | UserRole (enum) | Guest / Owner / Admin |
| RegisteredAt | DateTime | — |

**Computed (NotMapped):** FullName, Age, IsAdult, Initials

---

### 2. Venues
Lokacije gdje se održavaju eventi (klubovi, kafići, open-air pozornice).

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| Name | string | [Required, MaxLength(100)] |
| Address | string | [MaxLength(200)] |
| Latitude | double | — (za Leaflet kartu) |
| Longitude | double | — (za Leaflet kartu) |
| Capacity | int | — |
| WorkingHours | string | [MaxLength(50)] |
| ContactPhone | string | [MaxLength(30)] |
| Description | string | [MaxLength(2000)] |
| Type | VenueType (enum) | Club / Bar / Cafe / OpenAir |
| ImageUrl | string | [MaxLength(500)] |

---

### 3. Events
Eventi koji se održavaju u venuima.

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| Name | string | [Required, MaxLength(150)] |
| Description | string | [MaxLength(2000)] |
| StartTime | DateTime | — |
| EndTime | DateTime | — |
| Type | EventType (enum) | DJNight / Concert / PubQuiz / Festival |
| EntryPrice | decimal | [Column(decimal(10,2))] |
| PosterUrl | string | [MaxLength(500)] |
| AgeLimit | int | — |
| **VenueId** | int | [ForeignKey(Venue)] |

**Computed:** AverageRating, IsUpcoming, IsActive, Duration, TypeIcon, TypeColor, TypeLabel

---

### 4. Tables
Stolovi u venuima koji se mogu rezervirati.

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| TableNumber | int | — |
| SeatCount | int | — |
| Zone | TableZone (enum) | Regular / VIP |
| **VenueId** | int | [ForeignKey(Venue)] |

---

### 5. Reservations
Rezervacije stolova za evente.

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| CreatedAt | DateTime | — |
| NumberOfGuests | int | — |
| Status | ReservationStatus | Pending / Confirmed / Cancelled |
| Note | string | [MaxLength(500)] |
| MinimumSpending | decimal | [Column(decimal(10,2))] |
| **UserId** | int | [ForeignKey(User)] |
| **TableId** | int | [ForeignKey(Table)] |
| **EventId** | int | [ForeignKey(Event)] |

**Computed:** IsConfirmed, StatusLabel, StatusColor

---

### 6. Reviews
Recenzije evenata.

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| Rating | int | [Range(1, 5)] |
| Comment | string | [Required, MaxLength(1000)] |
| CreatedAt | DateTime | — |
| **UserId** | int | [ForeignKey(User)] |
| **EventId** | int | [ForeignKey(Event)] |

**Computed:** Stars (★ string)

---

### 7. PriceListItems
Stavke cjenika za pojedini venue.

| Property | Tip | Anotacije |
|----------|-----|-----------|
| Id | int | [Key] |
| ItemName | string | [Required, MaxLength(100)] |
| Price | decimal | [Column(decimal(10,2))] |
| Category | string | [MaxLength(50)] |
| **VenueId** | int | [ForeignKey(Venue)] |

---

### 8. UserFavoriteVenue (join tablica)
Implicit join tablica za N-N vezu User ↔ Venue (favoriti).

| Property | Tip |
|----------|-----|
| UserId | int (FK + composite key) |
| VenueId | int (FK + composite key) |

Generirana automatski iz konfiguracije:
```csharp
modelBuilder.Entity<User>()
    .HasMany(u => u.FavoriteVenues)
    .WithMany(v => v.FavoritedByUsers)
    .UsingEntity<Dictionary<string, object>>("UserFavoriteVenue", ...)
```

---

## Veze među tablicama

### 1-N (One-to-Many)
```
Venue 1 ─── N Events           (Venue.Events)
Venue 1 ─── N Tables           (Venue.Tables)
Venue 1 ─── N PriceListItems   (Venue.PriceList)

Event 1 ─── N Reservations     (Event.Reservations)
Event 1 ─── N Reviews          (Event.Reviews)

User  1 ─── N Reservations     (User.Reservations)
User  1 ─── N Reviews          (User.Reviews)

Table 1 ─── N Reservations     (Table.Reservations)
```

### N-N (Many-to-Many)
```
User N ─── N Venue             (User.FavoriteVenues ↔ Venue.FavoritedByUsers)
                                kroz join tablicu UserFavoriteVenue
```

---

## ER dijagram (tekstualno)

```
┌──────────┐      ┌──────────┐
│   User   │◄────►│  Venue   │  (N-N favoriti)
└────┬─────┘      └────┬─────┘
     │                  │
     │ 1-N              │ 1-N (events, tables, pricelist)
     ▼                  ▼
┌──────────┐      ┌──────────┐      ┌──────────────┐
│Reviews   │      │ Events   │      │ Tables       │
│Reservs   │◄─────┤          │      │              │
└──────────┘  N-1 └──────────┘      │ PriceListItem│
                                     └──────────────┘
```

## DbContext

Klasa: `ZagrebEvents.DAL.ZagrebEventsDbContext`

DbSet propertyji:
- `DbSet<User> Users`
- `DbSet<Venue> Venues`
- `DbSet<Event> Events`
- `DbSet<Table> Tables`
- `DbSet<Reservation> Reservations`
- `DbSet<Review> Reviews`
- `DbSet<PriceListItem> PriceListItems`

(N-N tablica `UserFavoriteVenue` nema posebnu C# klasu — kreirana implicit kroz Fluent API.)

## Cascade behavior

- `Reservation.User` → **Restrict** (sprječava cycle)
- `Reservation.Table` → **Restrict**
- `Reservation.Event` → **Cascade** (briše rezervacije s eventom)
- `Review.User` → **Restrict**
- `Review.Event` → **Cascade**

## Seed podaci (HasData)

- 6 korisnika
- 3 venue-a (Club Culture, Kavana Lav, Park Stage Bundek)
- 11 stolova
- 13 stavki cjenika
- 9 evenata
- 5 rezervacija
- 5 recenzija
- 5 favorita (N-N veze)

Ukupno: ~50 entiteta seeded pri prvom run-u baze.
