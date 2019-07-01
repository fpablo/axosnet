USE [axosnet]
GO

/****** Object:  Table [dbo].[Monedas]    Script Date: 6/28/2019 12:21:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Monedas](
	[MonedaID] [int] NOT NULL IDENTITY(1,1),
	[Nombre] [varchar](30) NOT NULL,
	[TextoImpteLetra] [varchar](30) NOT NULL,
	[Simbolo] [varchar](10) NOT NULL,
	[FechaHoraCreacion] [datetime] NULL DEFAULT GETDATE(),
	[FechaHoraModificacion] [datetime] NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_Monedas] PRIMARY KEY CLUSTERED 
(
	[MonedaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

