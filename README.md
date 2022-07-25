# Reservation System API

This API is being built to enable small yoga business to move from "pen and paper" reservation system to online reservation system. The vision is to enable business owner to quickly create and offer new yoga classes to customers. Customers then should be able to make reservations for these classes under their profile. It benefits both the business and clients because of the high availabilty, flexibility and responsivnes in comparison to classical "pen an paper" reservations. First phase is to create an API with C#. Second phase is to create front-end with 

## Stack
* .NET 6
* Azure SQL Database
* Azure App Service
* Azure API Management
* Azure Automation 

## API Demo
You can find the API demo published on Azure App Serivce <a  href="https://resclass.azurewebsites.net/swagger/index.html">here</a>. The 

## Database
Database behind the API is Azure SQL Database. It is accesed from the API via Dapper that calls stored procedures to perform CRUD operations. The database is reset to default demo state every midnight CET/CEST. The job scheduler used to run the reset task is Azure Automation. Below you can find the diagram of the database.  

<p align="center">
 <img style="width:50%; height:auto;" alt="Data model of resrvation system API" src="https://user-images.githubusercontent.com/62188066/180731442-cd8f4f94-7308-4e09-a467-fe3f8c70de81.png">
</p>
