USE [LabelPrinter]
GO
/****** Object:  Table [dbo].[LABEL_IN]    Script Date: 9/4/2018 12:15:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LABEL_IN](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LABEL_NAME] [varchar](20) NOT NULL,
	[DATE_TIME] [timestamp] NOT NULL,
	[WEIGHT] [decimal](4, 2) NULL,
	[LINE1] [varchar](100) NULL,
	[LINE2] [varchar](100) NULL,
	[LINE3] [varchar](100) NULL,
	[LINE4] [varchar](100) NULL,
	[LINE5] [varchar](100) NULL,
	[LINE6] [varchar](100) NULL,
	[LINE7] [varchar](100) NULL,
	[LINE8] [varchar](100) NULL,
	[LINE9] [varchar](100) NULL,
	[LINE10] [varchar](100) NULL,
	[LINE11] [varchar](100) NULL,
	[LINE12] [varchar](100) NULL,
	[LINE13] [varchar](100) NULL,
	[LINE14] [varchar](100) NULL,
	[LINE15] [varchar](100) NULL,
 CONSTRAINT [PK_LABEL_IN] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LABEL_OUT]    Script Date: 9/4/2018 12:15:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LABEL_OUT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LABEL_NAME] [varchar](20) NOT NULL,
	[QTY] [decimal](3, 0) NOT NULL,
	[I1] [varchar](100) NULL,
	[I2] [varchar](100) NULL,
	[I3] [varchar](100) NULL,
	[I4] [varchar](100) NULL,
	[I5] [varchar](100) NULL,
	[I6] [varchar](100) NULL,
	[I7] [varchar](100) NULL,
	[I8] [varchar](100) NULL,
	[I9] [varchar](100) NULL,
	[I10] [varchar](100) NULL,
	[I11] [varchar](100) NULL,
	[I12] [varchar](100) NULL,
	[I13] [varchar](100) NULL,
	[I14] [varchar](100) NULL,
	[I15] [varchar](100) NULL,
 CONSTRAINT [PK_LABEL_OUT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
