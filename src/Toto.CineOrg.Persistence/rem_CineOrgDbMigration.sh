#!/bin/bash

export ASPNETCORE_ENVIRONMENT="Development"

dotnet ef migrations remove --context CineOrgContext --project Toto.CineOrg.Persistence.csproj --startup-project ../Toto.CineOrg.WebApi  $1