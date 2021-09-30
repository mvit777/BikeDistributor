# TRChallengeReloadCore3_1
Trying to evolve my attempt at TrainRoadChallengeV2 into a framework

This is how I'm trying to evolve my attempt at https://github.com/trainerroad/BackendRefactorChallengeV2 into a more generic framework.
I restarted from scratch the one I submitted (which was quite different) this time on top of netcore3.1 LTS and MongoDb.

MongoDb is freely hosted by Atlas at https://www.mongodb.com/en/company

## Structure of the VS Solution ##

### BikeDistributor: ###
A library project to address a specific Business Domain.
In this case it is a greatly simplified Product Catalogue + Shopping Chart like app.

We have a bunch of Domain Models (namely Product, ProductOption, Order, ReceiptFormat, Customer) wrapped into respective MongoEnititesXXX.

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
  
  
 ## Other related projectes ##
 
 ### BikeShop ###
 A blazor client for the BikeDistributor library.
 This application will feature:
  - An admin area to build the Product Catalog and manage Orders and Orders' stats
  - A public Catalogue + Shopping Chart
  
 I have not yet started to work on this project and I'm probably opting for a serverless blazor app in Net5.0
