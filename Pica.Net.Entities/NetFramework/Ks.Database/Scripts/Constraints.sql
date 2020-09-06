USE [KallSonys]
ALTER TABLE [dbo].[PIC_ROLE_USER]  WITH CHECK ADD FOREIGN KEY([ROL_NCODE])
REFERENCES [dbo].[PIC_ROLES] ([ROL_NCODE])
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerCategory] FOREIGN KEY([IdCategory])
REFERENCES [dbo].[CustomerCategory] ([IdCategory])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerCategory]
GO

ALTER TABLE [dbo].[CustomerCreditCard]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCreditCard_Customer] FOREIGN KEY([CustID])
REFERENCES [dbo].[Customer] ([CustID])
GO

ALTER TABLE [dbo].[CustomerCreditCard] CHECK CONSTRAINT [FK_CustomerCreditCard_Customer]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Invoices] FOREIGN KEY([IdInvoince])
REFERENCES [dbo].[Invoices] ([IdInvoince])
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Invoices]
GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Orders] FOREIGN KEY([OrdId])
REFERENCES [dbo].[Orders] ([OrdId])
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Orders]
GO


