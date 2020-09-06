CREATE TABLE [dbo].[Customer](
	[CustID] [nchar](40) NOT NULL,
	[FName] [nchar](40) NULL,
	[LName] [nchar](40) NULL,
	[PhoneNumber] [nchar](40) NULL,
	[EMail] [nchar](40) NULL,
	[Password] [nchar](40) NULL,
	[CreditCardType] [nchar](40) NULL,
	[CrediCardNumber] [nchar](40) NULL,
	[Status] [nchar](10) NULL,
 [IdCategory] NVARCHAR(50) NULL, 
    [Country] NCHAR(40) NULL, 
    [City] NCHAR(40) NULL, 
    [ROL_NCODE] INT NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


