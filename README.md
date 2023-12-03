# Logging-with-Custom-Providers-in-Asp.Net-Core
**************************************** LOGGER LOBRARY ************************************************8

HOW THE LOGGER IS CONFIGURED
 1-> A class library Logger.Shared, is created for custom logging support i.e. for file logging and Db logging. Other login providers like Console, Debug etc are already build in Asp.net Core.
 2-> Library Logger.Shared is being referenced by Logger.Api which is a Wep API project to demontrate the use of the Logger library.
 3-> Api project has appsettings.json, where in, we can provide logging configuration to specify what logging providers are being supported.
 4-> In appsettings.json under "Logging" we provide category-wise loglevels to specify: under what category what different Log Levels the log providers can log the message for.
 5-> For file logging folderpath and FileName need to be specified in the appsettings.json file under Logging:File:Options 
 6->a-> For DB logging a db needs to be created either by restoring the db back file or by running a create db script file present in the LoggingDatabase folder.
 6->b-> write connectionstring in the appsettings.json file under Logging:Database:Options:ConnectionString. Table name is already set in the config.
 7-> One done with 6th step, hit f5.  Some info logs are added under Get method of the weatherForcastController class for demo purpose.
 8-> Execute the Get Action on WeatherForeCastController. Verify the logs added in db, file and console.
 
 
 
