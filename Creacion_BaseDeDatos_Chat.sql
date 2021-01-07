CREATE TABLE [dbo].[Chat] (
    [Id] INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (20) NOT NULL,
	[Creation] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[User] (
    [Id] INT           IDENTITY (1, 1) NOT NULL,
    [Username]      NVARCHAR (10) NULL,
    [Gmail]      NVARCHAR (70) NOT NULL,
    [Password]   NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[User_Chat] (
	[Id] int not null identity,
    [FUser] INT NULL,
    [FChat]    INT NULL,
	[Date_Join] datetime null,
	[Counter] int null, --lso mensajes enviados
    CONSTRAINT [fk_idChat] FOREIGN KEY ([Fchat]) REFERENCES [dbo].[Chat] ([Id]),
    CONSTRAINT [fk_idUser] FOREIGN KEY ([FUser]) REFERENCES [dbo].[User] ([Id]),
	PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Message] (
    [Id]  INT             IDENTITY (1, 1) NOT NULL,
	[FUser] int null, --a que usuario pertenece
    [FChat] INT             NULL,
    [Body]     NVARCHAR (4000) NULL,
    [Creation]       DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [fk_numeroUser] FOREIGN KEY ([FUser]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [fk_numeroChat] FOREIGN KEY ([FChat]) REFERENCES [dbo].[Chat] ([Id])
);
