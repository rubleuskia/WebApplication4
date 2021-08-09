[CmdletBinding()]
Param(
    [Parameter(Position=0,Mandatory=$true,ValueFromRemainingArguments=$true)]
    [string[]]$BuildArguments
)

Set-Location -Path "./WebApplication4"

dotnet ef migrations $BuildArguments --project ../DatabaseAccess

Set-Location -Path "./../"