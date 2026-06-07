# Google OAuth — postavljanje (Lab 5)

Kod za Google prijavu je već implementiran:
- `Program.cs` — `AddGoogle(...)` se registrira ako postoje ClientId i ClientSecret
- `AccountController` — `ExternalLogin`, `ExternalLoginCallback`, `ExternalLoginConfirmation`
- `Views/Account/Login.cshtml` — gumb "Prijavi se s Google"
- `Views/Account/ExternalLoginConfirmation.cshtml` — dovršetak registracije (OIB/JMBG)

Da bi Google login **stvarno radio**, trebaš vlastite Google credentials.

## 1. Kreiraj OAuth aplikaciju na Google Cloud Console

1. Idi na https://console.cloud.google.com/
2. Kreiraj projekt (ili odaberi postojeći)
3. **APIs & Services → OAuth consent screen** → External → popuni osnovne podatke
4. **APIs & Services → Credentials → Create Credentials → OAuth client ID**
5. Application type: **Web application**
6. **Authorized redirect URIs** dodaj:
   ```
   https://localhost:7xxx/signin-google
   ```
   (zamijeni 7xxx s HTTPS portom iz `Properties/launchSettings.json`, profil `https`)
7. Spremi → dobiješ **Client ID** i **Client Secret**

## 2. Spremi credentials u user-secrets (NE u kod!)

Iz `lab5/ZagrebEvents.Web` foldera:

```powershell
dotnet user-secrets set "Authentication:Google:ClientId" "TVOJ_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "TVOJ_CLIENT_SECRET"
```

User-secrets se spremaju izvan repozitorija (sigurno), pa se tajne ne commitaju.

## 3. Pokreni preko HTTPS

Google zahtijeva HTTPS:

```powershell
dotnet run --launch-profile https
```

## 4. Testiraj

1. Otvori `/prijava`
2. Klikni "Prijavi se s Google"
3. Prijavi se Google računom
4. Prvi put: forma traži OIB i JMBG da dovrši registraciju
5. Kreira se AppUser + domenski User profil, dodjeljuje se rola Guest

## Napomena

Ako ClientId/Secret nisu postavljeni, aplikacija normalno radi (lokalna prijava),
samo Google gumb neće funkcionirati (Google provider se ne registrira).
