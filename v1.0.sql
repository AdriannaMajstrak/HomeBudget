USE [Budget]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 18.04.2018 21:38:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](15) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountGroup]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](15) NOT NULL,
 CONSTRAINT [PK_AccountGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckPoint]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[SettlementPeriodId] [int] NOT NULL,
 CONSTRAINT [PK_CheckPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckPointEntry]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckPointEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckPointId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_CheckPointEntry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counter]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SettlementPeriodId] [int] NOT NULL,
	[Name] [nchar](15) NOT NULL,
	[AmountCounter] [money] NOT NULL,
	[Rate] [money] NULL,
	[TransactionOutgoesId] [int] NULL,
	[Equalized] [bit] NOT NULL,
	[TransactionIncomeId] [int] NULL,
	[DestinationAccountGroupId] [int] NULL,
	[ForecastIncrease] [money] NULL,
	[Equalizable] [bit] NOT NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CounterTemplate]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CounterTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](15) NOT NULL,
	[Rate] [money] NULL,
	[Equalizable] [bit] NOT NULL,
	[DestinationAccountGroupId] [int] NULL,
	[ForecastIncrease] [money] NULL,
 CONSTRAINT [PK_CounterTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CyclePayment]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CyclePayment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SettlementPeriodId] [int] NOT NULL,
	[Name] [nchar](15) NOT NULL,
	[Amount] [money] NOT NULL,
	[TransactionOutgoesId] [int] NULL,
	[DestinationAccountGroupId] [int] NULL,
	[TransactionIncomeId] [int] NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CyclePaymentTemplate]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CyclePaymentTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](15) NOT NULL,
	[Amount] [money] NOT NULL,
	[AccountGroupId] [int] NULL,
 CONSTRAINT [PK_BillTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SettlementPeriod]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettlementPeriod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SettlementPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 18.04.2018 21:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](20) NOT NULL,
	[Amount] [money] NOT NULL,
	[AccountGroupId] [int] NOT NULL,
	[SettlementPeriodId] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (1, N'IngAdam        ', 1)
INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (2, N'IngAda         ', 1)
INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (3, N'OkoAdam        ', 2)
INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (4, N'OkoAda         ', 2)
INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (5, N'Rachunki       ', 3)
INSERT [dbo].[Account] ([Id], [Name], [GroupId]) VALUES (6, N'Studia         ', 4)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[AccountGroup] ON 

INSERT [dbo].[AccountGroup] ([Id], [Name]) VALUES (1, N'Biezace        ')
INSERT [dbo].[AccountGroup] ([Id], [Name]) VALUES (2, N'OKO            ')
INSERT [dbo].[AccountGroup] ([Id], [Name]) VALUES (3, N'Rachunki       ')
INSERT [dbo].[AccountGroup] ([Id], [Name]) VALUES (4, N'Studia         ')
SET IDENTITY_INSERT [dbo].[AccountGroup] OFF
SET IDENTITY_INSERT [dbo].[CheckPoint] ON 

