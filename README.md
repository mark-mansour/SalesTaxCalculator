# SalesTaxCalculator
A REST API application to calculate sales tax on a retail transaction in NC.



## Getting Started

These instructions will give you a copy of the project up and running on
your local machine for development and testing purposes. See deployment
for notes on deploying the project on a live system.

### Prerequisites

Requirements for the software and other tools to build, test and push 
- Microsoft Visual Studio 2019 Community Edition
- Microsoft SQL Server 2019 Developer Edition
- Postman 

### Installing

To get a development environment running

1- Pull Code from GitHub. 
2- Run the DB script to create the database structure: ~\SalesTaxCalculator\Database\DDL.sql.
3- Run the DB script to insert data in CountyTax table: ~\SalesTaxCalculator\Database\DML.sql.
4- Compile the whole solution.
5- Run unit tests.
6- Run the project "SalesTaxCalculator.API" on "IIS Express" to run the REST API.



## Running the tests

The solution "SalesTaxCalculator" has unit test projects. Just open the solution and you should find the unit tests in the Test Explorer



## Running manual tests from Postman

The file "~\SalesTaxCalculator\AvalaraAssessment.postman_collection.json" contains the collection and the requests which can be used to query the REST API



## Authors

  - **Mark Mansour**


## Acknowledgments

I used help from the following sources on the internet:
- "Let's build a REST API using C#, ASP NET Core, Dapper, SQL Server, Repository Pattern", by Faith Olusegun,
https://www.bing.com/videos/search?q=build+rest+api+c%23&docid=608052023589043717&mid=A1573164BDE91ABB7B2FA1573164BDE91ABB7B2F&view=detail&FORM=VIRE
- "Dapper: Getting Started", by Steve Michelotti, https://app.pluralsight.com/library/courses/getting-started-dapper/table-of-contents
- "README Template", by Billie Thompson, https://github.com/PurpleBooth


