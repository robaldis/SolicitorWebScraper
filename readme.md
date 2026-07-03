# Solicitor Scraper

This app scrapes https://www.solicitors.com for conveyancer solicitors in certain
regions

To run the app open two terminals and 
run the backend and front end code with 

```bash

# Terminal 1
cd client
npm run dev

# Terminal 2
cd src/InfoTrack.Api
dotnet run

```


# Front end

Front end is a vue app that talks to the backend api, doing minimal validation 
on the inputs

# The backend

C# api with a few endpoints

Health, check the system is up

Locations, get the valid set of locations we can handle, this is configured with
the appsettings.json configuration and can be extended

Conveyancer, listing query that scrapes `https://www.solicitors.com/`, can input 
a list of locations to do a full search

 
