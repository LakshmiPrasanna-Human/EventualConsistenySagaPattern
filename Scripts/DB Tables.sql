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
  EventId  VARCHAR(100)  NOT NULL,
  EState  VARCHAR(50),
  IntegrationEvent VARCHAR(50),
  TimesSent INT,
  CreationTime DateTime,
  Content VARCHAR(100),
  EventTypeName VARCHAR(50),
);

