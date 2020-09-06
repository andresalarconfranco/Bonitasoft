CREATE TABLE [dbo].[Orders](
	[CustId] [nchar](10) NULL,
	[OrdId] [nchar](10) NOT NULL,
	[OrderDate] [date] NULL,
	[Price] [numeric](9, 2) NULL,
	[Status] [nchar](10) NULL,
	[Comments] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

