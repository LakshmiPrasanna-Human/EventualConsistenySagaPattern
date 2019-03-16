use [RIO.Cards]

--CREATE TABLE Cards (
--  id  INT  NOT NULL    IDENTITY    PRIMARY KEY,
--  CorrelationID  VARCHAR(100)  NOT NULL,
--  CompanyName  VARCHAR(50),
--  CardHolderName VARCHAR(50),
--  CardID VARCHAR(50),
--);

CREATE TABLE IntegrationEventLog (
id  INT  NOT NULL    IDENTITY    PRIMARY KEY,
  CorrelationID VARCHAR(100)  NOT NULL,
  EventId  uniqueidentifier  NOT NULL,
  State  int,
  IntegrationEvent VARCHAR(50),
  TimesSent INT,
  CreationTime DateTime,
  Content VARCHAR(MAX),
  EventTypeName VARCHAR(50),
);

