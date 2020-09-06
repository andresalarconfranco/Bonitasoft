CREATE TABLE [dbo].[CreditCards](
	[CrediCardNumber] [nchar](40) NOT NULL,
	[TypeCard] [nvarchar](50) NULL,
	[CheckCode] [nchar](10) NULL,
	[ExpirationDate] [nchar](10) NULL,
	[NameCard] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CrediCardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO