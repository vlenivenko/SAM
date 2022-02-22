# SAM
The SAM project

## Project structure:
  _00 Common_ - Folder contains core functionality that could be commonly used across the solution. _Note_: the projects could be moved to Nuget in future
  1. _SAM.Core.CQRS_ project contains functionality needed for CQRS approach implementation
  2. _SAM.Core.Utilities_ project may contain low level implementation of commonly used utilities
    
  _01 Api_ - All API could be placed here
  1. _SAM.API_ - Entry point of the application. The API project contains needed _Create Patient_ and _Create Search_ endpoints. Additional Get Patient List endpoint was implementation in order to return patients presented in the system

  _02 Services_ - Business logic area. Contains project with specific logic related to patient and search as well as test projects
  1. _SAM.Patient_ - Functionality related to patient area
  2. _SAM.Patient.Tests_ - Appropriate patient area tests
  3. _SAM.Search_ - Functionality related to search area
  4. _SAM.Search.Tests_ - Appropriate search area tests

  03 Repository - Data access layer area
  1. _SAM.Repository_ - Functionality that interacts with datasource (EF In-memory)


## Endpoints:
1. **GET** api/patient - Returns patient list
2. **POST** api/patient - Creates new patient
3. **POST** api/search - Creates search

Note: for additional information, please check swagger **swagger/index.html**
