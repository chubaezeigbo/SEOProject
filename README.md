# SEOProject
This app has been created on Visual Studio 2022 to scrape searches from Google and additionally Bing without using their respective APIs.
The logic for the scraping is dependent on the DOMs of both search engines so if these are to change in future then the code will require some changes.

# Design 
The project has been created using ASP.NET Core with a React.js SPA for the front-end. Regarding the database, it communicates with SQL Express. 
In the folder Sql, open the file “database_script.sql” using SQL Express and execute the script to create the database and the table.
The connection string can be found in the appsettings.json file, ensure the server name contained in this SQL connection string matches your system name. 
The backend is split into Controllers, Models and Services.
The front end (the ClientApp folder) is split into components, styles and the App.jsx file.

# Setup
Please ensure you have SQL Express, npm, Node.js and .Net 8 installed on your system to use this project. 
Run ‘npm install’ in the terminal to restore npm packages for the React.js front end. 
The proxy property can be found in package.json in the ClientApp folder. It is currently set to - "https://localhost:7224".
To run the application, ensure that the startup project is set to SEOProject, then click run or F5.

# Improvements
I would have liked to add unit testing but due to time constraints, my busy work schedule, and my daily commute to work I was unable to implement this. In the future, xUnit would have been a good aid.

The addition of trends to show the rankings on a daily/weekly basis would also be a good improvement on this project.

Authentication and authorization

Better CSS Styling
