# HabitTrackerApp

Habit Tracker Console App that utilizes SQLite.
User can create, read, update and delete records from the menu.

Walk through the Project:
- On application start, a connection will be made to an existing database
- When user makes their selection from the menu, a prompt will ask them to either Create, Read, Update or Delete a record
- Program will terminate when the user selects 0

Requirements:
- This is an application where you’ll register one habit
- This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- The application should store and retrieve data from a real database
- When the application starts, it should create a sqlite database, if one isn’t present
- It should also create a table in the database, where the habit will be logged
- The app should show the user a menu of options
- The users should be able to insert, delete, update and view their logged habit
- You should handle all possible errors so that the application never crashes
- The application should only be terminated when the user inserts 0
- You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework

