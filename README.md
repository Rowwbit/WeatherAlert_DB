# Weather Alert DB
Uses the National Weather Service API to document weather alerts in a SQLite DB.

Whats the point? This lets you see all the active weather alerts within the US and saves them into a Database. That way you can track or view them long term to track weather behavior long term.

This application checks the NWS API every 15 minutes and aggregates records in a SQLite Database. This is my second full featured project and this application uses more intermediate skills such as: LINQ, Delagates, Timers/Events, Async/Await, HTTPClient/API requests, DataBinding, and better seperation of View and Data Logic. The application saves the data so it can be used later to check both what types and where certain weather events occur the most.

The Graph View section is based off of the ScottPlot library. If you want to check out Scott's API for your own use: https://scottplot.net/

![DemoWA_DB_Gif.gif](https://github.com/Rowwbit/WeatherAlert_DB/blob/main/DemoWA_DB_Gif.gif)

![WeatherDB MainEventView](https://github.com/Rowwbit/WeatherAlert_DB/blob/main/Weather_DB_MainEventView.PNG)

![WeatherDB DBOptions](https://github.com/Rowwbit/WeatherAlert_DB/blob/main/Weather_DB_DBOptions.PNG)
