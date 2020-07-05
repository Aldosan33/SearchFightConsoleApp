# SearchFightConsoleApp

Repository for an application that queries search engines and compare results

This is an starter project using the framework net core 3.1 and C# as a main language

### Execution Example

```
C:\SearchFight.exe .net java
```

```
.net: Google: 13200000 MSN Search: 15600000
java: Google: 405000 MSN Search: 20700000
Google winner: .net
MSN Search winner: java
Total winner: .net
```

## Pre Requisites

Before running, you must configure the following Keys in appconfig.json

```
{
  "AppSettings": {
    "GoogleAPIKey": "YOUR_GOOGLE_APY_KEY",
    "GoogleCEKey": "YOUR_GOOGLE_CSE_KEY",
    "BingKey": "YOUR_BING_KEY"
  }
}
```

## Setup

After cloning this repo, do the following:

Run `dotnet build`. This will make sure that all of the dependencies are installed.

#### `dotnet run`

Runs the solution in the development mode.

##### comand example

```
dotnet run .net java
```

#### `dotnet build`

Prepare the solution for production environments.

#### `dotnet test`

Run all the tests available.
