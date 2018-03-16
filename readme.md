# Vita Quality
This is a simple Utility App used for Quality Control with involves:
- Email workflow to receipe External Issues
*- Issue Tracking to receipe Inner Issues* (**TBI**)
- Agile Scrum Workflow to map Issues in Dev Team Work Items

## Workflow
We follow this approach:
- We consume Mail service *and Issue service* (**TBI**) to get the Inbounds Tickets
- We convert Inbound tickets to Defect Work Items
- We lead workitems to resolution state

So, first of all, in the Convert page we read from Email Service our inbound tickets then we process them into the Acigle Scrum workflow, mapping each ticket into single work item so this will be assigned to a dev mantainance team.
In any inbound ticket match any existing workitem, this fact will cause the work item to be reopened.

In this way, Dev Team can process their inbound work items to their resolved state.

## Technologies
We had to be interfaced with several servicese such as:
- Exchange for Email Workflow
- Jira for Issue Tracking
- TFS for Agile Scrum Workflow ()

For doing that we have implemented some basic clients.

### Authentication
We follow a basic Form-Cookie Authentication approach

### Documentation
[External Authentication](https://docs.microsoft.com/it-it/aspnet/web-api/overview/security/external-authentication-services)

### Ews Client
We consume Microsoft Exchange Ews service to comunicate with our mail server

#### Documentation
[Get started with EWS client applications](https://msdn.microsoft.com/EN-US/library/office/dn789003.aspx)

#### Download
[Microsoft Exchange Web Services Managed API 2.2 ](https://www.microsoft.com/en-us/download/details.aspx?id=42951)

### Microsoft Team Foundation Server Extended Client 
We use Microsoft TFS Extended Client to comunicate with our TFS Server

#### Download
[Microsoft.TeamFoundationServer.ExtendedClient](https://www.nuget.org/packages/Microsoft.TeamFoundationServer.ExtendedClient/)

#### Documentation
Here's some online doc for that
* [Samples](https://docs.microsoft.com/en-us/vsts/integrate/get-started/client-libraries/samples)
* [Integration](https://opbuildstorageprod.blob.core.windows.net/output-pdf-files/en-us/MSDN.team-services/live/integrate.pdf)

### REST/SOAP Namespaces on this page require the following NuGet packages:
* Microsoft.TeamFoundationServer.ExtendedClient
* Microsoft.TeamFoundationServer.Client
* Microsoft.VisualStudio.Services.Client
* Microsoft.VisualStudio.Services.InteractiveClient

#### Jira REST Client
We consume JIRA REST service to comunicate with our Issue Tracker

#### Documentation
[JIRA REST Service Methods](https://docs.atlassian.com/software/jira/docs/api/REST/7.6.1/)


