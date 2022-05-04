USE [StudyWebApi]
GO

/****** Object:  Table [dbo].[TestTable]    Script Date: 2022/5/4 11:07:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestTable](
	[Id] [uniqueidentifier] NOT NULL,
	[TsetInt] [int] NOT NULL,
	[TsetString] [nvarchar](max) NULL,
	[TsetDateTime] [datetime2](7) NULL,
	[TsetBool] [bit] NOT NULL,
	[CreateTime] [datetime2](7) NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_TestTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


