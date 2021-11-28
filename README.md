# BikeDistributor Library #
A set of .netcore31 libraries to target any .NET platforms from aging but still very much in use .NET 4.7.2 to .NET 6 and its latest technologies.

A non-secondary aim of this project is also to select a bunch of well-established and mantained opensource libs & technologies and lightly wrap them into my own library

Also providing an abstraction layer for various db system. At the moment the only complete implementation is a MongoDB connector.

MongoDb is freely hosted by Atlas at https://www.mongodb.com/en/company

(Having the ability to host a database on a free remote server is truly a great gift, as one can easily test latency and try his/her favorite spinning stuff. Thanks again to Atlas)

## Structure of the VS Solution ##

### MV.Framework: ###
This is a business-domain-agnostic library.
  It mainly contains Generic Interfaces and BaseClasses (Ex. BaseRepository) with a high potential of being re-usable in BikeDistributor-like projects.
  
  Same goes for other plumbing-facilities like DbQueryAsyncProvider, MongoContext, MongoSettings and the like.

### BikeDistributor: ###
A library project to address a specific Business Domain.
In this case it is a greatly simplified Product Catalogue + Shopping Chart like app.

We have a bunch of Domain Models (namely Product, ProductOption, Order, ReceiptFormat, Customer, Stock) wrapped into respective MongoEnititesXXX.

MongoEntities are saved into respective Repository<MongoEntityXXX> which are only accessible via specialized MongoServicesXXX which take care of CRUD
  and everything else needed. An implementation of the Repository pattern, in short. 
  
  Data get written in No-sql db MongoDb and all revolve around the concept of collections.
  
The library also features some business-domain-specific plumbing and infrasctures that we will see later (maybe) 

 ### BikeDistributor.Test: ###
 Unit-Test project for the **BikeDistributor** project
  
  
 ## Other related projectes at some point in the future ##
 
 ### [BikeShop](https://github.com/mvit777/BikeShop) ###
 A [blazor demo client](https://github.com/mvit777/BikeShop) for the BikeDistributor library.
 This application will feature:
  - An admin area to build the Product Catalog and manage Orders and Orders' stats
  - A public Catalogue + Shopping Chart
  
 A wasm blazor spa app in Net6.0.
  It will be nothing more than a barebone demo.
  
 ### [BikeShop.BlazorComponents](https://github.com/mvit777/BikeShop/tree/master/BikeShop.BlazorComponents)
  A sub-project of BikeShop, is just a thin wrapper around Bootstrap components
 
### [BikeShopWS](https://github.com/mvit777/BikeShop/blob/master/BikeShopWS/gRPC.md) 
  The glue webservice supporting both REST and gRPC
  
### BikeShop.Bot.Customer.Random ###
A selenium web-driver alike set of scripts to create Orders.

It will pick Customers and issue orders using guided random while testing the Blazor app user-interface.
Random values are adjusted on the customers' profile, that is to say one (type of) profile will have more chances of an other to buy/customise some type of bike or to have a particular type of interaction with user interface. 
The data produced should act as the test pool. The alternative would be totally deterministic scripts which I feel would be much more long to code.
  
### BikeShop.Bot.CustomerBehaviors ###
Mainly a dream 'coz I don't know shit of AI beyond simple linear-regression.

This bot should be a console-test project and imitate the shopping behavior of 3-4 stereotypized customer behaviors. Ex:
  - **low budget**: favours special offers and discount over any choice
  - **fashion victim**: always buy newest or more expensive products
  - **customisation maniac**: if he can't choose every single detail of the product he/she does not buy
  - **reseller**: always places bulk orders
  - etc etc ...
 Customer behaviors are composed by freely mixable collections of personality traits that act as indicators of a given Customer Behavior Model, so that you 
  can derive/create also new behaviorus.
  
 Customer Behavior Models will be trained by BikeShop.Bot.Customer.AI
  
 Initial Customer Behavior Models will most inevitably start as stereotypized based on "classic" western literature on the topic of Marketing and Consumerism and then I will try 
  to drive them towards the verification of the following observation by [Karl Gerth](https://karlgerth.com/):
  
 >What is the Chinese dream?” I think the Chinese dream is the American dream plus 10%. It’s not the European dream of restraint and conservation. I think they’re closer to Americans. They want even more, bigger, better—as a generalization of course - Karl Gerth
  
(I know I'm getting a bit bombastic about my project targets but dreaming costs nothing.) 
 
 ## Related and inspiring links ##
 
 - [NoSQL – MongoDB Repository Implementation in .NET Core with Unit Testing example](https://www.thecodebuzz.com/mongodb-repository-implementation-unit-testing-net-core-example/)
 Sounds familiar? Yes, that acted as the firestarter and something more than simply inspiring for me, in fact I "stole" a lot of code from there.
  This TheCodebuzz's article is truly a must-read. It is coincise, informative and features fine crafted code.
 - [MongoDB resources](https://www.mongodb.com/resources)
 - [Reduce Customer Churn](https://info.microsoft.com/ww-landing-predictive-AI-and-marketing-automation-demo-video.html?LCID=en&ocid=eml_pg299577_gdc_comm_ba)
  >Customer buying habits are changing. Learn how to use predictive AI and marketing automation to get a pulse on your customers’ evolving behaviors and keep them coming back.
 
  - [Dynamics 365 Customer Insights](https://dynamics.microsoft.com/en-us/ai/customer-insights/)
  - [ML.NET](https://github.com/dotnet/machinelearning)
  - [Model Builder](https://docs.microsoft.com/en-us/dotnet/machine-learning/automate-training-with-model-builder?WT.mc_id=dotnet-35129-website)
 - [giacomelli/GeneticSharp](https://github.com/giacomelli/GeneticSharp) GeneticSharp is a fast, extensible, multi-platform and multithreading C# Genetic Algorithm library that simplifies the development of applications using Genetic Algorithms (GAs)
 
 - [playwright](https://playwright.dev/dotnet/) Seems there is a new kid in town of end-to-end testing (Selenium alike)
## Books I want/will read soonish
- [Deep Learning for Coders with fastai and PyTorch](https://www.oreilly.com/library/view/deep-learning-for/9781492045519/)by Jeremy Howard, Sylvain Gugger, ISBN: 9781492045526
- [Building Data Science Applications with FastAPI](https://www.packtpub.com/product/building-data-science-applications-with-fastapi/9781801079211) By François Voron, ISBN: 9781801079211
  
- [Old and new approaches to marketing. The quest of their epistemological roots](https://mpra.ub.uni-muenchen.de/30841/1/MPRA_paper_30841.pdf) Volpato, Giuseppe and Stocchetti, Andrea
- [As China Goes, So Goes the World: How Chinese Consumers Are Transforming Everything](https://karlgerth.com/publications/)

## Other things I want to do before dying
  - Contribute in some form to the most controversial but yet incredibly short page on wikipedia [Anthropological theories of value](https://en.wikipedia.org/wiki/Anthropological_theories_of_value)
## News & Events
- [MongoDB.local](https://events.mongodb.com/dotlocallondon?utm_campaign=Int_LCL_MongoDB.local%20London_11_21_EMEA_Virtual%20Reg%20last%20chance&utm_medium=email&utm_source=eloqua&utm_term=Last%20chance%21%20Register%20for%20FREE%20to%20join%20virtually) Tuesday, 9th November (London, UK)
