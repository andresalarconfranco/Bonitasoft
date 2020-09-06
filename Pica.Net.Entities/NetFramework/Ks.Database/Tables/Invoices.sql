CREATE TABLE [dbo].[Invoices](
	[IdInvoince] [nchar](10) NOT NULL,
	[DateProcess] [datetime] NULL,
	[CustID] [nchar](40) NULL,
	[TotalValue] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdInvoince] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO