## Introduction
Recently [Postmark](http://www.postmarkapp.com) [introduced](http://blog.postmarkapp.com/post/15687406657/introducing-postmark-inbound-easily-parse-replies-other) a new feature to allow a user to send or forward an email to a special inbound email address and automatically receive a[ JSON formatted](http://developer.postmarkapp.com/developer-inbound-parse.html) "web hook" API call to a url resource of your choosing. The benefit of this service is that it takes away the complicated task of setting up an email server and parsing messages yourself. In order to use the service all you need to do is setup a server application to receive the web hook API call. This post and sample code will show you how to setup an API end point using the .NET WCF WebAPI component.

## Examining the Sample Project
For this example I've borrowed from the WCF WebAPI [code](http://wcf.codeplex.com/wikipage?title=Getting%20started:%20Building%20a%20simple%20web%20api) hosted at [CodePlex](http://wcf.codeplex.com) so you can refer to the tutorial for further API examples using WCF WebAPI. Just like in the CodePlex example, I've written this API receiver inside of an ASP.NET MVC3 web application. There are [other ways](http://www.bizcoder.com/index.php/2011/04/16/the-worlds-simplest-wcf-web-api/) to host a WCF WebAPI module but I think this example makes sense for a lot of users who might be integrating Postmark Inbound into their existing web applications.

* Begin by downloading the postmark-inbound-demo code. The project is a Visual Studio 2010 solution and assumes you have [ASP.NET MVC3](http://www.asp.net/mvc) installed. The test project runs [Xunit](http://xunit.codeplex.com/) so in order to run the tests you will want to install the Xunit tools.
* Open the solution. You should have two projects, postmark-inbound-demo and postmark-inbound-demo.tests.
* In the references section, you'll see we've added several new namespaces like System.ServiceModel by way of installing "WebAPI" from [Nuget](http://nuget.org/).
* Open the API folder under the web project. There is a class called `PostmarkApi.cs`. This is where our WebMethods are listed. These are the "end points" of the API. The` [ServiceContract]` attribute on the class indicates that this class should be exposed over http. There is one method that responds to an http post and returns an "Email" object.
* Open the Resources folder and take a look at the Email.cs class. This is modeled after the JSON data that is sent from the Postmark Inbound web hook. Apart from that the Email class is just a plain old C# object (POCO).
* Return to the `PostmarkApi.cs` class. The single method in this class has the `[WebInvoke(UriTemplate = "", Method = "POST")]` attribute. This means the method will only accept POST actions over http and nothing else. The code inside will correctly parse the JSON POSTed from the Postmark web hook into the Email object from the Resources folder.
* In the Global.asax.cs file we've added some routing to complement the standard MVC routes:

		routes.Add(new ServiceRoute("api/emails", new HttpServiceHostFactory() { Configuration = config }, typeof(PostmarkApi)));

## Notes
* While this is good example code for creating an API end point, it shouldn't be used verbatim in your project. I've demonstrated saving a file attachment directly in the web method but this should be handled less ridgedly by your own processor or repository classes.
* For further examples of performing other actions with WCF WebAPI, please consult the [documentation](http://wcf.codeplex.com/documentation) and tutorials on the WCF CodePlex site.
