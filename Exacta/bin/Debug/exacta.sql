USE [master]
GO
/****** Object:  Database [Exacta]    Script Date: 24.10.2019 17:07:22 ******/
CREATE DATABASE [Exacta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Exacta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Exacta.mdf' , SIZE = 247808KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Exacta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Exacta_log.ldf' , SIZE = 20096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Exacta] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Exacta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Exacta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Exacta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Exacta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Exacta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Exacta] SET ARITHABORT OFF 
GO
ALTER DATABASE [Exacta] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Exacta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Exacta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Exacta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Exacta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Exacta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Exacta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Exacta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Exacta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Exacta] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Exacta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Exacta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Exacta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Exacta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Exacta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Exacta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Exacta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Exacta] SET RECOVERY FULL 
GO
ALTER DATABASE [Exacta] SET  MULTI_USER 
GO
ALTER DATABASE [Exacta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Exacta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Exacta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Exacta] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Exacta] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Exacta]
GO
/****** Object:  User [OLIMPIAS\un0e]    Script Date: 24.10.2019 17:07:22 ******/
CREATE USER [OLIMPIAS\un0e] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [OLIMPIAS\umuk]    Script Date: 24.10.2019 17:07:22 ******/
CREATE USER [OLIMPIAS\umuk] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [OLIMPIAS\ulyc]    Script Date: 24.10.2019 17:07:22 ******/
CREATE USER [OLIMPIAS\ulyc] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_datareader] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [OLIMPIAS\ulyc]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [OLIMPIAS\ulyc]
GO
/****** Object:  Table [dbo].[Articole]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Articol] [nvarchar](50) NOT NULL,
	[Descriere] [nvarchar](200) NULL,
	[Collection] [nvarchar](50) NULL,
	[Client] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Articole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientss]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientss](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[VATNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Telephone] [nvarchar](50) NULL,
	[Mail] [nvarchar](50) NULL,
	[Bank] [nvarchar](50) NULL,
 CONSTRAINT [PK_Clientss] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_Collections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lines]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lines](
	[LineID] [int] IDENTITY(1,1) NOT NULL,
	[LineName] [nvarchar](50) NOT NULL,
	[LineManager] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lines] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[log]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IDM] [int] NOT NULL,
	[TS] [datetime] NOT NULL,
	[MVC] [bit] NOT NULL,
	[MLC] [bit] NOT NULL,
	[CIC] [bit] NOT NULL,
	[TIC] [bit] NOT NULL,
	[ALS] [bit] NOT NULL,
	[ALF] [bit] NOT NULL,
	[ALM] [bit] NOT NULL,
	[PW] [bit] NOT NULL,
	[AUTO] [bit] NOT NULL,
	[SPEED] [int] NULL,
	[NAME] [nvarchar](50) NULL,
	[PHASE] [nvarchar](50) NULL,
	[OPERATOR] [nvarchar](50) NULL,
	[OrderNumber] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[log1]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log1](
	[ID] [bigint] NOT NULL,
	[IDM] [int] NOT NULL,
	[TS] [datetime] NOT NULL,
	[MVC] [bit] NOT NULL,
	[MLC] [bit] NOT NULL,
	[CIC] [bit] NOT NULL,
	[TIC] [bit] NOT NULL,
	[ALS] [bit] NOT NULL,
	[ALF] [bit] NOT NULL,
	[ALM] [bit] NOT NULL,
	[PW] [bit] NOT NULL,
	[AUTO] [bit] NOT NULL,
	[SPEED] [int] NULL,
	[NAME] [nvarchar](50) NULL,
	[PHASE] [nvarchar](50) NULL,
 CONSTRAINT [PK_log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Machines]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IDM] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[NrMatricola] [nvarchar](50) NOT NULL,
	[DateArrival] [datetime] NOT NULL,
	[Line] [nvarchar](50) NOT NULL,
	[ProductionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operatii]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operatii](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodOperatie] [nvarchar](20) NOT NULL,
	[Operatie] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Operatii] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatiiArticole]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatiiArticole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdArticol] [int] NOT NULL,
	[IdOperatie] [int] NOT NULL,
	[BucatiOra] [float] NOT NULL,
	[PhaseName] [nvarchar](50) NULL,
 CONSTRAINT [PK_OperatiiArticole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationParameters]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationParameters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdArticol] [int] NOT NULL,
	[IdOperatie] [int] NOT NULL,
	[BucatiOra] [float] NOT NULL,
	[AffIniziale] [int] NOT NULL,
	[Velocita] [int] NOT NULL,
	[Tens1] [int] NOT NULL,
	[Finezza] [int] NOT NULL,
	[Tens2] [int] NOT NULL,
	[Tens3] [int] NOT NULL,
	[TensIniziali] [int] NOT NULL,
	[PtFinali] [int] NOT NULL,
	[TensFinale] [int] NOT NULL,
	[TensAff] [int] NOT NULL,
	[P1] [int] NOT NULL,
	[P2] [int] NOT NULL,
	[GapIniziale] [int] NOT NULL,
	[GapFinale] [int] NOT NULL,
	[AffFinale] [int] NOT NULL,
	[Components] [int] NULL,
 CONSTRAINT [PK_OperationParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Surname] [nvarchar](40) NOT NULL,
	[Telephone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](30) NOT NULL,
	[Line] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Client] [nvarchar](20) NOT NULL,
	[Article] [nvarchar](20) NOT NULL,
	[DateArrival] [datetime] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametriArticole]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametriArticole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdOperatiArticole] [int] NOT NULL,
	[AffIniziale] [int] NOT NULL,
	[Velocita] [int] NOT NULL,
	[Tens1] [int] NOT NULL,
	[Finezza] [int] NOT NULL,
	[Tens2] [int] NOT NULL,
	[Tens3] [int] NOT NULL,
	[TensIniziali] [int] NOT NULL,
	[PtFinali] [int] NOT NULL,
	[TensFinale] [int] NOT NULL,
	[TensAff] [int] NOT NULL,
	[P1] [int] NOT NULL,
	[P2] [int] NOT NULL,
	[GapIniziale] [int] NOT NULL,
	[GapFinale] [int] NOT NULL,
	[AffFinale] [int] NOT NULL,
 CONSTRAINT [PK_ParametriArticole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ActiveLines]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ActiveLines]
AS
SELECT DISTINCT dbo.Machines.Line
FROM            dbo.[log] AS g INNER JOIN
                             (SELECT        IDM, MAX(TS) AS n_date
                               FROM            dbo.[log]
                               GROUP BY IDM) AS a ON a.IDM = g.IDM AND a.n_date = g.TS INNER JOIN
                         dbo.Machines ON g.IDM = dbo.Machines.IDM
GO
/****** Object:  StoredProcedure [dbo].[getMinutesBetween]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getMinutesBetween]
@IDM int,
@startDate date,
@endDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
WITH    rows AS
        (
        SELECT  *, ROW_NUMBER() OVER (ORDER BY ts) AS rn
        FROM log
		where IDM = @IDM and convert(varchar,ts,101) >= @startDate 
		and convert(varchar,ts,101) <= @endDate and TIC = 1	
        )
SELECT  DATEDIFF(minute, mc.ts, mp.ts) as MinutesBetweenQty, mc.TS as TimeOfDay
FROM    rows mc
JOIN    rows mp
ON      mc.rn = mp.rn - 1
order by mc.TS
END
GO
/****** Object:  StoredProcedure [dbo].[selectFromArticole]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[selectFromArticole] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select * from Articole
END
GO
/****** Object:  StoredProcedure [dbo].[sp_efficiency_per_machine]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_efficiency_per_machine]

@idm int,
@current_date datetime,
@article nvarchar(50),
@phase nvarchar(50)

AS
BEGIN

	SET NOCOUNT ON;

	select --DATEPART(hour, l.ts) as hourx,		
		count(l.tic) as Cuts,		
		OperationParameters.BucatiOra,
		OperationParameters.Components
	from log l
	full outer join Articole on Articole.Articol = @article
	full outer join Operatii on Operatii.Operatie = @phase
	full outer join OperationParameters on OperationParameters.IdArticol = Articole.Id
	and OperationParameters.IdOperatie = Operatii.Id
	where l.IDM = @idm and TIC = 'true' and
		convert(varchar,l.ts,110) = @current_date and l.PHASE = @phase and l.NAME = @article
		
		group by l.IDM,
		 DATEPART(hour, l.ts),
		  OperationParameters.BucatiOra,
		   OperationParameters.Components
	order by 
	l.IDM, 
	DATEPART(hour, l.ts)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_currentMachine_efficiency]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_get_currentMachine_efficiency]
@idm int,
@currentDate datetime,
@article nvarchar(50),
@phase nvarchar(50)

AS
BEGIN

	SET NOCOUNT ON;

	select l.IDM,
		DATEPART(hour, l.ts) as hourx,		
		count(l.tic) as Cuts,		
		OperationParameters.BucatiOra,
		OperationParameters.Components
	from log l
	full outer join Articole on Articole.Articol = @article
	full outer join Operatii on Operatii.Operatie = @phase
	full outer join OperationParameters on OperationParameters.IdArticol = Articole.Id
	and OperationParameters.IdOperatie = Operatii.Id
	where l.IDM = @idm and TIC = 'true' and
		convert(varchar,l.ts,110) = @currentDate and l.PHASE = @phase and l.NAME = @article
		
		group by l.IDM,
		 DATEPART(hour, l.ts),
		  OperationParameters.BucatiOra,
		   OperationParameters.Components
	order by 
	l.IDM, 
	DATEPART(hour, l.ts)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getcurrentdata]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_getcurrentdata]
	
AS
BEGIN

SET NOCOUNT ON;

select g.IDM,g.TS,g.AUTO,g.ALS,g.ALF,g.ALM,g.NAME,g.PHASE, g.TIC, g.CIC, g.MVC, g.MLC from log g
  inner join
    ( 
	select IDM, max(TS) as n_date
    from log
    group by IDM
    )a 
 on a.IDM = g.IDM 
  and a.n_date=TS 
  order by g.IDM
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getdata]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[sp_getdata]
	@startDate		nvarchar(20),
	@endDate		nvarchar(20)

as
begin
set nocount on;

select g.idm,
	   Convert(date,g.ts,101) as dates,
	   g.ts,
	   g.auto,
	   g.pw,
	   g.als,g.alf,g.alm,
	   g.name, g.phase,
	   g.CIC, g.TIC, g.MVC, g.MLC,
	   DATEPART(hour, g.ts) as hourx,
	   g.OPERATOR
	from log g
	where Convert(date,g.ts,101) 
		between CAST(@startDate AS DATE) and CAST(@endDate AS DATE)	
	order by g.idm,
			 g.TS,
			 g.name,g.PHASE asc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getEfficiency]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getEfficiency] 

@Article nvarchar(50) = null,
@Phase nvarchar(50) = null,
@dateFrom datetime,
@dateTo datetime

AS
BEGIN	
	SET NOCOUNT ON;
	SELECT g.IDM,
		DATEPART(hour, g.ts) as hourx,
		g.ts,
		g.TIC,	
		g.NAME,
		g.PHASE,
		OperationParameters.BucatiOra,
		OperationParameters.Components,
		g.OPERATOR
	from log g
	full outer join Articole on Articole.Articol = g.NAME
	full outer join Operatii on Operatii.Operatie = g.PHASE
	full outer join OperationParameters on OperationParameters.IdArticol = Articole.Id
	and OperationParameters.IdOperatie = Operatii.Id
	where 
		convert(varchar,g.ts,110) >= @dateFrom and convert(varchar,g.ts,110) <= @dateTo
	order by 
	g.IDM, 
	g.ts,
	DATEPART(hour, g.ts),
	g.NAME,
	g.PHASE
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getlogdata]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_getlogdata] 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	/*
	 * Searches for TIC count, valued as 'true'
	 */

	create table #tmpauto
	(
	idms int, names varchar(50), phase varchar(50), times varchar(50)
	);

	create table #tmpmanual
	(
	idms int, names varchar(50), phase varchar(50), times varchar(50)
	);

	select idm,convert(date,ts,101),name,phase,count(tic) as numofoperations
	from log
		where tic = 'true'
		group by idm,convert(date,ts,101),NAME,phase	

	/*
	 * Cumulate time per AUTO by flag
	 */

	 insert into #tmpauto
	 select idm,name,phase, Convert(TIME, DATEADD(mi, SUM(( DATEPART(hh, ts) * 3600 ) + 
													( DATEPART(mi, ts) * 60 ) + 
													DATEPART(ss, ts)), 0)) as total_time 
	 from log 
		where auto = 'true'
		group by idm,name,phase	
		select * from #tmpauto;

	 insert into #tmpmanual
	 select idm,name,phase,
	 Convert(TIME, DATEADD(hh, SUM(( DATEPART(mi, ts) * 3600 ) + 
													( DATEPART(mi, ts) * 60 ) + 
													DATEPART(ss, ts)), 0)) as total_time
	 from log 
		where auto = 'false'
		group by idm,name,phase

		select * from #tmpmanual

	drop table #tmpauto;
	drop table #tmpmanual;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getmachinecuts]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[sp_getmachinecuts] 
						 @Machine INT,
						 @dateFrom date,
						 @dateTo date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select NAME as Article, PHASE as Fase,count(tic) as Cuts
	from log
		where tic = 'true' and idm = @Machine and 
		convert(varchar, ts, 110) >= @dateFrom 
		and convert(varchar, ts, 110) <= @dateTo
		group by idm,NAME, PHASE, convert(date,ts,101)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getqt]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_getqt]
