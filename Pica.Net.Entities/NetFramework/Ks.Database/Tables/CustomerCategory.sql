
CREATE TABLE [dbo].[CustomerCategory](
	[IdCategory] [nvarchar](50) NOT NULL,
	[NameCategory] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[Amount] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO