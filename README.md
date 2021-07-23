# WeatherFront

Full stack application for looking up weather in various cities around the world.

## Requirements

- .NET 5.0 SDK - Backend application is a ASP.Net Core Web API project.
- NodeJS (14.17+) This is for building client side application


## Running the application locally

To run the application in development mode, execute the following command in the
project directory.

```
dotnet run
```

This should pull down all packages and build both the .NET and Angular project 
in development mode. 

### Failure messages during build

Note: you may see some fail messages from the SpaServices object during Angular 
project build. I am not sure what is causing these, but these can be ignored as the 
project appears to build correctly.

## Using the application

Open a browser to http://localhost:5000 and you should be presented with the WeatherFront
client application. Enter a city name and country code to peform query.

## Configuration

To change application settings, edit the appsettings.json file. 

### KeyService section
- RequestLimit - adjusts how many requests can occur within the expiry period.
- ExpirySeconds - the time span in which to enfore the request limit.
- ValidKeys - array of strings which the client can use to authorize its API use with.

### OpenWeather section
- ApiKeys - array of API keys provided by the OpenWeather service to use. The application will
pick one of these randomly on startup.
