USE [axosnet]
GO

/****** Object:  Table [dbo].[Doctos]    Script Date: 6/28/2019 12:21:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Doctos](
	[DoctoID] [int] NOT NULL IDENTITY(1,1),
	[Fecha] [date] NULL DEFAULT GETDATE(),
	[Monto] [money] NOT NULL,
	[ProveedorID] [int] NOT NULL,
	[MonedaID] [int] NOT NULL,
	[Comentario] [text] NOT NULL,
	[FechaHoraCreacion] [datetime] NULL DEFAULT GETDATE(),
	[FechaHoraModificacion] [datetime] NULL DEFAULT GETDATE(),
	[UsuarioID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Doctos] PRIMARY KEY CLUSTERED 
(
	[DoctoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Doctos]  WITH CHECK ADD  CONSTRAINT [FK_Doctos_Monedas] FOREIGN KEY([MonedaID])
REFERENCES [dbo].[Monedas] ([MonedaID])
GO

ALTER TABLE [dbo].[Doctos] CHECK CONSTRAINT [FK_Doctos_Monedas]
GO

ALTER TABLE [dbo].[Doctos]  WITH CHECK ADD  CONSTRAINT [FK_Doctos_Proveedores] FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
GO

ALTER TABLE [dbo].[Doctos] CHECK CONSTRAINT [FK_Doctos_Proveedores]
GO