@MachineID int,
@Article nvarchar(50) = null,
@Phase nvarchar(50) = null,
@startDate nvarchar(20) = null,
@endDate nvarchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(tic) as Cuts
	from log 	
	where tic = 1 and IDM = @MachineID and NAME = @Article and PHASE = @Phase and
	Convert(date,ts,101) 
		between CAST(@startDate AS DATE) and CAST(@endDate AS DATE)	
	group by IDM
END
GO
/****** Object:  StoredProcedure [dbo].[spGetActiveLines]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Peki,,Peki>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetActiveLines]
@StartEndDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT dbo.Machines.Line
FROM            dbo.[log] AS g INNER JOIN
                             (SELECT        IDM, MAX(TS) AS n_date
                               FROM            dbo.[log]
                               WHERE        (CONVERT(varchar, TS, 110) = @StartEndDate)
                               GROUP BY IDM) AS a ON a.IDM = g.IDM AND a.n_date = g.TS INNER JOIN
                         dbo.Machines ON g.IDM = dbo.Machines.IDM
END

GO
/****** Object:  StoredProcedure [dbo].[spGetDataPerHour]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetDataPerHour]
@StartDate datetime,
@EndDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT g.IDM,
		DATEPART(hour, g.ts) as hourx, 
		g.ts,
		g.TIC, g.CIC, g.MVC, g.MLC,
		g.NAME,
		g.PHASE,
		g.ALF, g.ALM, g.ALS,
		OperationParameters.BucatiOra,
		OperationParameters.Components,
		g.OrderNumber,
		g.OPERATOR
	from log g
	left join Articole on Articole.Articol = g.NAME
	left join Operatii on Operatii.Operatie = g.PHASE
	left join OperationParameters on OperationParameters.IdArticol = Articole.Id and
	OperationParameters.IdOperatie = Operatii.Id
	where 
		convert(varchar,g.ts,110) >= @StartDate 
		and convert(varchar,g.ts,110) <= @EndDate
	order by 
	g.IDM, 
	g.ts,
	DATEPART(hour, g.ts)
END


GO
/****** Object:  StoredProcedure [dbo].[spGetTotaleByMachineAndLine]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Peki,,Peki>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetTotaleByMachineAndLine]
@StartEndDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT dbo.Machines.Line, dbo.Machines.IDM, SumTotal
FROM            dbo.[log] AS g 
INNER JOIN
   (SELECT        IDM, MAX(TS) AS n_date, SUM(CAST(TIC AS INT)) AS SumTotal
   FROM           dbo.[log]
   WHERE		  convert(varchar,dbo.[log].TS,110) = @StartEndDate
   GROUP BY IDM) AS a ON a.IDM = g.IDM AND a.n_date = g.TS 
   INNER JOIN
                  dbo.Machines ON g.IDM = dbo.Machines.IDM
END

GO
/****** Object:  StoredProcedure [dbo].[spReportsData]    Script Date: 24.10.2019 17:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spReportsData] 

@dateFrom datetime,
@dateTo datetime

AS
BEGIN	
	SET NOCOUNT ON;
	SELECT g.IDM,
		DATEPART(hour, g.ts) as hourx,
		g.ts,
		g.TIC,
		g.NAME,
		g.PHASE,
		Machines.Line,
		g.OPERATOR, --7
		OperationParameters.BucatiOra, --8
		g.OrderNumber
	from log g
	full outer join Machines on Machines.IDM = g.IDM
	full outer join Articole on Articole.Articol = g.NAME
	full outer join Operatii on Operatii.Operatie = g.PHASE
	full outer join OperationParameters on OperationParameters.IdArticol = Articole.Id
	and OperationParameters.IdOperatie = Operatii.Id
	where 
		convert(varchar,g.ts,110) >= @dateFrom and convert(varchar,g.ts,110) <= @dateTo
	order by 
	Machines.Line,
	g.IDM,	
	g.NAME,
	g.PHASE,
	g.TS,
	DATEPART(hour, g.ts),
	g.OPERATOR
	
	
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[2] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 311
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Machines"
            Begin Extent = 
               Top = 0
               Left = 610
               Bottom = 338
               Right = 782
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActiveLines'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActiveLines'
GO
USE [master]
GO
ALTER DATABASE [Exacta] SET  READ_WRITE 
GO
