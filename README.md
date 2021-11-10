# BikeDistributor Library #
Trying to evolve my attempt at TrainRoadChallengeV2 into a framework

This is how I'm trying to evolve my attempt at https://github.com/trainerroad/BackendRefactorChallengeV2 into a more generic framework.
I restarted from scratch the one I submitted (which was quite different) this time on top of netcore3.1 LTS and MongoDb.

MongoDb is freely hosted by Atlas at https://www.mongodb.com/en/company

## Structure of the VS Solution ##

### BikeDistributor: ###
A library project to address a specific Business Domain.
In this case it is a greatly simplified Product Catalogue + Shopping Chart like app.

We have a bunch of Domain Models (namely Product, ProductOption, Order, ReceiptFormat, Customer, Stock) wrapped into respective MongoEnititesXXX.

MongoEntities are saved into respective Repository<MongoEntityXXX> which are only accessible via specialized MongoServicesXXX which take care of CRUD
  and everything else needed. An implementation of the Repository pattern, in short. 
  
  Data get written in No-sql db MongoDb and all revolve around the concept of collections.
  
The library also features some business-domain-specific plumbing and infrasctures that we will see later (maybe) 

### MV.Framework: ###
This is a business-domain-agnostic library.
  It mainly contains Generic Interfaces and BaseClasses (Ex. BaseRepository) with a high potential of being re-usable in BikeDistributor-like projects.
  
  Same goes for other plumbing-facilities like DbQueryAsyncProvider, MongoContext, MongoSettings and the like.
  
 ### BikeDistributor.Test: ###
 Unit-Test project for the **BikeDistributor** project
  
  
 ## Other related projectes at some point in the future ##
 
 ### [BikeShop](https://github.com/mvit777/BikeShop) ###
 A [blazor demo client](https://github.com/mvit777/BikeShop) for the BikeDistributor library.
 This application will feature:
  - An admin area to build the Product Catalog and manage Orders and Orders' stats
  - A public Catalogue + Shopping Chart
  
 A wasm blazor spa app in Net5.0.
  It will be nothing more than a barebone demo.
  
 ### [BikeShop.BlazorComponents](https://github.com/mvit777/BikeShop/tree/master/BikeShop.BlazorComponents)
  A sub-project of BikeShop, is just a thin wrapper around Bootstrap components
 
### [BikeShopWS](https://github.com/mvit777/BikeShop/blob/master/BikeShopWS/gRPC.md) 
  The glue webservice supporting both REST and gRPC
  
### BikeShop.Bot.Customer.Random ###
A selenium web-driver alike set of scripts to create Orders.

It will pick Customers and issue orders randomly while testing the Blazor app user-interface
  
### BikeShop.Bot.CustomerBehaviors ###
Mainly a dream 'coz I don't know shit of AI beyond simple linear-regression.

This bot should be a console-test project and imitate the shopping behavior of 3-4 stereotypized customer behaviors. Ex:
  - low budget: favours special offers and discount over any choice
  - fashion victim: always buy newest or more expensive products
  - customisation maniac: if he can't choose every single detail of the product he/she does not buy
  - reseller: always places bulk orders
  - etc etc ...
 Customer behaviors are composed by freely mixable collections of personality traits that act as indicators of a given Customer Behavior Model, so that you 
  can derive/create also new behaviorus.
  
 Customer Behavior Models are trained by BikeShop.Bot.Customer.AI 
 
 ## Related and inspiring links ##
 
 - [NoSQL – MongoDB Repository Implementation in .NET Core with Unit Testing example](https://www.thecodebuzz.com/mongodb-repository-implementation-unit-testing-net-core-example/)
 Sounds familiar? Yes, that acted as the firestarter and something more than simply inspiring for me, in fact I "stole" a lot of code from there.
  This TheCodebuzz's article is truly a must-read. It is coincise, informative and features fine crafted code.
 - [MongoDB resources](https://www.mongodb.com/resources)
 
 - [GeneticSharp](https://github.com/giacomelli/GeneticSharp)

## News & Events
- [MongoDB.local](https://events.mongodb.com/dotlocallondon?utm_campaign=Int_LCL_MongoDB.local%20London_11_21_EMEA_Virtual%20Reg%20last%20chance&utm_medium=email&utm_source=eloqua&utm_term=Last%20chance%21%20Register%20for%20FREE%20to%20join%20virtually) Tuesday, 9th November (London, UK)