INSERT [dbo].[CheckPoint] ([Id], [Date], [SettlementPeriodId]) VALUES (1, CAST(N'2017-08-25T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[CheckPoint] OFF
SET IDENTITY_INSERT [dbo].[CheckPointEntry] ON 

INSERT [dbo].[CheckPointEntry] ([Id], [CheckPointId], [AccountId], [Amount]) VALUES (1, 1, 1, 20.0000)
INSERT [dbo].[CheckPointEntry] ([Id], [CheckPointId], [AccountId], [Amount]) VALUES (2, 1, 2, 30.0000)
INSERT [dbo].[CheckPointEntry] ([Id], [CheckPointId], [AccountId], [Amount]) VALUES (3, 1, 3, 11.0000)
INSERT [dbo].[CheckPointEntry] ([Id], [CheckPointId], [AccountId], [Amount]) VALUES (4, 1, 4, 1.0000)
SET IDENTITY_INSERT [dbo].[CheckPointEntry] OFF
SET IDENTITY_INSERT [dbo].[Counter] ON 

INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (2, 1, N'Woda           ', 0.0000, 12.4100, NULL, 0, NULL, 3, 6.0000, 1)
INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (4, 1, N'gaz            ', 0.0000, 1.1000, NULL, 0, NULL, 3, 100.0000, 1)
INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (5, 1, N'Prąd           ', 0.0000, 5.2000, NULL, 1, NULL, NULL, NULL, 0)
INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (7, 2, N'Woda           ', 77.5000, 12.4100, NULL, 0, NULL, 3, 6.0000, 1)
INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (8, 2, N'gaz            ', 68.0000, 1.1000, NULL, 0, NULL, 3, 100.0000, 1)
INSERT [dbo].[Counter] ([Id], [SettlementPeriodId], [Name], [AmountCounter], [Rate], [TransactionOutgoesId], [Equalized], [TransactionIncomeId], [DestinationAccountGroupId], [ForecastIncrease], [Equalizable]) VALUES (9, 2, N'Prąd           ', 200000.0000, 5.2000, NULL, 0, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Counter] OFF
SET IDENTITY_INSERT [dbo].[CyclePayment] ON 

INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (1, 1, N'Czynsz         ', 500.0000, NULL, NULL, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (2, 1, N'Gaz            ', 100.0000, NULL, 3, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (4, 1, N'Prąd           ', 55.0000, NULL, NULL, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (5, 1, N'Studia         ', 300.0000, NULL, 4, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (6, 2, N'Czynsz         ', 500.0000, NULL, NULL, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (7, 2, N'Gaz            ', 100.0000, NULL, 3, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (8, 2, N'Prąd           ', 0.0000, NULL, NULL, NULL)
INSERT [dbo].[CyclePayment] ([Id], [SettlementPeriodId], [Name], [Amount], [TransactionOutgoesId], [DestinationAccountGroupId], [TransactionIncomeId]) VALUES (9, 2, N'Studia         ', 300.0000, NULL, 4, NULL)
SET IDENTITY_INSERT [dbo].[CyclePayment] OFF
SET IDENTITY_INSERT [dbo].[SettlementPeriod] ON 

INSERT [dbo].[SettlementPeriod] ([Id], [Date]) VALUES (1, CAST(N'2017-09-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[SettlementPeriod] ([Id], [Date]) VALUES (2, CAST(N'2017-10-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[SettlementPeriod] OFF
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (6, N'Wyplata             ', 1000.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (7, N'Wydatek1            ', -20.2200, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (8, N'Jedzenie            ', -13.3000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (9, N'jedz                ', 0.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1010, N'buty                ', -100.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1011, N'buty                ', -110.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1012, N'tratwa              ', -80.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1013, N'tratwa              ', -80.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1014, N'gg                  ', -50.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1015, N'jutrzenka           ', -600.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1016, N'j                   ', -8.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1017, N'NAzwa               ', -1.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1018, N'era                 ', -1.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1019, N'era                 ', -1.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1020, N'jogurt              ', -1.2000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1021, N'Tylko               ', -0.5000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1022, N'j                   ', -7.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1023, N'j                   ', -7.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1024, N'j                   ', -7.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1025, N'6                   ', 0.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1026, N'7                   ', 0.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1027, N'y                   ', -78.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1028, N'ul                  ', -20.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1029, N'Nowy                ', -1.2000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1030, N'nowy2               ', -3.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1031, N'AlaMakota           ', -15.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1032, N'u                   ', -8.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1033, N'minus               ', -9.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1034, N'bez minusa          ', -9.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1035, N'wyplata             ', 6000.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1036, N'napiwek pop         ', 10.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1038, N'książka             ', -34.9900, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1039, N'Premia Adam z ing   ', 20000.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1040, N'Samochód            ', -19500.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1041, N'domek               ', -1095.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1042, N'7                   ', 0.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1043, N'cos                 ', 0.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1044, N'cos2                ', -3.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1045, N'teraz ok            ', -100.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1046, N'Premia              ', 200.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1047, N'Nowe rekawiczki     ', -19.9900, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1048, N'Nowy szal           ', -50.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1049, N'premia              ', 20000.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (1050, N'k                   ', 10.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2028, N'Oszcz               ', -1520.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2029, N'Oszcz               ', 1520.0000, 2, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2030, N'Z oszczednosci      ', -1520.0000, 2, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2031, N'Z oszczednosci      ', 1520.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2032, N'vrrad               ', -40.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2033, N'Tytuł               ', -20.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2034, N'Tytuł               ', 20.0000, 2, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2035, N'Tets                ', -20.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2036, N'Tets                ', 20.0000, 2, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2037, N'tytul               ', -15.0000, 1, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2038, N'tytul               ', 15.0000, 2, 1)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2039, N'lo                  ', -2.0000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2040, N'lo                  ', 2.0000, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2041, N'Woda                ', -248.2000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2042, N'Woda                ', 248.2000, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2043, N'Woda                ', -248.2000, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2044, N'Woda                ', 248.2000, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2045, N'Woda                ', -24.8200, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2046, N'Woda                ', 24.8200, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2047, N'Woda                ', -37.2300, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2048, N'Woda                ', 37.2300, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2049, N'Woda                ', -37.2300, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2050, N'Woda                ', 37.2300, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2051, N'Woda                ', -37.2300, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2052, N'Woda                ', 37.2300, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2053, N'Woda                ', -37.2300, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2054, N'Woda                ', 37.2300, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2055, N'Woda                ', -37.2300, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2056, N'Woda                ', 37.2300, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2057, N'Woda                ', -12.4100, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2058, N'Woda                ', 12.4100, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2059, N'Woda                ', -12.4100, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2060, N'Woda                ', 12.4100, 2, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2061, N'Woda                ', -18.6150, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2062, N'Woda                ', 18.6150, 3, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2063, N'Woda                ', -18.6150, 1, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2064, N'Woda                ', 18.6150, 3, 2)
INSERT [dbo].[Transaction] ([Id], [Name], [Amount], [AccountGroupId], [SettlementPeriodId]) VALUES (2080, N'sgtg                ', 2.0000, 1, 2)
SET IDENTITY_INSERT [dbo].[Transaction] OFF
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_AccountGroup]
GO
ALTER TABLE [dbo].[CheckPoint]  WITH CHECK ADD  CONSTRAINT [FK_CheckPoint_SettlementPeriod] FOREIGN KEY([SettlementPeriodId])
REFERENCES [dbo].[SettlementPeriod] ([Id])
GO
ALTER TABLE [dbo].[CheckPoint] CHECK CONSTRAINT [FK_CheckPoint_SettlementPeriod]
GO
ALTER TABLE [dbo].[CheckPointEntry]  WITH CHECK ADD  CONSTRAINT [FK_CheckPointEntry_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[CheckPointEntry] CHECK CONSTRAINT [FK_CheckPointEntry_Account]
GO
ALTER TABLE [dbo].[CheckPointEntry]  WITH CHECK ADD  CONSTRAINT [FK_CheckPointEntry_CheckPoint] FOREIGN KEY([CheckPointId])
REFERENCES [dbo].[CheckPoint] ([Id])
GO
ALTER TABLE [dbo].[CheckPointEntry] CHECK CONSTRAINT [FK_CheckPointEntry_CheckPoint]
GO
ALTER TABLE [dbo].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_AccountGroup] FOREIGN KEY([DestinationAccountGroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[Counter] CHECK CONSTRAINT [FK_Counter_AccountGroup]
GO
ALTER TABLE [dbo].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_SettlementPeriod] FOREIGN KEY([SettlementPeriodId])
REFERENCES [dbo].[SettlementPeriod] ([Id])
GO
ALTER TABLE [dbo].[Counter] CHECK CONSTRAINT [FK_Counter_SettlementPeriod]
GO
ALTER TABLE [dbo].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_Transactions] FOREIGN KEY([TransactionOutgoesId])
REFERENCES [dbo].[Transaction] ([Id])
GO
ALTER TABLE [dbo].[Counter] CHECK CONSTRAINT [FK_Counter_Transactions]
GO
ALTER TABLE [dbo].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_Transactions2] FOREIGN KEY([TransactionIncomeId])
REFERENCES [dbo].[Transaction] ([Id])
GO
ALTER TABLE [dbo].[Counter] CHECK CONSTRAINT [FK_Counter_Transactions2]
GO
ALTER TABLE [dbo].[CounterTemplate]  WITH CHECK ADD  CONSTRAINT [FK_CounterTemplate_AccountGrop] FOREIGN KEY([DestinationAccountGroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[CounterTemplate] CHECK CONSTRAINT [FK_CounterTemplate_AccountGrop]
GO
ALTER TABLE [dbo].[CyclePayment]  WITH CHECK ADD  CONSTRAINT [FK_Bill_AccountGroup] FOREIGN KEY([DestinationAccountGroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[CyclePayment] CHECK CONSTRAINT [FK_Bill_AccountGroup]
GO
ALTER TABLE [dbo].[CyclePayment]  WITH CHECK ADD  CONSTRAINT [FK_Bill_SettlementPeriod] FOREIGN KEY([SettlementPeriodId])
REFERENCES [dbo].[SettlementPeriod] ([Id])
GO
ALTER TABLE [dbo].[CyclePayment] CHECK CONSTRAINT [FK_Bill_SettlementPeriod]
GO
ALTER TABLE [dbo].[CyclePayment]  WITH CHECK ADD  CONSTRAINT [FK_Bill_ToTable] FOREIGN KEY([TransactionOutgoesId])
REFERENCES [dbo].[Transaction] ([Id])
GO
ALTER TABLE [dbo].[CyclePayment] CHECK CONSTRAINT [FK_Bill_ToTable]
GO
ALTER TABLE [dbo].[CyclePayment]  WITH CHECK ADD  CONSTRAINT [FK_CyclePayment_ToTable] FOREIGN KEY([TransactionIncomeId])
REFERENCES [dbo].[Transaction] ([Id])
GO
ALTER TABLE [dbo].[CyclePayment] CHECK CONSTRAINT [FK_CyclePayment_ToTable]
GO
ALTER TABLE [dbo].[CyclePaymentTemplate]  WITH CHECK ADD  CONSTRAINT [FK_BillTemplate_AccountGroup] FOREIGN KEY([AccountGroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[CyclePaymentTemplate] CHECK CONSTRAINT [FK_BillTemplate_AccountGroup]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_AccountGroup] FOREIGN KEY([AccountGroupId])
REFERENCES [dbo].[AccountGroup] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_AccountGroup]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_SettlementPeriod] FOREIGN KEY([SettlementPeriodId])
REFERENCES [dbo].[SettlementPeriod] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_SettlementPeriod]
GO
