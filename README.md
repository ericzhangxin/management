# Management Solution Repository
In this repo, I provide a solution to manage Customers by using technologies of ASP.NET WebAPI,Angular4.

# Code Structure
Management.sln contains a console project,a Unit Test project, a ASP.NET WebAPI project and an Angular4 frontend porject.

### CustomerManagement.csproj is a console porject which contains classes CustomerManager,FileReader,ConsoleDisplayer,models.
CustomerManager is the class to manipulate customer collection. It has functionalities of CRUDs and sorting customers.

FileReader reads the customers from file into customer collection. It uses async ReadLineAsync method to read line by line from file. The purpose of using async is to make the IO operations aligned with computational operations responsively.

ConsoleDisplayer displays the customer collection in corresponding sorting criteria.


### CustomerManagementTests.csproj is a test project to test the CustomerManager and FileReader. 
CustomerManagementTests covers the add/sorting test cases and file reading functionality.

### CustomerManagementWebAPI.csproj is a ASP.NET WebAPI project.
CustomerManagementWebAPI provides the end points of GET/POST/PUT/DELETE customer(s) functionalities. 
Once this project is started, there is a introduction on the end points on home page(http://localhost:46692/) as shown below.
(./WebAPI_home.png)
### CustomerManagementFrontend is an Angular4 CLI project.
CustomerManagementFrontend provides the front end of customer management. It has only one angular module app.module so far as this project is simple enough to be contained in one angular module. As this project grows, I should separate out more modules, components and routings based on functions,security concerns.

core folder contains the services which are singltons and injected into components.
customers folder contains the components of display/edit/add customer(s).
shared folder contains shared components like pipes,filter-textbox.






