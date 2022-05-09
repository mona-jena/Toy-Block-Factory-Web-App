#  Toy Block Factory

[![build status](https://badge.buildkite.com/8f19c12dbe6f3d7215bf1864c3a99900b97734aa0afda30365.svg?branch=master&theme=00aa65,ce2554,2b74df,8241aa,fff,fff)](https://buildkite.com/myob/mona-app)

------------------------------------------------------------------------------------------------------------
## Tools & Technologies
- App built on .NET framework
- Containerized using Docker
- Deployed on Kubernetes
- Hosted on AWS
- Automated deployment that runs on CI/CD Buildkite pipeline
- Automated quality and security checks through Sonarqube

------------------------------------------------------------------------------------------------------------
## Code Architecture:
![image](https://user-images.githubusercontent.com/68258766/167348780-ec4ef86c-bd76-4cfb-af65-feb921c1fac5.png)

------------------------------------------------------------------------------------------------------------
## Web App Url:
~~~
https://mona.svc.platform.myobdev.com
~~~
## Endpoints:

#### Create new order:
~~~
POST  /order 
~~~
#### Add blocks to an order:
~~~
POST  /order/{orderId}/addblock 
~~~
#### Delete order:
~~~
DELETE  /order?orderId={orderId}
~~~
#### Get an existing order:
~~~
GET  /order?orderId={orderId}
~~~
#### Get all orders:
~~~
GET  /orders
~~~
#### Submit order:
~~~
PUT  /order?orderId={orderId}
~~~
#### Get report:
~~~
GET  /report?orderId={orderId}&reportType={ReportType}
~~~
------------------------------------------------------------------------------------------------------------
# Toy Block Factory Specifications

There is a factory that makes toy blocks. The blocks come in three different shapes (square, circle and triangle) and in three different colours (red, blue and yellow)

The factory does not keep any stock of blocks, instead blocks are produced per order. For example let's say the factory gets 3 orders in a day, the factory would make each order on it's own. 

An order consists of a combination of shapes and colors. Below is an example of what an order would look like:  

|          | Red | Blue | Yellow |  
|----------|-----|------|--------|  
| Square   | 1   | -    | 1      |  
| Triangle | -   | 2    | -      |  
| Circle   | -   | 1    | 2      |  




### Functionalities

Factory has an Order Management System that is able to take in single order details at a time. It also has a Report System to generate reports. 

This application can:
- Take orders
- Generate an Invoice Report per order
- Generating a Cutting List Report per order
- Generating a Painting Report per order
- Take bulk orders
- Generate Cutting List Reports due on a particular date
- Generate Painting Reports due on a particular date

####Requests can be made via the console or hitting the above api endpoints. 

####This is how an order would be taken via the console:
### Example input
~~~
Welcome to the Toy Block Factory!

Please input your Name: Mark Pearl  
Please input your Address: 1 Bob Avenue, Auckland
Please input your Due Date: 11 Feb 2021

Please input the number of Red Squares: 1
Please input the number of Blue Squares: 
Please input the number of Yellow Squares: 1

Please input the number of Red Triangle:
Please input the number of Blue Triangle: 2
Please input the number of Yellow Triangle:

Please input the number of Red Circle:
Please input the number of Blue Circle: 1
Please input the number of Yellow Circle: 2

~~~

### Pricing of blocks

Blocks have a fixed price which is determined by the shape. We have the following pricing list:

- Square blocks cost $1 
- Triangle blocks cost $2 
- Circle blocks cost $3

Red colour blocks are charged an additional $1 per shape surcharge, other colours have no surcharges applied.

### Order details

An order has a:
- Customer Name
- Address
- Due Date
- Order Number
- List of the blocks in an order with their respective colors

### Cutting & Painting Department Needs

The factory cutting department and a painting department

Your cutting department has 3 groups, one that cuts squares, one that cuts circles and one that cuts triangles

You have a single painting department that paints all shapes but wants to have shapes grouped by color

## Invoice Report

Name: Mark Pearl &nbsp;
Address: 1 Bob Avenue, Auckland &nbsp;
Due Date: 11 Feb 2021 &nbsp;
Order #: 0001 

|          |      Red |   Yellow |     Blue |
|----------|----------|----------|----------|
| Square   |        1 |        1 |        - |
| Triangle |        - |        - |        2 |
| Circle   |        - |        2 |        1 |

Squares 		              2 @ $1 ppi = $2  
Triangles		              2 @ $2 ppi = $4  
Circles			              3 @ $3 ppi = $9  
Red color surcharge	      1 @ $1 ppi = $1  

Total : $16

### Example
~~~
Your invoice report has been generated:

Name: Mark Pearl  Address: 1 Bob Avenue, Auckland  Due Date: 11/02/2021  Order #: 0001

|          |      Red |   Yellow |     Blue |
|----------|----------|----------|----------|
| Square   |        1 |        1 |        - |
| Triangle |        - |        - |        2 |
| Circle   |        - |        2 |        1 |

Square                    2 @ $1 ppi = $2
Triangle                  2 @ $2 ppi = $4
Circle                    3 @ $3 ppi = $9
Red colour surcharge      1 @ $1 ppi = $1

Total : $16

~~~

## Cutting List Report

Name: Mark Pearl &nbsp;
Address: 1 Bob Avenue, Auckland &nbsp;
Due Date: 11 Feb 2021 &nbsp;
Order #: 0001

|          | Qty |
|----------|-----|
| Square   | 2   |
| Triangle | 2   |
| Circle   | 3   |

### Example 

~~~
Your cutting list report has been generated:

Name: Mark Pearl Address: 1 Bob Avenue, Auckland Due Date: 11/02/2021 Order #: 0001

|          |      Qty |
|----------|----------|
| Square   |        2 |
| Triangle |        2 |
| Circle   |        3 |

~~~

## Painting Report

Name: Mark Pearl &nbsp;
Address: 1 Bob Avenue, Auckland &nbsp;
Due Date: 11 Feb 2021 &nbsp;
Order #: 0001

|          |      Red |   Yellow |     Blue |
|----------|----------|----------|----------|
| Square   |        1 |        1 |        - |
| Triangle |        - |        - |        2 |
| Circle   |        - |        2 |        1 |

### Example
~~~
Your painting report has been generated:

Name: Mark Pearl Address: 1 Bob Avenue, Auckland Due Date: 11/02/2021 Order #: 0001

|          |      Red |   Yellow |     Blue |
|----------|----------|----------|----------|
| Square   |        1 |        1 |        - |
| Triangle |        - |        - |        2 |
| Circle   |        - |        2 |        1 |

~~~

------------------------------------------------------------------------------------------------------------
## Run program locally:

#### 1. Clone this repo to your local machine:
~~~
https://github.com/monajena27/Toy-Block-Factory-Web-App.git
~~~

#### 2. From your terminal, navigate to the /ToyBlockFactoryConsole folder.

#### 3. Type command:
~~~
dotnet run
~~~
------------------------------------------------------------------------------------------------------------
