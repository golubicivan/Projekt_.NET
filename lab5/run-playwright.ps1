# ============================================================
# Pokrece GdjeCemo app na portu 5199 i vrti Playwright testove
# (API endpointi + 10-koracni UI scenarij).
# Koristenje:  .\run-playwright.ps1     (iz lab5 direktorija)
# ============================================================
$ErrorActionPreference = "Stop"
$root = $PSScriptRoot

Write-Host "== Build ==" -ForegroundColor Cyan
dotnet build "$root\ZagrebEvents.Web\ZagrebEvents.Web.csproj" -nologo -v q
dotnet build "$root\ZagrebEvents.PlaywrightTests\ZagrebEvents.PlaywrightTests.csproj" -nologo -v q

# Playwright browseri (jednokratno)
$pwScript = "$root\ZagrebEvents.PlaywrightTests\bin\Debug\net8.0\playwright.ps1"
Write-Host "== Playwright install (chromium) ==" -ForegroundColor Cyan
& $pwScript install chromium | Out-Null

Write-Host "== Start app na :5199 ==" -ForegroundColor Cyan
$app = Start-Process -FilePath "dotnet" `
    -ArgumentList "run --no-build --project `"$root\ZagrebEvents.Web`" --urls http://localhost:5199" `
    -PassThru -WindowStyle Hidden

try {
    # cekaj da app odgovori (max 60 s)
    $ok = $false
    for ($i = 0; $i -lt 60; $i++) {
        try {
            $r = Invoke-WebRequest "http://localhost:5199/" -UseBasicParsing -TimeoutSec 2
            if ($r.StatusCode -eq 200) { $ok = $true; break }
        } catch { Start-Sleep -Seconds 1 }
    }
    if (-not $ok) { throw "App se nije digao na :5199" }
    Write-Host "App je gore." -ForegroundColor Green

    Write-Host "== Playwright testovi ==" -ForegroundColor Cyan
    $env:ZE_BASEURL = "http://localhost:5199"
    dotnet test "$root\ZagrebEvents.PlaywrightTests" --no-build -nologo
    $code = $LASTEXITCODE
}
finally {
    Write-Host "== Stop app ==" -ForegroundColor Cyan
    if ($app -and -not $app.HasExited) { Stop-Process -Id $app.Id -Force }
    # dotnet run spawn-a i dijete (ZagrebEvents.Web.exe) - pocisti
    Get-Process ZagrebEvents.Web -ErrorAction SilentlyContinue |
        Where-Object { $_.Path -like "$root*" } | Stop-Process -Force
}
exit $code
