# StataTuba

Console is where I spend most of my time.  Maybe someday I'll actually deploy a service, but for now.
Here's what StataTuba does:
Get and Store Data - This is the raw data.  Store a local copy so that we can write the transform code without having to hammer the source server.
Transform Data - Transform the source data into a common format.  This might be CSV, JSON file, JSON in Mongo, SQL, etc.
Measure Data - This is the funnest part, take the base data and create a bunch of measures in order to explore whatever meanings it might have at that time.
Visualize - Bah, we'll get to that!

Here are the projects:
Console - Just a place to dev and fuck-around in.
Common - (core, base, shared, etc)  Stuff that will be used a lot of places.  Classes and IO that supports those classes.  The older I get, the less I like multiple projects crowding up my solution.  Besides Common appears at the top of the solution, so far.
ETL - This is where scraping, transform and measures occur.  Fucking magic stuff!! (no, not Magic fucking stuff.)
Web - Future
IdentityServer - Future
Service - Future

This one is important
1 - All Measures are calculated using the Adjust Prices, which are reflected in OHLC.  Any back testing, which involves spending money will use OrigOpen and OrigClose to accurately reflect the historical price paid.


Vars.cs - Should contain only global static vars.  If a var is only used in one namespace, DON'T put it here.
