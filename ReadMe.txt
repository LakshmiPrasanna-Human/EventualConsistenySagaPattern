
*************************FlowStart*******************************
a. Publish CardOrderRequest Event :
1. Write to Cards database with EF Core resiliency strategy:
	1. Cards database with TransactionStatus as 0(Inserted)
	2. Write to IntegrationEventLog with state (Not Published)

2. Write to Event Bus
	1. Write to IntegrationEventLog with state (inprogress)
	2. Publish to Event Service Bus
	3. Write to IntegrationEventLog with state (Published)
	4. In Exception Handler of above code, Update Transaction Status of Card record as 2 (RollBack)

b. Recieve Card Order Request
	1. Write to Hierarchy Database with card details.
	2. Write to IntegrationEventLog with details.
	3. Once Event Bus message processing completed, create CardOrderRequestSuccessEvent to Event service bus.
	4. create CardOrderRequestFailureEvent to Event service bus, if records are not present in Hierarchy Database with CorrelationIds

c. Recieve CardOrderRequestSuccessEvent
	1. Write to Cards database with TransactionStatus as 1(Success)
	2. Write to IntegrationEventLog with state (Success)
	
c. Recieve CardOrderRequestFailureEvent
	1. Write to Cards database with TransactionStatus as 2(Rollback)
	2. Write to IntegrationEventLog with state (Failure)
	

*************************FlowEnd*******************************

	


Transaction scope
{
	1. Write to local db table
	2. Write to IntegrationEventLog table with Not Published state
}
try
{
	3. Write to IntegrationEventLog - inprogress
	4. Write to service bus - CardOrderRequestIntegrationEvent
	5. Write to IntegrationEventLog - published
}
catch
{
	Transaction scope
	{
		update local db transaction entry as failed status  
		Write to IntegrationEventLog - failure
	}
}

B. Hierarchy

Recieve CardOrderRequestIntegrationEvent
try
{
	TransactionScope
	{
		1. Write to local db table
		2. Write to IntegrationEventLog table with Subscribe state
	}
	If above statement are successfull
		3. Send Success Event to source microservice - CardOrderRequestIntegrationSuccessEvent 
}
catch
{
	4. Send Failure to Source Microservice. 
}
 
C. Card - CardOrderRequestIntegartionSuccessEvent  
 
	Write to IntegrationEventLog table with Success state

d. Card - CardOrderRequestIntegartionFailureEvent 

Transaction scope
{
	update local db transaction entry as failed status 
	Write to IntegrationEventLog - failure
}




//Scaffold-DbContext "Server=.\HYDHTC135611L;Database=RIO.Hierarchy;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DBContext


