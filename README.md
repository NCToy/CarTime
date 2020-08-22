# CarTime

  Project was developed for my portfolio.
  
  What you need to run the project?
  
  First step: download Visual Studio 2019, MS SQL Server.
  
  Second step: to clone the project and open it in Visual Studio (CarTime.sln).
  
  Third step: open Package Manager Console and enter the command "update-database".
  Then Visual Studio will use migrations for creating database and updating it.
  
  Fourth step: press Ctrl + F5 to run project.
  
  Below, you can see users with roles. So, you can sign in and check features of every role.
  
  Admin:
    login - admin
    password - superpass
  
  Manager:
    login - manager
    password - passmanager
    
  User:
    login - user01
    password - user01
    
Also you can register new user. Press button "SignUp" for it. If you want to add a manager, you must authorized as admin. in Admin panel you can add new manager.
So, you can add or edit any car. You must authorized as manager and added new car. Some photo with cars or brands you can find in wwwroot/img/cars, wwwroot/img/brands.
