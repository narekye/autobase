﻿CREATE TABLE [dbo].[Dump]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[MakeId] INT NOT NULL,
	[ModelId] INT NOT NULL,
	[Year] INT NOT NULL,
	[ModuleId] INT NOT NULL,
	[Memory] VARCHAR(1024) NOT NULL,
	[BlockNumber] VARCHAR(1024) NOT NULL,
	[Path] VARCHAR(1024) NOT NULL

	CONSTRAINT PK_Dump PRIMARY KEY ([Id]),
	CONSTRAINT FK_Dump_Make FOREIGN KEY ([MakeId]) REFERENCES [dbo].[Make]([Id]),
	CONSTRAINT FK_Dump_Model FOREIGN KEY ([ModelId]) REFERENCES [dbo].[Model]([Id]),
	CONSTRAINT FK_Dump_Module FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module]([Id])
)
