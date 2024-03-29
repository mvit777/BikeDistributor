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
  
ideally I wish to also somewhat target this observation by Eric Miller (this sentence is attributed at this guy on Wikipedia page consumerism, but I cannot find reference anywhere else. I apologise for my blatant ignorance)
>Businesses have realized that wealthy consumers are the most attractive targets of marketing. The upper class's tastes, lifestyles, and preferences trickle down to become the > standard for all consumers. The not-so-wealthy consumers can "purchase something new that will speak of their place in the tradition of affluence"

the only other reference I found to this author and sentence is on one of the craziest but interesting internet page I have ever seen in my life
[Shea's SoapBox - BuySexual](https://sheaitisntsoopublications.wordpress.com/) I don't know who this lady is but I like her crazy blog even if it looks abandoned
  
I know I'm getting a bit bombastic about my project targets but dreaming costs nothing.
 
 ## Related and inspiring links ##
 
 - [NoSQL – MongoDB Repository Implementation in .NET Core with Unit Testing example](https://www.thecodebuzz.com/mongodb-repository-implementation-unit-testing-net-core-example/)
 Sounds familiar? Yes, that acted as the firestarter and something more than simply inspiring for me, in fact I "stole" a lot of code from there.
  This TheCodebuzz's article is truly a must-read. It is coincise, informative and features fine crafted code.
 - [MongoDB resources](https://www.mongodb.com/resources)
 - [Reduce Customer Churn](https://info.microsoft.com/ww-landing-predictive-AI-and-marketing-automation-demo-video.html?LCID=en&ocid=eml_pg299577_gdc_comm_ba)
  >Customer buying habits are changing. Learn how to use predictive AI and marketing automation to get a pulse on your customers’ evolving behaviors and keep them coming back.
 
  - [Dynamics 365 Customer Insights](https://dynamics.microsoft.com/en-us/ai/customer-insights/)
  - [ML.NET](https://github.com/dotnet/machinelearning) is a cross-platform open-source machine learning (ML) framework for .NET.
    - [Model Builder](https://docs.microsoft.com/en-us/dotnet/machine-learning/automate-training-with-model-builder?WT.mc_id=dotnet-35129-website) ML.NET Model Builder is an intuitive graphical Visual Studio extension to build, train, and deploy custom machine learning models.
 - [giacomelli/GeneticSharp](https://github.com/giacomelli/GeneticSharp) GeneticSharp is a fast, extensible, multi-platform and multithreading C# Genetic Algorithm library that simplifies the development of applications using Genetic Algorithms (GAs)
 
 - [playwright](https://playwright.dev/dotnet/) Seems there is a new kid in town of end-to-end testing (Selenium alike)
## Books I want/will read soonish
- [Deep Learning for Coders with fastai and PyTorch](https://www.oreilly.com/library/view/deep-learning-for/9781492045519/) by Jeremy Howard, Sylvain Gugger, ISBN: 9781492045526
- [Building Data Science Applications with FastAPI](https://www.packtpub.com/product/building-data-science-applications-with-fastapi/9781801079211) By François Voron, ISBN: 9781801079211
  
- [Old and new approaches to marketing. The quest of their epistemological roots](https://mpra.ub.uni-muenchen.de/30841/1/MPRA_paper_30841.pdf) Volpato, Giuseppe and Stocchetti, Andrea
- [As China Goes, So Goes the World: How Chinese Consumers Are Transforming Everything](https://karlgerth.com/publications/) by Karl Gerth
- [A Thousand Brains: A New Theory of Intelligence](https://www.goodreads.com/book/show/54503521-a-thousand-brains) by Jeff Hawkins, Richard Dawkins. ISBN 154167581
  This book is on Guglielmo Cancelli latest reading list and sounds pretty interesting
- [Confessions of an Advertising Man](https://openlibrary.org/books/OL8774119M/Confessions_of_an_Advertising_Man) by David Ogilvy - Atheneum, Revised edition, 1988, ISBN 0-689-70800-9
## Other things I want to do before dying
  - Contribute in some form to the most controversial but yet incredibly short page on wikipedia [Anthropological theories of value](https://en.wikipedia.org/wiki/Anthropological_theories_of_value)
## News & Events
- [Microsoft Power Platform - Virtual Training](https://mktoevents.com/Microsoft+Event/314239/157-GQE-382) January 18, 2022
- [Migrare a MongoDB](https://events.mongodb.com/darelazionaleamongodb?utm_campaign=Int_WB_Webinar%20da%20relazionale%20a%20MongoDB_01_22_EMEA%20E-mail%201&utm_medium=email&utm_source=eloqua&utm_term=%5BSAVE%20THE%20DATE%5D%20Migrare%20un%27applicazione%20da%20un%20database%20relazionale%20a%20MongoDB%3A%20perch%C3%83%C2%A9%20e%20come%20farlo) Una serie in 3 atti di webinar sulla migrazione da un database relazionale a MongoDB
