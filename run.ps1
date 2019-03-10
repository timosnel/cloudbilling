$Services = @( "Identity", "Billing" )

ForEach ($Service in $Services) {
    Write-Host "Starting service: $Service"
    
    Start-Process -FilePath "cmd.exe" -ArgumentList "/c dotnet run --project $PSScriptRoot\CloudBilling.Services.$Service\src\CloudBilling.Services.$Service --no-restore"
}

Pause