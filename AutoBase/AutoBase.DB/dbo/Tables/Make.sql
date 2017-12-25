﻿CREATE TABLE [dbo].[Make]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] VARCHAR(1024) NOT NULL

	CONSTRAINT PK_Make PRIMARY KEY ([Id]),
	CONSTRAINT UN_Make_Name UNIQUE ([Name])
)
