#!/bin/bash
dotnet new sln -n ChrisSimonAu.UniversityApi
dotnet new webapi -o ChrisSimonAu.UniversityApi
dotnet new xunit -o ChrisSimonAu.UniversityApi.Tests
dotnet sln add ChrisSimonAu.UniversityApi
dotnet sln add ChrisSimonAu.UniversityApi.Tests
dotnet dev-certs https
sudo -E dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM
sudo update-ca-certificates
