#  Toy Block Factory

There is a factory that makes toy blocks. The blocks come in three different shapes (square, circle and triangle) and in three different colours (red, blue and yellow)

The factory does not keep any stock of blocks, instead blocks are produced per order. For example let's say the factory gets 3 orders in a day, the factory would make each order on it's own. 

An order consists of a combination of shapes and colors. Below is an example of what an order would look like:  

|          | Red | Blue | Yellow |  
|----------|-----|------|--------|  
| Square   | 1   | -    | 1      |  
| Triangle | -   | 2    | -      |  
| Circle   | -   | 1    | 2      |  




### Functionalities

Factory has an order management system that should be able to enter single order details. It also has a Report Generator to generate reports. 

This application can:
- Take orders
- Generate an Invoice Report per order
- Generating a Cutting List Report per order
- Generating a Painting Report per order
- Take bulk orders
- Generate Cutting List Reports due on a particular date
- Generate Painting Reports due on a particular date

Everything will be done via the console.

### Example input
~~~
Welcome to the Toy Block Factory!

Please input your Name: Mark Pearl  
Please input your Address: 1 Bob Avenue, Auckland
Please input your Due Date: 19 Jan 2021

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

## Pricing of blocks

Blocks have a fixed price which is determine by the shape. We have the following pricing list:

- Square blocks cost $1 
- Triangle blocks cost $2 
- Circle blocks cost $3

There is no additional charge for the colour unless the shape is in red, in which case we charge an additional $1 per shape surcharge is applied.

## Order details

An order has a:
- Customer Name
- Address
- Due Date
- Order Number
- List of the blocks in an order with their respective colors

## Cutting & Painting Department Needs

The factory cutting department and a painting department

Your cutting department has 3 groups, one that cuts squares, one that cuts circles and one that cuts triangles

You have a single painting department that paints all shapes but wants to have shapes grouped by color

# Invoice Report

Name: Mark Pearl
Address: 1 Bob Avenue, Auckland
Due Date: 19 Jan 2021
Order #: 0001

|          | Red | Blue | Yellow |
|----------|-----|------|--------|
| Square   | 1   | -    | 1      |
| Triangle | -   | 2    | -      |
| Circle   | -   | 1    | 2      |

Squares 		2 @ $1 ppi = $2  
Triangles		2 @ $2 ppi = $4  
Circles			3 @ $3 ppi = $9  
Red color surcharge	1 @ $1 ppi = $1  

Total : $16

### Example
~~~
Your invoice report has been generated:

Name: Mark Pearl Address: 1 Bob Avenue, Auckland Due Date: 19 Jan 2021 Order #: 0001

|          | Red | Blue | Yellow |
|----------|-----|------|--------|
| Square   | 1   | -    | 1      |
| Triangle | -   | 2    | -      |
| Circle   | -   | 1    | 2      |

Squares 		2 @ $1 ppi = $2  
Triangles		2 @ $2 ppi = $4  
Circles			3 @ $3 ppi = $9  
Red color surcharge	1 @ $1 ppi = $1 

Total : $16
~~~

# Cutting List Report

Name: Mark Pearl
Address: 1 Bob Avenue, Auckland
Due Date: 19 Jan 2021
Order #: 0001

|          | Qty |
|----------|-----|
| Square   | 2   |
| Triangle | 2   |
| Circle   | 3   |

### Example 

~~~
Your cutting list has been generated:

Name: Mark Pearl Address: 1 Bob Avenue, Auckland Due Date: 19 Jan 2021 Order #: 0001

|          | Qty |
|----------|-----|
| Square   | 2   |
| Triangle | 2   |
| Circle   | 3   |


~~~

# Painting Report

Name: Mark Pearl
Address: 1 Bob Avenue, Auckland
Due Date: 19 Jan 2021
Order #: 0001

|          | Red | Blue | Yellow |
|----------|-----|------|--------|
| Square   | 1   | -    | 1      |
| Triangle | -   | 2    | -      |
| Circle   | -   | 1    | 2      |

### Example
~~~
Your painting report has been generated:

Name: Mark Pearl Address: 1 Bob Avenue, Auckland Due Date: 19 Jan 2021 Order #: 0001

|          | Red | Blue | Yellow |
|----------|-----|------|--------|
| Square   | 1   | -    | 1      |
| Triangle | -   | 2    | -      |
| Circle   | -   | 1    | 2      |
~~~

------------------------------------------------------------------------------------------------------------

# Run program: 

#### Clone this repo to your local machine:
~~~
https://github.com/monajena27/Toy-Block-Factory.git
~~~

#### Navigate to the /ToyBlockFactoryConsole folder.

#### In your terminal type command:
~~~
dotnet run
~~~


