#!/bin/bash

export ASPNETCORE_ENVIRONMENT="Development"

dotnet ef migrations add CineOrgContext_$1 --startup-project ../Toto.CineOrg.WebApi 