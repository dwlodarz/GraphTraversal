# GraphTraversal
[![Build status](https://ci.appveyor.com/api/projects/status/5slaym3urfywwcb9?svg=true)](https://ci.appveyor.com/project/dwlodarz/graphtraversal)

## Technologies used
- Continuous Integration + Uni Test Runner - [AppVeyor](https://ci.appveyor.com/project/dwlodarz/graphtraversal) [![Build status](https://ci.appveyor.com/api/projects/status/5slaym3urfywwcb9?svg=true)](https://ci.appveyor.com/project/dwlodarz/graphtraversal)
- Dependency Injection - [SimpleInjector](https://simpleinjector.org/index.html)
- Exception logging - log4net
- Model <-> Entity mapping - AutoMapper
- UnitTest mocking - [NSubstitute](http://nsubstitute.github.io/)
- Test data generation - [AutoFixture](https://github.com/AutoFixture/AutoFixture)
- DataBase layer - [Neo4j](https://neo4j.com/) NoSQL graph database
- Data access layer - [Neo4jclient](https://github.com/Readify/Neo4jClient)
- Service layer - WCF REST(JSON) service. No contract sharing between the parties
- Front-end - [AngularJS](https://angularjs.org/) v.1.4.7 + [Alchemy.js](http://graphalchemist.github.io/Alchemy/) + [Lodash](https://lodash.com/) + [Bootstrap](http://getbootstrap.com/)
- Data loading app - Simple console application with [HttpClient](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx)

> **Database installation steps**
> - Download [Community edition](https://neo4j.com/download/?ref=home) version of Neo4j
> - Install the package following all of the instructions
> - Run it
> <p align="center">  <img src="/ReadMeImages/1.PNG" width="350"/></p>
> - After first entering the webpage [http://localhost:7474](http://localhost:7474) user will be prompted to change the password (default neo4j/neo4j)
> -The connection string is set in main Web.config of WebServices project
> <p align="center">  <img src="/ReadMeImages/2.PNG" width="450"/></p>

## IIS Setup
- WebServices http://localhost/GraphTraversal.WebServices
- Web application http://localhost/GraphTraversal.WebApp

## Console App setup
- The only configuration required is the Source folder of the xmls
<p align="center">  <img src="/ReadMeImages/3.PNG" width="450"/></p>

> **Test Data**
> - Test data is located in ['GraphTraversal/GraphXml/'](https://github.com/dwlodarz/GraphTraversal/tree/master/GraphXml)

## Web application
The FrontEnd application is written n such a way that it doesn't require scrolling because it takes advantage of SVG and d3.js library bound together with alchemy.js. Basically, it allows to move the graph around, zoom-in and zoom-out and to change the positioning of nodes and edges completelly.
After 2 nodes were selected and 'Calucalte shortest path' clicked, the appropriate edges will be marked in red. Then one has to click 'Reset' in order to start over again.
Most of the js libraries are loaded from CDNs in order to distribute the load.
For a first look and feel of the app please have a look at the video attached:
> [Demo.avi](https://github.com/dwlodarz/GraphTraversal/blob/master/Demo.avi)
> <p align="center">  <img src="/ReadMeImages/4.PNG" width="450"/></p>

## Unit Tests ##
Due to lack of time and other previously scheduled task I cover only a single class with unit test (NodeManager) from the BLL project. This was only meant to show that I understand the concept off.



