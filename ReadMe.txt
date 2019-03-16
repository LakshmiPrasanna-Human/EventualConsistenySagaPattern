A. Cards

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
		update local db transaction entry as failed status  --- need to implement  -- complete
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
		3. Send Success Event to source microservice - CardOrderRequestIntegrationSuccessEvent --- need to implement
}
catch
{
	4. Send Failure to Source Microservice. -- CardOrderRequestIntegartionFailureEvent --- need to implement
}
 
C. Card - CardOrderRequestIntegartionSuccessEvent  --- need to implement
 
	Write to IntegrationEventLog table with Success state

d. Card - CardOrderRequestIntegartionFailureEvent --- need to implement

Transaction scope
{
	update local db transaction entry as failed status 
	Write to IntegrationEventLog - failure
}




//Scaffold-DbContext "Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


