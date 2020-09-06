CREATE TABLE [dbo].[Items](
	[ItemId] [nchar](10) NOT NULL,
	[ProdId] [numeric](22, 0) NULL,
	[ProductName] [nchar](50) NULL,
	[PartNum] [nchar](20) NULL,
	[Price] [numeric](9, 2) NULL,
	[Quantity] [numeric](9, 0) NULL,
	[OrdId] [nchar](10) NULL,
	[IdInvoince] [nchar](10) NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO