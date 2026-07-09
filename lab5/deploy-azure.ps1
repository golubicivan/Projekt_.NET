# ============================================================
# Deploy GdjeCemo na Azure (App Service + Azure SQL)
# Live: https://gdjecemo-app.azurewebsites.net
#
# Jednokratno napravljeno (Azure for Students pretplata):
#   az login
#   az group create -n gdjecemo-rg -l westeurope
#   az sql server create -n gdjecemo-sql-9f27 -g gdjecemo-rg -l polandcentral -u zeadmin -p <SQL_LOZINKA>
#   az sql db create -g gdjecemo-rg -s gdjecemo-sql-9f27 -n ZagrebEventsDb --edition Basic --capacity 5 --max-size 2GB
#   az sql server firewall-rule create -g gdjecemo-rg -s gdjecemo-sql-9f27 -n AllowAzureServices `
#       --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
#   az appservice plan create -n gdjecemo-plan -g gdjecemo-rg -l polandcentral --sku F1 --is-linux
#   az webapp create -n gdjecemo-app -g gdjecemo-rg -p gdjecemo-plan --runtime "DOTNETCORE:8.0"
#   az webapp config connection-string set -g gdjecemo-rg -n gdjecemo-app -t SQLAzure `
#       --settings "ZagrebEventsDbContext=Server=tcp:gdjecemo-sql-9f27.database.windows.net,1433;Initial Catalog=ZagrebEventsDb;User ID=zeadmin;Password=<SQL_LOZINKA>;MultipleActiveResultSets=True;Encrypt=True;Connection Timeout=60;"
#   # Tajne (SMTP, Google OAuth, Anthropic) kopirane iz user-secrets u app settings
#   # s dvostrukim underscoreom umjesto dvotocke: Anthropic__ApiKey, Smtp__Host, ...
#
# Baza se sama migrira i seeda pri prvom startu (Program.cs: db.Database.Migrate()).
#
# Ova skripta radi REDEPLOY nakon promjena koda:  .\deploy-azure.ps1
# ============================================================
$ErrorActionPreference = "Stop"
$root = $PSScriptRoot

Write-Host "== Publish (Release) ==" -ForegroundColor Cyan
dotnet publish "$root\ZagrebEvents.Web\ZagrebEvents.Web.csproj" -c Release -o "$root\publish_output" -nologo -v q

Write-Host "== Zip (tar - forward slashevi, Compress-Archive NE radi za Linux!) ==" -ForegroundColor Cyan
$zip = "$env:TEMP\gdjecemo-deploy.zip"
if (Test-Path $zip) { Remove-Item $zip -Force }
tar.exe -a -c -f $zip -C "$root\publish_output" .

Write-Host "== Deploy na gdjecemo-app ==" -ForegroundColor Cyan
az webapp deploy -g gdjecemo-rg -n gdjecemo-app --src-path $zip --type zip --clean true

Write-Host "Gotovo: https://gdjecemo-app.azurewebsites.net" -ForegroundColor Green
