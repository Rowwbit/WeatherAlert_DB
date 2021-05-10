# Weather Alert DB
Uses the National Weather Service API to document weather alerts in a SQLite DB.

This application checks the NWS API every 15 minutes and aggregates records in a SQLite Database. This is my second full featured project and this application uses more intermediate skills such as: LINQ, Delagates, Timers/Events, Async/Await, HTTPClient/API requests, DataBinding, and better seperation of View and Data Logic. The application saves the data so it can be used later to check both what types and where certain weather events occur the most.

***Next Planned Update:***
Next main update will move the logic for the API request into a windows service that will automatically sync DB records without requiring the main application.

![Application Demo](https://github.com/Rowwbit/WeatherAlert_DB/blob/main/DemoWA_DB_Gif.gif)

![WeatherDB MainEventView](https://github.com/Rowwbit/WeatherAlert_DB/blob/main/Weather_DB_MainEventView.PNG)
