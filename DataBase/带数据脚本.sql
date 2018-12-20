USE [Bonus]
GO
/****** Object:  Table [dbo].[BonusReson]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusReson](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BonusResonName] [nvarchar](50) NULL,
	[BonusResonUserId] [varchar](50) NULL,
	[companyId] [nvarchar](50) NULL,
 CONSTRAINT [PK_BONUSRESON] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BonusParameter]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BonusParameter](
	[BonusSetID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](20) NULL,
	[BonusNum] [int] NULL,
	[IsOrNo] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[CompanyID] [nvarchar](50) NULL,
 CONSTRAINT [PK_BONUSPARAMETER] PRIMARY KEY CLUSTERED 
(
	[BonusSetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BonusOutput]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusOutput](
	[BonusOutputId] [bigint] IDENTITY(1,1) NOT NULL,
	[BonusItemId] [bigint] NOT NULL,
	[GrantPerson] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BonusOutput] PRIMARY KEY CLUSTERED 
(
	[BonusOutputId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BonusItemRule]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BonusItemRule](
	[BIRuleID] [int] IDENTITY(1,1) NOT NULL,
	[BIRuleName] [nvarchar](50) NULL,
	[RemainNum] [int] NULL,
	[GainNum] [int] NULL,
	[AvgMoney] [decimal](18, 2) NULL,
	[BonusItemID] [nvarchar](50) NULL,
	[CompanyID] [nvarchar](50) NULL,
 CONSTRAINT [PK_BonusItemRule] PRIMARY KEY CLUSTERED 
(
	[BIRuleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_BonusItemRule] UNIQUE NONCLUSTERED 
(
	[BIRuleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BonusItem2]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusItem2](
	[BonusItemID] [varchar](20) NOT NULL,
	[BIName] [nvarchar](50) NULL,
	[BIPrincipal] [nvarchar](20) NULL,
	[BIState] [int] NOT NULL,
	[BIType] [int] NULL,
	[BIDepID] [varchar](50) NULL,
	[InDate] [int] NULL,
	[CompanyID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[BonusItem2] ([BonusItemID], [BIName], [BIPrincipal], [BIState], [BIType], [BIDepID], [InDate], [CompanyID]) VALUES (N'xmbjj', N'项目部奖金', N'zy', 0, 1, N'44', 1, 1)
INSERT [dbo].[BonusItem2] ([BonusItemID], [BIName], [BIPrincipal], [BIState], [BIType], [BIDepID], [InDate], [CompanyID]) VALUES (N'zyjj001', N'张宇奖金', N'zy', 0, 0, N'0', 1, 1)
/****** Object:  Table [dbo].[BonusItem]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusItem](
	[BonusItemID] [uniqueidentifier] NOT NULL,
	[BIName] [nvarchar](50) NULL,
	[BIPrincipal] [nvarchar](20) NULL,
	[BIState] [int] NOT NULL,
	[BIType] [int] NULL,
	[BIDepID] [varchar](50) NULL,
	[InDate] [int] NULL,
	[CompanyID] [nvarchar](50) NULL,
 CONSTRAINT [PK_BONUSITEM] PRIMARY KEY CLUSTERED 
(
	[BonusItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BonusImpower]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusImpower](
	[ImpowerID] [int] IDENTITY(1,1) NOT NULL,
	[BonusItemID] [uniqueidentifier] NULL,
	[BIPID] [varchar](20) NULL,
	[EmpID] [varchar](20) NULL,
	[AddMoney] [decimal](18, 2) NULL,
	[RemainMoney] [decimal](18, 2) NULL,
	[ImpowerDate] [datetime] NULL,
	[Model] [int] NULL,
	[IsValid] [int] NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_BONUSIMPOWER] PRIMARY KEY CLUSTERED 
(
	[ImpowerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BonusData2]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BonusData2](
	[BonusDataID] [int] IDENTITY(1,1) NOT NULL,
	[BonusItemID] [uniqueidentifier] NULL,
	[DisMan] [varchar](50) NULL,
	[DisDate] [datetime] NULL,
	[EarMan] [varchar](20) NULL,
	[EarMoney] [decimal](18, 2) NULL,
	[EarDate] [datetime] NULL,
	[BonusType] [int] NULL,
	[IsGet] [int] NULL,
	[ResonID] [uniqueidentifier] NULL,
	[CompanyId] [nvarchar](50) NULL,
 CONSTRAINT [PK_BONUSDATA2] PRIMARY KEY CLUSTERED 
(
	[BonusDataID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Authorities]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authorities](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleID] [nvarchar](200) NULL,
	[ModuleCode] [varchar](50) NULL,
	[AuthType] [int] NULL,
 CONSTRAINT [PK_AUTHORITIES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Authorities] ON
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (1, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'4', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (2, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'5', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (3, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'6', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (4, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'7', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (5, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'8', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (6, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'9', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (7, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'10', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (8, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'12', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (9, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'13', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (10, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'14', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (11, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'15', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (12, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'16', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (13, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'17', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (14, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'18', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (15, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'21', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (16, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'22', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (17, N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'23', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (18, N'4458867A-B356-4780-846F-916E3718EB63', N'4', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (19, N'4458867A-B356-4780-846F-916E3718EB63', N'5', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (20, N'4458867A-B356-4780-846F-916E3718EB63', N'6', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (21, N'4458867A-B356-4780-846F-916E3718EB63', N'7', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (22, N'4458867A-B356-4780-846F-916E3718EB63', N'8', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (23, N'4458867A-B356-4780-846F-916E3718EB63', N'9', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (24, N'4458867A-B356-4780-846F-916E3718EB63', N'10', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (25, N'4458867A-B356-4780-846F-916E3718EB63', N'12', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (26, N'4458867A-B356-4780-846F-916E3718EB63', N'13', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (27, N'4458867A-B356-4780-846F-916E3718EB63', N'14', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (28, N'4458867A-B356-4780-846F-916E3718EB63', N'15', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (29, N'4458867A-B356-4780-846F-916E3718EB63', N'16', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (30, N'4458867A-B356-4780-846F-916E3718EB63', N'18', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (31, N'4458867A-B356-4780-846F-916E3718EB63', N'21', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (32, N'4458867A-B356-4780-846F-916E3718EB63', N'22', 1)
INSERT [dbo].[Authorities] ([ID], [RoleID], [ModuleCode], [AuthType]) VALUES (33, N'4458867A-B356-4780-846F-916E3718EB63', N'23', 1)
SET IDENTITY_INSERT [dbo].[Authorities] OFF
/****** Object:  Table [dbo].[Author]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [nvarchar](50) NULL,
	[AuthorUrl] [nvarchar](max) NULL,
	[AuthorIcon] [nvarchar](max) NULL,
	[PID] [int] NULL,
	[IsValid] [int] NULL,
	[Fanwei] [int] NULL,
 CONSTRAINT [PK_AUTHOR] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0代表客户可以看到的菜单，否则客户就不能看到' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Author', @level2type=N'COLUMN',@level2name=N'Fanwei'
GO
SET IDENTITY_INSERT [dbo].[Author] ON
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (1, N'人事管理', NULL, NULL, NULL, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (2, N'奖金管理', NULL, NULL, NULL, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (3, N'配置管理', NULL, NULL, NULL, 0, 1)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (4, N'公司管理', N'/CompanyRegist/CompanyManager', N'depart.png', 1, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (5, N'部门管理', N'/DepartManger/Index', N'depart.png', 1, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (6, N'职位管理', N'/PositionManage/Index', N'zhiwei.png', 1, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (7, N'员工管理', N'/UserManager/Index', N'yuangong.png', 1, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (8, N'角色管理', N'/Role/Index', N'quanxian.png', 1, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (9, N'奖金项管理', N'/BonusItem/Detail', N'jiangjinxiang.png', 2, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (10, N'奖金金额管理', N'/BonusItemRule/Detail', N'jine.png', 2, 1, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (12, N'部门奖金管理', N'/DepartBouns/Index', N'Depjiangjin.png', 2, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (13, N'奖金授权管理', N'/BonusImpower/Index', N'shouquan.png', 2, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (14, N'奖金支出明细（我）', N'/BonusPay/Index', N'mingxi.png', 2, 1, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (15, N'奖金数据查询', N'/BonusDataQuery/Index', N'mingxi.png', 2, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (16, N'奖金参数管理', N'/BonusParameter/Index', N'canshu.png', 2, 0, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (17, N'客户管理', N'/KeHuManager/Index', N'woshou.png', 3, 0, 1)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (18, N'奖金提现', N'/BonusNoGet/Index', N'demo-icons-button', 2, 1, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (21, N'部门奖金(我页面)', N'/UserDepartBonus/Index', N'demo-icons-button', 3, 1, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (22, N'奖金授权(我)', N'/BonusImpower/myImpower', N'demo-icons-button', 2, 1, 0)
INSERT [dbo].[Author] ([AuthorID], [AuthorName], [AuthorUrl], [AuthorIcon], [PID], [IsValid], [Fanwei]) VALUES (23, N'提现审核', N'/DepositAudit/Index', N'mingxi.png', 2, 0, 0)
SET IDENTITY_INSERT [dbo].[Author] OFF
/****** Object:  Table [dbo].[AuditBonus]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AuditBonus](
	[AuditBonusID] [int] IDENTITY(1,1) NOT NULL,
	[GetUserID] [varchar](20) NULL,
	[GetBonusID] [varchar](max) NULL,
	[GetMoney] [decimal](18, 2) NULL,
	[GetTime] [datetime] NULL,
	[IsState] [int] NULL,
	[ConfirmUserID] [varchar](20) NULL,
	[ConfirmTime] [datetime] NULL,
	[Remake] [varchar](200) NULL,
	[CompanyID] [nvarchar](50) NULL,
	[IsDepOrEmp] [int] NULL,
 CONSTRAINT [PK_AuditBonus] PRIMARY KEY CLUSTERED 
(
	[AuditBonusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SugReson]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SugReson](
	[SugGuid] [varchar](200) NOT NULL,
	[SugContent] [nvarchar](200) NULL,
	[SugType] [nvarchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SuggectionComm]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SuggectionComm](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](50) NULL,
	[resonId] [varchar](200) NULL,
	[lxType] [nvarchar](50) NULL,
	[CommitTime] [datetime] NULL,
	[CompId] [nvarchar](50) NULL,
 CONSTRAINT [PK_SuggectionComm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RuleData]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RuleData](
	[RuleDataID] [int] IDENTITY(1,1) NOT NULL,
	[BonusItemID] [varchar](50) NULL,
	[AddMoney] [decimal](18, 2) NULL,
	[RemainMoney] [decimal](18, 2) NULL,
	[JoinDate] [date] NULL,
	[EndDate] [date] NULL,
	[JoinType] [int] NULL,
 CONSTRAINT [PK_RuleData] PRIMARY KEY CLUSTERED 
(
	[RuleDataID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [varchar](200) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
	[IsActive] [int] NULL,
	[CompanyID] [nvarchar](50) NULL,
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Role] ([RoleID], [RoleName], [IsActive], [CompanyID]) VALUES (N'4458867A-B356-4780-846F-916E3718EB63', N'管理员', 0, N'wx512ad5972960e003')
INSERT [dbo].[Role] ([RoleID], [RoleName], [IsActive], [CompanyID]) VALUES (N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'超级管理员', 0, N'wx512ad5972960e003')
/****** Object:  Table [dbo].[ResonDetial]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResonDetial](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ResonID] [uniqueidentifier] NOT NULL,
	[ResonContent] [nvarchar](200) NULL,
	[ResonType] [int] NULL,
 CONSTRAINT [PK_ResonDetial_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Operation](
	[OperCode] [varchar](50) NOT NULL,
	[OperName] [nvarchar](50) NULL,
 CONSTRAINT [PK_OPERATION] PRIMARY KEY CLUSTERED 
(
	[OperCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ModuleOper]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModuleOper](
	[MOCode] [varchar](50) NOT NULL,
	[OperCode] [varchar](50) NULL,
	[ModuleCode] [varchar](50) NULL,
 CONSTRAINT [PK_MODULEOPER] PRIMARY KEY CLUSTERED 
(
	[MOCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KeyCode]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyCode](
	[KeyCodeID] [int] IDENTITY(1,1) NOT NULL,
	[KeyCode] [uniqueidentifier] NULL,
	[State] [int] NULL,
	[CompanyManNum] [int] NULL,
 CONSTRAINT [PK_KEYCODE] PRIMARY KEY CLUSTERED 
(
	[KeyCodeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[KeyCode] ON
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (1, N'986b8b27-8c34-4449-93ef-6a53db61852d', 1, 100)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (2, N'3b55898b-5a73-45e0-9cdf-d64e5cbf1436', 1, NULL)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (3, N'4c70fce7-fcb8-4233-9815-267a75567a22', 0, NULL)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (5, N'9405ec26-c5f0-4836-9787-4306be4d6a4b', 0, NULL)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (6, N'55ca4861-aebc-4db7-920c-229bb9e9db9f', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (7, N'01f41948-68c5-42f9-a0f9-77d64b31635e', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (8, N'3a9ae399-d47e-41f5-a2c2-f41cd7c90db5', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (9, N'36fcc5dc-2120-4cb3-b4fc-40e3e2db7d51', 1, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (10, N'0bd6017e-c61c-4974-9852-d419ddc02f10', 0, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (11, N'f0a77a25-572e-4cb6-886b-042acb234f94', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (12, N'118ab084-aaf7-4c47-bba9-b9c68efd4228', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (13, N'721c1e0d-ffdb-434d-8946-58641dbce0e2', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (14, N'3b18812c-3840-4235-a11f-6ed8b90df553', 0, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (15, N'919da8d0-6781-494b-8841-a9c91e02e695', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (16, N'4cbf489d-ead1-43ee-829e-8eed1adfda1c', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (17, N'ea7b2f0e-fa74-4461-8846-58c09604e5b8', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (18, N'323dbb05-e8c6-491a-ba5f-50981f67a341', 0, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (19, N'0c2e96fc-19a9-4da9-b699-ba5975def714', 0, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (20, N'65211186-70c1-407a-8382-9150c5eb8a9c', 0, 3)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (21, N'2765da4d-a071-4563-b324-08f8ac957298', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (22, N'b0c0f6e3-95a4-478f-b15e-726d9539121b', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (23, N'7133295f-4f80-4d97-a5cf-0c07c98a23c3', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (24, N'c25eeabe-dbfd-4bf7-ba77-50d7eec7b55c', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (25, N'09de7d26-938e-4a74-8393-3ea52c608697', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (26, N'4a54301b-49d9-414c-9433-75d352c9db79', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (27, N'17e1c2fa-054c-44dd-8a2b-e85ef040ecc2', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (28, N'6dfd2732-3958-410c-a70f-37b229cc67f3', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (29, N'7121840b-eceb-44ad-a339-3678580cb6c6', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (30, N'aca45810-6508-4644-918c-9d7ff1930c40', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (31, N'39a1adcd-ba8e-438a-bab3-af51f6d87c48', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (32, N'f4f87176-90e9-467f-a152-2dd3d6744ff1', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (33, N'e72b02e1-956b-4ea8-bf01-be216a9ef9ec', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (34, N'13c6c958-d756-4500-8528-1e572d305190', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (35, N'5a9fa1cd-b242-4c53-9c0b-bf72aef0d2b2', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (36, N'e40e4504-4962-48b1-b323-00df619a8ce0', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (37, N'26d91f07-75a8-4412-9539-f148a298e9fb', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (38, N'5033c94b-51f5-4bcb-9eb0-ddb9855091c8', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (39, N'bcd896cb-2114-4255-845d-9c2dbf51b129', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (40, N'ea4320a9-81dd-4143-8b84-ff9302b1a32c', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (41, N'd3f1243b-a272-432e-b880-9d3ba28eff7f', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (42, N'87814e1c-51d4-49c1-aa44-137d9a9211b1', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (43, N'b92cd762-16ac-460c-9bd1-38e420231208', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (44, N'975d844c-fd8d-420d-ba5d-de65fe50603d', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (45, N'd80cc8d3-0bcb-4517-8784-e5aa6c37279e', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (46, N'aec85c1d-1f1e-4b64-a314-bdc48fd56f6d', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (47, N'586ddbf4-edd0-411b-bb19-c5e0671c33f9', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (48, N'bfe2556f-70d4-42e8-ac6e-519b2f3455e9', 0, 1)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (49, N'ecfe5dd7-e091-42e2-86dd-e25d22fa1768', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (50, N'759fc453-b5fd-432a-a943-d75f65239f85', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (51, N'459d20ce-a550-4478-a1ac-36835f7722ba', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (52, N'fbfa3b4d-0894-4f45-b5e6-de5757354ca9', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (53, N'02c157c7-192b-4e5c-92e4-f88b6c261262', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (54, N'eb5d7a82-3f45-4666-8b97-43cce2fada3f', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (55, N'3ed9e6b8-0d8b-4c58-b38e-7702a5674509', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (56, N'5edba6cc-c856-418f-a737-f906f6ac8263', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (57, N'948bd698-8f80-413e-8acc-048628b59ac8', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (58, N'0c1c9dcf-4470-48cc-bc26-07f6eb4e499d', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (59, N'91361c59-f866-4a9a-80df-20227af39b21', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (60, N'565de752-bc1c-4b36-b4e9-955c47618269', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (61, N'f9f8c54f-26cd-4300-98da-cc0fb9a8cbfa', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (62, N'f846caaf-cd79-4ca0-bc8a-07eed6f9af76', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (63, N'dfa67897-e9bf-4e70-98d3-d8eece50f1c2', 0, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (64, N'292c0c3e-6882-45f1-a00a-c5c9beadf22b', 1, 2)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (65, N'ebbbfc14-9cd3-4e69-b8f7-a53b81725c65', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (66, N'c51416e9-027f-43c9-bb3d-a550c846ffbc', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (67, N'bfcbb771-6234-4a85-9304-0e589029bd69', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (68, N'a982f31b-8316-4017-8807-86183e536733', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (69, N'b6371bd2-420d-4b17-a85c-0457c4367e8c', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (70, N'1bd33b61-af90-4ca4-9e3d-dce50c2904bf', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (71, N'983016d7-d4cc-4fc2-9e4a-5aa9fdd147a9', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (72, N'4e4940d8-c29e-4dcb-9eb2-54ad70e1a63a', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (73, N'554ce2c0-4fa2-4dec-b882-021b61cb6562', 0, 4)
INSERT [dbo].[KeyCode] ([KeyCodeID], [KeyCode], [State], [CompanyManNum]) VALUES (74, N'65ce5051-b7e6-4167-b6b3-bc604fdf5802', 0, 4)
SET IDENTITY_INSERT [dbo].[KeyCode] OFF
/****** Object:  UserDefinedFunction [dbo].[fn_splitStrs]    Script Date: 07/09/2018 16:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_splitStrs](
	@s   varchar(8000),   --待分拆的字符串
	@split varchar(10),     --数据分隔符
	@splitIndex int
)RETURNS nvarchar(100)
AS
BEGIN
 DECLARE @splitlen nvarchar(50)
 DECLARE @retrnstr nvarchar(100)
 DECLARE @whileIndex INT=0
 SET @splitlen=LEN(@split+'a')-2
 WHILE CHARINDEX(@split,@s)>0
 BEGIN
  SET @whileIndex=@whileIndex+1
  SET @retrnstr=LEFT(@s,CHARINDEX(@split,@s)-1)
  IF(@whileIndex=@splitIndex)
	BREAK;
  SET @s=STUFF(@s,1,CHARINDEX(@split,@s)+@splitlen,'')
 END
 IF(CHARINDEX(@split,@s)=0) SET @retrnstr=@s
 RETURN @retrnstr
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_splitStr]    Script Date: 07/09/2018 16:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_splitStr](
@s   varchar(8000),   --待分拆的字符串
@split varchar(10)     --数据分隔符
)RETURNS @t TABLE(ModulCode nvarchar(50))
AS
BEGIN
 DECLARE @splitlen nvarchar(50)
 SET @splitlen=LEN(@split+'a')-2
 WHILE CHARINDEX(@split,@s)>0
 BEGIN
  INSERT @t VALUES(LEFT(@s,CHARINDEX(@split,@s)-1))
  SET @s=STUFF(@s,1,CHARINDEX(@split,@s)+@splitlen,'')
 END
 INSERT @t VALUES(@s)
 RETURN
END
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmpID] [varchar](20) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Sex] [int] NULL,
	[Birth] [datetime] NULL,
	[JoinDate] [datetime] NULL,
	[EmpTel] [varchar](20) NULL,
	[EmpEmail] [varchar](50) NULL,
	[IsOut] [int] NULL,
	[SpellJX] [varchar](10) NULL,
	[SpellQP] [varchar](50) NULL,
	[EmpPassword] [nvarchar](50) NULL,
	[DepartID] [int] NULL,
	[PositionID] [int] NULL,
	[CompanyID] [nvarchar](50) NOT NULL,
	[RoleID] [varchar](200) NULL,
	[EmpPhotos] [nvarchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'S017090106', N'铃', 0, NULL, NULL, N'13972358224', NULL, 0, N'L', N'ling', NULL, 1, NULL, N'wx512ad5972960e003', N'4458867A-B356-4780-846F-916E3718EB63', N'http://p.qlogo.cn/bizmail/VMHuTGYXCVEqD3f9iaxFrkLDzuNKCib7ibGNtXCoYOa9MGEgTNbZy3iatQ/0')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'sp001', N'甘武华', 1, NULL, NULL, N'', N'', 0, N'GWH', N'ganwuhua', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'sp002', N'sp002', 1, NULL, NULL, N'', N'', 0, N'SP002', N'sp002', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'zy', N'张玉', 2, NULL, NULL, N'', N'', 0, N'ZY', N'zhangyu', NULL, 1, NULL, N'wx512ad5972960e003', N'4458867A-B356-4780-846F-916E3718EB63', N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'll', N'刘乐', 1, NULL, NULL, N'', N'', 0, N'LL', N'liule', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'jyz', N'朱亚哲', 1, NULL, NULL, N'', N'', 0, N'ZYZ', N'zhuyazhe', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'555333', N'2121', 0, NULL, NULL, N'', N'', 0, N'2121', N'2121', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'zs', N'张三', 0, NULL, NULL, N'', N'', 0, N'ZS', N'zhangsan', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'lisi', N'李四', 0, NULL, NULL, N'', N'', 0, N'LS', N'lisi', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'dw', N'段文', 1, NULL, NULL, N'', N'', 0, N'DW', N'duanwen', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'sa', N'刘乐333', 1, NULL, NULL, N'', N'', 0, N'LL333', N'liule333', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'ww', N'王五', 0, NULL, NULL, N'', N'', 0, N'WW', N'wangwu', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'admin', N'admin', 0, NULL, NULL, N'', N'', 0, N'ADMIN', N'admin', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'cq', N'陈琦', 1, NULL, NULL, N'', N'', 0, N'CQ', N'chenqi', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'lzc', N'刘志成', 1, NULL, NULL, N'', N'', 0, N'LZC', N'liuzhicheng', NULL, 9, NULL, N'wx512ad5972960e003', N'8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0', N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'sp003', N'瞿乐', 1, NULL, NULL, N'', N'', 0, N'QL', N'qule', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'lyq', N'赖玉清', 1, NULL, NULL, N'', N'', 0, N'LYQ', N'laiyuqing', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'xqf', N'徐奇峰', 1, NULL, NULL, N'', N'', 0, N'XQF', N'xuqifeng', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'lrj', N'黎日建', 1, NULL, NULL, N'', N'', 0, N'LRJ', N'lirijian', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'S017072801', N'朱考惠', 2, NULL, NULL, N'', N'', 0, N'ZKH', N'zhukaohui', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'fzslai', N'熊琨', 1, NULL, NULL, N'', N'', 0, N'XK', N'xiongkun', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'zhangsasssn', N'张三', 0, NULL, NULL, N'', N'', 0, N'ZS', N'zhangsan', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
INSERT [dbo].[Employee] ([EmpID], [Name], [Sex], [Birth], [JoinDate], [EmpTel], [EmpEmail], [IsOut], [SpellJX], [SpellQP], [EmpPassword], [DepartID], [PositionID], [CompanyID], [RoleID], [EmpPhotos]) VALUES (N'sss', N'测试', 1, NULL, NULL, N'', N'', 0, N'CS', N'ceshi', NULL, 1, NULL, N'wx512ad5972960e003', NULL, N'https://rescdn.qqmail.com/node/wwmng/wwmng/style/images/independent/DefaultAvatar$73ba92b5.png')
/****** Object:  Table [dbo].[Depart]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Depart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DepartID] [int] NOT NULL,
	[DepartName] [nvarchar](50) NULL,
	[PID] [int] NULL,
	[DepartPrincipal] [varchar](50) NULL,
	[Remark] [nvarchar](50) NULL,
	[CompanyID] [nvarchar](50) NULL,
	[SpellJX] [varchar](50) NULL,
	[SpellQP] [varchar](50) NULL,
 CONSTRAINT [PK_Depart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Depart] ON
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (1, 1, N'讯鹏科技IT', 0, NULL, N'', N'wx512ad5972960e003', N'XPKJIT', N'xunpengkejiIT')
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (2, 2, N'项目部222', 1, NULL, N'', N'wx512ad5972960e003', N'XMB222', N'xiangmubu222')
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (3, 6, N'交互组', 2, NULL, N'', N'wx512ad5972960e003', N'JHZ', N'jiaohuzu')
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (4, 7, N'产品组', 2, NULL, N'', N'wx512ad5972960e003', N'CPZ', N'chanpinzu')
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (5, 8, N'技术部', 1, NULL, N'', N'wx512ad5972960e003', N'JSB', N'jishubu')
INSERT [dbo].[Depart] ([id], [DepartID], [DepartName], [PID], [DepartPrincipal], [Remark], [CompanyID], [SpellJX], [SpellQP]) VALUES (6, 9, N'研发组', 8, NULL, N'', N'wx512ad5972960e003', N'YFZ', N'yanfazu')
SET IDENTITY_INSERT [dbo].[Depart] OFF
/****** Object:  Table [dbo].[Company]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyPrincipal] [nvarchar](50) NULL,
	[CompanyPhone] [nvarchar](20) NULL,
	[CompanyEmail] [nvarchar](50) NULL,
	[CompanyLogo] [nvarchar](500) NULL,
	[IsTongguo] [int] NULL,
	[endDate] [datetime] NULL,
	[beginDate] [datetime] NULL,
	[Longcode] [nvarchar](500) NULL,
	[attoken] [nvarchar](600) NULL,
	[expressYxq] [nvarchar](200) NULL,
 CONSTRAINT [PK_COMPANY] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Company] UNIQUE NONCLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'企业token，用于获取该企业下的信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'attoken'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'token有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'expressYxq'
GO
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyPrincipal], [CompanyPhone], [CompanyEmail], [CompanyLogo], [IsTongguo], [endDate], [beginDate], [Longcode], [attoken], [expressYxq]) VALUES (N'wx512ad5972960e003', N'深圳市讯鹏科技有限公司', N'S017090106', NULL, NULL, N'http://p.qlogo.cn/bizmail/0XvWBvgsaz3tsL1ia8ETZicjmtCxu6MF5tQONFS5jVnnk4ibt83eYEX3A/0', 0, CAST(0x0000A91C0127619A AS DateTime), CAST(0x0000A9150127619A AS DateTime), N'QCGMtrSaWWAH4sOB8oVv--dDwvwIpuYwFZEjVnTgEcA', N'ibhql6o854KIpAbRyROnrWoUbIizkMVqQF6BP9pvUSkBJ5y1UNatQdmxE2fg3Ew4unIodQD2NWw7Iz5JsxqAEuMK1Yy8VAeitv-gKiOlfjmt-31Cp481m52mJtzrsjMcEwy2b1SRBt5OhyseEqESleM20SBVBc5Z9-tNDsu1xqd8f-hsmbl1KK7iRQSGHfBRwrBFbsOe4mXsf0OOr-ZqCA', N'2018-07-06 19:55:28')
/****** Object:  Table [dbo].[Position]    Script Date: 07/09/2018 16:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [nvarchar](20) NULL,
	[Remark] [nvarchar](200) NULL,
	[DepartID] [int] NULL,
	[CompanyId] [nvarchar](50) NULL,
 CONSTRAINT [PK_POSITION] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PayEmpBonus]    Script Date: 07/09/2018 16:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PayEmpBonus]
AS
SELECT     TOP (100) PERCENT dbo.BonusItem.BIName, dbo.BonusData2.DisMan, dbo.BonusData2.EarMan, dbo.Employee.Name, dbo.BonusData2.EarMoney, dbo.BonusData2.DisDate, 
                      dbo.ResonDetial.ResonContent, dbo.BonusData2.BonusType, dbo.BonusData2.BonusDataID
FROM         dbo.BonusData2 INNER JOIN
                      dbo.ResonDetial ON dbo.BonusData2.ResonID = dbo.ResonDetial.ResonID INNER JOIN
                      dbo.Employee ON dbo.BonusData2.EarMan = dbo.Employee.EmpID INNER JOIN
                      dbo.BonusItem ON dbo.BonusData2.BonusItemID = dbo.BonusItem.BonusItemID
ORDER BY dbo.BonusData2.DisDate DESC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "BonusData2"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 221
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ResonDetial"
            Begin Extent = 
               Top = 0
               Left = 483
               Bottom = 105
               Right = 641
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 147
               Left = 225
               Bottom = 267
               Right = 380
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BonusItem"
            Begin Extent = 
               Top = 141
               Left = 591
               Bottom = 261
               Right = 742
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayEmpBonus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayEmpBonus'
GO
/****** Object:  View [dbo].[PayDepartBonus]    Script Date: 07/09/2018 16:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PayDepartBonus]
AS
SELECT     TOP (100) PERCENT dbo.BonusItem.BIName, dbo.BonusData2.DisMan, dbo.BonusData2.EarMan, dbo.Depart.DepartName AS EarDepart, dbo.BonusData2.EarMoney AS DisMoney, 
                      dbo.BonusData2.DisDate, dbo.ResonDetial.ResonContent, dbo.BonusData2.BonusType, dbo.BonusData2.BonusDataID
FROM         dbo.BonusData2 INNER JOIN
                      dbo.Depart ON dbo.BonusData2.EarMan = CONVERT(varchar, dbo.Depart.DepartID) INNER JOIN
                      dbo.BonusItem ON dbo.BonusData2.BonusItemID = dbo.BonusItem.BonusItemID INNER JOIN
                      dbo.ResonDetial ON dbo.BonusData2.ResonID = dbo.ResonDetial.ResonID
ORDER BY dbo.BonusData2.DisDate DESC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "BonusData2"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 270
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Depart"
            Begin Extent = 
               Top = 173
               Left = 403
               Bottom = 475
               Right = 564
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BonusItem"
            Begin Extent = 
               Top = 6
               Left = 427
               Bottom = 126
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResonDetial"
            Begin Extent = 
               Top = 53
               Left = 678
               Bottom = 158
               Right = 836
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayDepartBonus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayDepartBonus'
GO
/****** Object:  StoredProcedure [dbo].[chayue]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[chayue] 
(@userID varchar(50)='')
 AS 
  --exec chayue 'lzc'
DECLARE @TixianGeTime nvarchar(50)='1998-5-25'
SELECT @TixianGeTime=GetTime FROM dbo.AuditBonus WHERE GetUserID=@userID AND IsState!=2 ORDER BY AuditBonusID DESC
DECLARE @a DECIMAL(18,2)=0.00
DECLARE @b DECIMAL(18,2)=0.00

SELECT @a=ISNULL(SUM(EarMoney),0) FROM dbo.BonusData2 
WHERE BonusType=2 AND DisMan=@userID AND DisDate>
@TixianGeTime
PRINT @TixianGeTime  -- 交易给别人的钱


 
SELECT @b=ISNULL(SUM(EarMoney),0)

 FROM dbo.BonusData2 WHERE EarMan=@userID AND IsGet=0 AND BonusType!=0--我没有提现得到的总额

SELECT @b-@a AS EarMoney2 --余额
GO
/****** Object:  StoredProcedure [dbo].[departTIJIAO]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[departTIJIAO] 
	-- Add the parameters for the stored procedure here
	@depart VARCHAR(20),
	@allUserID VARCHAR(MAX),
	@sumMoney   INT,
	--@nowTime DATETIME,
	@comPanyID NVARCHAR(50)
AS
 BEGIN TRAN
DECLARE @erroSum INT
SET @erroSum=0
INSERT INTO dbo.AuditBonus
        ( GetUserID ,
         GetBonusID,
          GetMoney ,
           GetTime,
          IsState ,
          CompanyID,
          IsDepOrEmp
        )
VALUES  ( @depart , -- GetUserID - varchar(20)
          @allUserID,
          @sumMoney , -- GetMoney - decimal
          GETDATE() , 
          0,-- IsState - int
          @comPanyID,
          0
        )
SET @erroSum=@erroSum+@@ERROR
DECLARE @sql NVARCHAR(500)='UPDATE dbo.BonusData2 SET  IsGet=2 WHERE BonusDataID IN('+@allUserID+')' 
--PRINT @sql
EXEC(@sql)
SET @erroSum=@erroSum+@@ERROR
IF @erroSum<>0
BEGIN 
PRINT '失败，回滚事务'
ROLLBACK TRAN
end
ELSE
BEGIN
PRINT '提现成功'
COMMIT TRAN
END
GO
/****** Object:  StoredProcedure [dbo].[Reject]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Reject] 
	-- Add the parameters for the stored procedure here
	@tijiaoNum VARCHAR(20),
	@bd2ID VARCHAR(MAX),
	@userID VARCHAR(50)
 
AS
 BEGIN TRAN
DECLARE @erroSum INT
SET @erroSum=0
UPDATE dbo.AuditBonus SET IsState=2,ConfirmUserID=@userID,ConfirmTime=GETDATE() WHERE AuditBonusID=@tijiaoNum
SET @erroSum=@erroSum+@@ERROR
DECLARE @sql NVARCHAR(500)='UPDATE dbo.BonusData2 SET IsGet=0 WHERE BonusDataID IN ('+@bd2ID+')'  
--PRINT @sql
EXEC(@sql)
SET @erroSum=@erroSum+@@ERROR
IF @erroSum<>0
BEGIN 
PRINT '失败，回滚事务'
ROLLBACK TRAN
end
ELSE
BEGIN
PRINT '提现成功'
COMMIT TRAN
END
GO
/****** Object:  StoredProcedure [dbo].[Ratify]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Ratify] 
	-- Add the parameters for the stored procedure here
	@tijiaoNum VARCHAR(20),
	@bd2ID VARCHAR(MAX),
	@userID VARCHAR(50)
AS
 BEGIN TRAN
DECLARE @erroSum INT
SET @erroSum=0
UPDATE dbo.AuditBonus SET IsState=1,ConfirmUserID=@userID,ConfirmTime=GETDATE() WHERE AuditBonusID=@tijiaoNum
SET @erroSum=@erroSum+@@ERROR
DECLARE @sql NVARCHAR(500)='UPDATE dbo.BonusData2 SET IsGet=1 WHERE BonusDataID IN ('+@bd2ID+')'  
--PRINT @sql
EXEC(@sql)
SET @erroSum=@erroSum+@@ERROR
IF @erroSum<>0
BEGIN 
PRINT '失败，回滚事务'
ROLLBACK TRAN
end
ELSE
BEGIN
PRINT '提现成功'
COMMIT TRAN
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_Wo]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proc_Wo]
(
@strType nvarchar(50)
,@companyId nvarchar(50)=''
,@EmpId nvarchar(50)=''
,@EmpPhoto nvarchar(500)=''
,@EmpPhone nvarchar(20)=''
,@EmpEmail nvarchar(50)=''
,@qp nvarchar(20)=''
,@jp nvarchar(20)=''
,@sex int=0--性别  传进来的是1代表男  2代表女
,@rows int=0 output
)
as
if(@strType='IsValidComoany')--检查公司是否有效 exec Proc_Wo 'IsValidComoany','wx512ad5972960e003'
begin
select * from Company where CompanyID=@companyId and endDate>GETDATE()
end
else if(@strType='UpdateTouXiang') --更新用户的信息 
--exec Proc_Wo 'UpdateTouXiang','wx512ad5972960e003','lzc','http://p.qlogo.cn/bizmail/L
--V7s1GgXIxtjHhWicmibCnSOkYzhEFhHTCaib3jwGbRZu4mWmmTOnqicEw/0','18827507367','lzc@qq.com','liuzhicheng','LZC',1
--exec Proc_Wo 'UpdateTouXiang','wx512ad5972960e003','S017090106','a.jpg','13566664444','123@qq.com','zouyaling','jp',2
begin
declare @Err int=0;
SET XACT_ABORT ON ----语句产生运行时错误，则整个事务将终止并回滚。 
begin try
Begin Tran
declare @sqlSex int
declare @EmpTel nvarchar(20)
declare @sqlempEmail nvarchar(50)
--declare @Isout int
declare @jianpin nvarchar(20)
declare @quanpin nvarchar(50)
declare @sqlEmpPhoto nvarchar(500)
select @sqlSex=Sex,@EmpTel=EmpTel,@empEmail=EmpEmail
,@jianpin=SpellJX,@quanpin=SpellQP,@sqlEmpPhoto=EmpPhotos
from Employee where EmpID=@EmpId and CompanyID=@companyId
print '性别'
print @sqlsex
if((@sqlSex is null) or LEN(@sqlSex)=0)--性别没有时
begin
print '性别2'
if(@sex=1)
begin
set @sex=1
end
else
begin
set @sex=0
end
update Employee set Sex=@sex where EmpID=@EmpId and CompanyID=@companyId
end

if((@EmpTel is null) or len(@EmpTel)=0)--电话没有时
begin
update Employee set EmpTel=@EmpPhone where EmpID=@EmpId and CompanyID=@companyId
end

if((@sqlempEmail is null) or len(@sqlempEmail)=0)--邮箱没有时
begin
update Employee set EmpEmail=@EmpEmail where EmpID=@EmpId and CompanyID=@companyId
end

if((@jianpin is null) or len(@jianpin)=0)--简拼没有时
begin
update Employee set SpellJX=@jp where EmpID=@EmpId and CompanyID=@companyId
end

if((@quanpin is null) or LEN(@quanpin)=0)--全拼
begin
update Employee set SpellQP=@qp where EmpID=@EmpId and CompanyID=@companyId
end

if((@sqlEmpPhoto is null) or LEN(@sqlEmpPhoto)=0)--头像
begin
update Employee set EmpPhotos=@EmpPhoto where EmpID=@EmpId and CompanyID=@companyId
end
set @Err=@Err+ @@ERROR
if(@Err=0)
    begin
		set @rows=0--正确
		select @rows as row
		Commit Tran
	end
	else 
	begin
		set @rows=1--错误
		select @rows as row
		rollback Tran
	end
end try
begin catch
set @rows=1--错误
	select @rows as row
	select  ERROR_MESSAGE() as message
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[proc_Weixin]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_Weixin]
(
@strType nvarchar(50)
,@CompanyName nvarchar(50)=''--公司名
,@CompanyPrincipal nvarchar(50)=''--负责人id
,@CompanyLogo nvarchar(500)=''--公司logo
,@Longcode nvarchar(500)=''--永久授权码
,@coripId nvarchar(50)=''--公司coripId
,@Name nvarchar(50)=''--负责人名字
,@token nvarchar(500)=''--企业token
,@express_Yqx nvarchar(200)=''--企业token有效期
,@rows int=0 output
)
as
if(@strType='ZhuceCompany')--注册公司 exec proc_Weixin 'ZhuceCompany','讯鹏科技','S017090106','a.jpg','abcd','123'
begin
SET XACT_ABORT ON 
begin try
	Begin Tran
--插入公司负责人的角色
select * from Employee
select * from Authorities
declare @RoleIds varchar(200)
	set @RoleIds=newid()--4,5,6,7,8,9,10,12,13,14,15,16,18,21,22,23
	insert into Role values(@RoleIds,'管理员',0,@coripId)--插入角色
    --select top(1) RoleID=@RoleId from Role where RoleName=@RoleName--通过角色名，得到刚才插入的角色id
    declare @code2 int
    declare @b int
    set @b=1
    while(@b<=16)
    begin
		print @b
		
		
		
		print @b
		--select * from Authorities
		
		set @code2= dbo.fn_splitStrs('4,5,6,7,8,9,10,12,13,14,15,16,18,21,22,23',',',@b)
		--insert into Authorities values(@RoleName,(
		--select top(1) ModulCode 
		--from (select top(@b) * from dbo.fn_splitStr(@ModulCode,',')) as t order by ModulCode desc
		--),@AuthType)
		print @code2
		insert into Authorities values(@RoleIds,@code2,1)
		set @b=@b+1
    end
--插入这个人
insert into Employee (EmpID,Name,CompanyID,RoleID,IsOut)
values(@CompanyPrincipal,@Name,@coripId,@RoleIds,0)
--插入公司
select * from Company
insert into Company (CompanyName,CompanyPrincipal,CompanyLogo,Longcode,CompanyID,IsTongguo,endDate)
values(
@CompanyName,@CompanyPrincipal,@CompanyLogo,@Longcode,@coripId,0,dateadd(day,7,GETDATE())
)
declare @Err2 int=0
    set @Err2=@Err2+ @@ERROR
    
    if(@Err2=0)
    begin
		set @rows=0--正确
		select @rows as row
		Commit Tran
	end
	else 
	begin
		set @rows=1--错误
		select @rows as row
		rollback Tran
	end
end try
begin catch
	set @rows=1--错误
	select @rows as row
	select  ERROR_MESSAGE() as message
end catch
end
else if(@strType='UpdateAtoken')--更新企业token
-- exec proc_Weixin 'UpdateAtoken','','','','','wx512ad5972960e003','','qxvGPPK-2iZfMhJb3kCVqphfqYRtsHNY2X6Hnjv34YY8863YimcihZ0xjW-n2lXMdTkiUV9lRppWXN8Mm65hYpAkgRnudy5_-sJgZYV-ELaBLx11LGHpDjWltmgjd1g3Jw4qUZuAUZFim7wYHLnxHallLj6mmL-qXkmA6OI4cY2-3xIQUIiU952p_fHlsu-XpylVyhLlL8BynH61-6m2sg','7200'
begin
update Company set attoken=@token,expressYxq=@express_Yqx where CompanyID=@coripId

end
GO
/****** Object:  StoredProcedure [dbo].[proc_Tradding]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_Tradding]
(
@strType nvarchar(50),
@ComUserID nvarchar(50),--进来的userid
@GetPerson nvarchar(50)--得到钱的人
,@companyid nvarchar(50)--公司id
)
as
if(@strType='IsPersons')--看是否是本公司的人
begin
--declare @companyid int
--select @companyid=CompanyID from Employee where EmpID=@ComUserID
select * from Employee where EmpID=@GetPerson and CompanyID=@companyid
end
GO
/****** Object:  StoredProcedure [dbo].[proc_Suggection]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_Suggection]
(
@strType nvarchar(50),
@ResonContent nvarchar(200)='',--内容原因
@ResonImg nvarchar(500)='',--图片原因
@comType nvarchar(50)='',--联系方式
@comId nvarchar(50)='',--进来id
@ResonId varchar(200)='',--原因guid
@resonCount int=0,--原因个数
@compid nvarchar(50)='',
@rows int=0 output
)--exec proc_Suggection 'InsertSuggestion','uuuu','img,abc','14@qq.com','zy','123456',2
as
if(@strType='InsertSuggestion')--
begin
declare @Err int=0;
SET XACT_ABORT ON ----语句产生运行时错误，则整个事务将终止并回滚。 
begin try
Begin Tran
    set @Err=@Err+ @@ERROR
    set @ResonId=newid()
    select * from SuggectionComm
    insert into SuggectionComm values(@comId,@ResonId,@comType,GETDATE(),@compid)--提交反馈
    
    insert into SugReson values(@ResonId,@ResonContent,1)--文字原因
    declare @i int
    set @i=1
    print @resonCount
    declare @code nvarchar(50)
    if(@resonCount!=0)
    begin
		while(@i<=@resonCount)
		begin
		set @code=dbo.fn_splitStrs(@ResonImg,',',@i)
		print @code
		select * from SugReson
		insert into SugReson values(@ResonId,@code,2)--图片原因
		set @i=@i+1
		end
    end
    set @Err=@Err+ @@ERROR
    
    if(@Err=0)
    begin
		set @rows=0--正确
		select @rows as row
		Commit Tran
	end
	else 
	begin
		set @rows=1--错误
		select @rows as row
		rollback Tran
	end
end try
begin catch
	set @rows=1--错误
	select @rows as row
	select  ERROR_MESSAGE() as message
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[proc_SendBonus]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_SendBonus]
@strType nvarchar(50)
,@BonusItemId varchar(200)--奖金项id
,@DisMan varchar(50)--发奖金的人
,@EarMan varchar(200)--接收奖金的人
,@EarMoney decimal(10,2)--发奖金的金额
,@BonusType int --发奖金的类型0部门奖金1发放的个人奖金2交易
,@IsGet int
,@ResonID nvarchar(200)--原因id
,@PersonCount int --传进来的人的个数
,@ResonContent nvarchar(200)=''--原因文字内容
,@ResonContentImg nvarchar(200)=''--原因图片
,@ResonCount int=0--图片个数
,@ResonType int=0--原因类型2图片，1文字
,@BonusTypes int=1--奖金项类型，是负责的，还是授权的（授权的需要减两个地方）
,@CompanyId nvarchar(50)=''
,@rows int=0 output
as  
--exec proc_SendBonus 'SendBonus','ccc001','lzc','{BonusOut.EarMan}',{BonusOut.EarMoney},{BonusOut.BonusType},{BonusOut.IsGet},'{BonusOut.ResonID}',{BonusOut.PersonCount},'{BonusOut.ResonContent}','{BonusOut.ResonContentImg}',{BonusOut.ResonCount}
-- exec proc_SendBonus 'SendBonus','zyl','zy','cgx001,cnm001',20,0,0,'5abacf6f-8f8d-446f-afd7-afbcbc284e11',2,'777','',0,2

--exec proc_SendBonus 'SendBonus','35c849c4-50a3-4dad-9100-9517194a4660','lzc','dw',122,0,0,
--'39049eec-8058-4beb-90e7-9a3718b3417f',1,'坚持减肥见到你','/UploadImage/Sunpn-20180626152832735.jpg',1,0,0,d3g1MTJhZDU5NzI5NjBlMDAz
declare @Err int=0;
if(@strType='SendBonus')--发放奖金
begin
SET XACT_ABORT ON ----语句产生运行时错误，则整个事务将终止并回滚。 
begin try
Begin Tran
--select * from BonusData2
--插入原因
print 'aa'
    --select * from ResonDetial
    --将文字与图片分开插入
    declare @a int
    set @a=1
    --看有没有文字
    if(@ResonContent!='')
    begin
    insert into ResonDetial values(@ResonID,@ResonContent,1)--插入文字原因
    end
    while(@a<=@ResonCount)
    begin
    declare @ImgUrl nvarchar(50)
    --将图片地址全部分割出来，有几张图片就插入几条数据
    select top(1) @ImgUrl=ModulCode from 
    (select top(@a) * from dbo.fn_splitStr(@ResonContentImg,',') order by ModulCode) as t order by ModulCode desc
    insert into ResonDetial values(@ResonID,@ImgUrl,2)--插入图片原因
    set @a=@a+1
    end
declare @i int
    set @i=1
    while(@i<=@PersonCount)
    begin
    declare @code nvarchar(50)
    --得到奖金人的id
    select top(1) @code=ModulCode from (select top(@i) * from dbo.fn_splitStr(@EarMan,',') order by ModulCode) as t order by ModulCode desc
   --判断是部门还是个人
		declare @TypeCount2 int
		select @TypeCount2=COUNT(*) from Employee where EmpID=@code--当在人员表里查的到时，就证明是个人
		if(@TypeCount2>0)
		begin
			set @BonusType=1
		end
		else
		begin
			set @BonusType=0
		end
    --发放明细插入一条数据
    select * from BonusData2
    insert into BonusData2 (BonusItemID,DisMan,DisDate,EarMan,EarMoney,EarDate,BonusType,IsGet,ResonID,CompanyId)
    values(@BonusItemId,@DisMan,GETDATE(),@code,@EarMoney,GETDATE(),@BonusType,@IsGet,@ResonID,@CompanyId)
    --对应的发放人的余额减金额，注：默认减最开始的钱
    declare @Yue decimal(10,2)--余额
    declare @RemainMoney2 decimal(10,2)--查出来这条还剩多少钱
    select @RemainMoney2=RemainMoney from RuleData where RuleDataID =
    (select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
    set @Yue=@RemainMoney2-@EarMoney
    
    declare @Yueshouquan decimal(10,2)--被授权的余额
		declare @RemainMoneyshouquan decimal(10,2)--查出来这条还剩多少钱
		select top(1) @RemainMoneyshouquan=RemainMoney from BonusImpower where BonusItemID=@BonusItemId and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		set @Yueshouquan=@RemainMoneyshouquan-@EarMoney
    print @Yue
    print @RemainMoney2
    if(@Yue<=0)
    begin
		--余额不足
		print '余额不足'
		if(@BonusTypes=1)--奖金项的人
		begin
		--将这条余额里的钱给更新到0
		update RuleData set RemainMoney=0
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		--再将多余的钱更新到下一条数据中
		update RuleData set RemainMoney=(RemainMoney+@Yue)
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		end
		else--授权的
		begin
		update RuleData set RemainMoney=0
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		--再将多余的钱更新到下一条数据中
		update RuleData set RemainMoney=(RemainMoney+@Yue)
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		
		
		--看被授权的金额足不足
		if(@Yueshouquan<=0)
		begin
		update BonusImpower set RemainMoney=0
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		update BonusImpower set RemainMoney=(RemainMoney+@Yueshouquan)
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		end
		
		else --余额足
		begin
		
		update BonusImpower set RemainMoney=(RemainMoney-@EarMoney)
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId 
		and EmpID=@DisMan
		and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		
		end
		end
		--如果为被授权的人发的奖金，那么即要扣奖金项里的剩余金额，也需要扣授权表里的剩余金额
		
		select * from RuleData
    end
    else
    begin
    print '余额足'
    print @EarMoney
    if(@BonusTypes=1)--奖金项
    begin
    update RuleData set RemainMoney=(RemainMoney-@EarMoney)
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		
    end
    else
    begin
    --看被授权的金额足不足
		update RuleData set RemainMoney=(RemainMoney-@EarMoney)
		where RuleDataID =
		(select top(1) RuleDataID from RuleData where BonusItemID=@BonusItemId and EndDate>GETDATE() and RemainMoney>0)
		
		if(@Yueshouquan<=0)
		begin
		
		update BonusImpower set RemainMoney=0
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		update BonusImpower set RemainMoney=(RemainMoney+@Yueshouquan)
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		end
		
		else --余额足
		begin
		
		update BonusImpower set RemainMoney=(RemainMoney-@EarMoney)
		where ImpowerID=(
		select top(1) ImpowerID from BonusImpower where BonusItemID=@BonusItemId 
		and EmpID=@DisMan
		and MONTH(ImpowerDate)=(MONTH(GETDATE())) and RemainMoney>0
		)
		
		end
    end
		
		
		
    end
    print 'sss'
    set @i=@i+1
    end
set @Err=@Err+ @@ERROR
if(@Err=0)
    begin
		set @rows=0--正确
		select @rows as row
		Commit Tran
	end
	else 
	begin
		set @rows=1--错误
		select @rows as row
		rollback Tran
	end
end try
begin catch
set @rows=1--错误
	select @rows as row
	select  ERROR_MESSAGE() as message
end catch
end
else if(@strType='BonusFa')--罚奖金
begin
declare @Err2 int
SET XACT_ABORT ON ----语句产生运行时错误，则整个事务将终止并回滚。 
begin try
Begin Tran
--将文字与图片分开插入
    declare @b int
    set @b=1
    --看有没有文字
    if(@ResonContent!='')
    begin
    insert into ResonDetial values(@ResonID,@ResonContent,1)--插入文字原因
    end
    while(@b<=@ResonCount)
    begin
		declare @ImgUrl2 nvarchar(50)
		--将图片地址全部分割出来，有几张图片就插入几条数据
		select top(1) @ImgUrl2=ModulCode from 
		(select top(@b) * from dbo.fn_splitStr(@ResonContentImg,',') order by ModulCode) as t order by ModulCode desc
		insert into ResonDetial 
		values(@ResonID,@ImgUrl2,2)--插入图片原因
		set @b=@b+1
    end
    declare @d int
    set @d=1
    while(@d<=@PersonCount)
    begin
		declare @code2 nvarchar(50)
		--得到奖金人的id
		select top(1) @code2=ModulCode from (select top(@d) * from dbo.fn_splitStr(@EarMan,',') order by ModulCode) as t order by ModulCode desc
		--判断是部门还是个人
		declare @TypeCount int
		select @TypeCount=COUNT(*) from Employee where EmpID=@code2--当在人员表里查的到时，就证明是个人
		if(@TypeCount>0)
		begin
			set @BonusType=1
		end
		else
		begin
			set @BonusType=0
		end
		--发放明细插入一条数据
		insert into BonusData2 (BonusItemID,DisMan,DisDate,EarMan,EarMoney,EarDate,BonusType,IsGet,ResonID,CompanyId)
		values(@BonusItemId,@DisMan,GETDATE(),@code2,@EarMoney,GETDATE(),@BonusType,@IsGet,@ResonID,@CompanyId)
		set @d=@d+1
    end
        
set @Err2=@Err+ @@ERROR
if(@Err2=0)
    begin
		set @rows=0--正确
		select @rows as row
		Commit Tran
	end
	else 
	begin
		set @rows=1--错误
		select @rows as row
		rollback Tran
	end
end try
begin catch
set @rows=1--错误
	select @rows as row
	select  ERROR_MESSAGE() as message
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[proc_Role]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_Role]
(
@strType nvarchar(50),
@RoleId nvarchar(50)='',
@RoleName nvarchar(50)='',
@Roles nvarchar(50)='',--所属公司
@IsActive int=0,--是否有效
@ModulCode nvarchar(200)='', --exec proc_Role 'AddRole',0,'测试角色',1,0,'4,6',1,2
@AuthType int=1,
@rows int=0,--通过分割符传的长度
@rows2 int=0 output
)
as
declare @Err int=0;
if(@strType='SelectRole')
begin
SET XACT_ABORT ON ----语句产生运行时错误，则整个事务将终止并回滚。 
begin try
Begin Tran
    --INSERT INTO Role(RoleName, Roles,IsActive) VALUES(@RoleName,@Roles,@IsActive)
    update Role set RoleName=@RoleName,IsActive=@IsActive where RoleID=@RoleId  --跟新角色表的东西
    set @Err=@Err+ @@ERROR
    --删除之前的权限
    delete Authorities where RoleID=@RoleId
    --插入新的权限
    --declare @t TABLE(ModulCode INT)
    --set @t=(select * from dbo.fn_splitStr('1,2,3,4,56',','))--分割
    
    --select top(1) * from Authorities
    declare @i int
    set @i=1
    while(@i<=@rows)
    begin
    declare @code int
    set @code= dbo.fn_splitStrs(@ModulCode,',',@i)
    --select top(1) @code=ModulCode from (select top(@i) * from dbo.fn_splitStr(@ModulCode,',')) as t order by ModulCode desc
    insert into Authorities values(@RoleId,@code,@AuthType)
    set @i=@i+1
    end
    --select MAX(RoleID) from Role where RoleName=@RoleName
    set @Err=@Err+ @@ERROR
    
    if(@Err=0)
    begin
		set @rows2=0--正确
		select @rows2 as row
		Commit Tran
	end
	else 
	begin
		set @rows2=1--错误
		select @rows2 as row
		rollback Tran
	end
end try
begin catch
	set @rows2=1--错误
	select @rows2 as row
	select  ERROR_MESSAGE() as message
end catch

end

else if(@strType='DelRole')
begin
SET XACT_ABORT ON 
begin try
	Begin Tran
	delete Role where RoleID=@RoleId
	set @Err=@Err+ @@ERROR
	delete Authorities where RoleID in(@RoleId)
	set @Err=@Err+ @@ERROR
	if(@Err=0)
    begin
		set @rows2=0--正确
		select @rows2 as row
		Commit Tran
	end
	else 
	begin
		set @rows2=1--错误
		select @rows2 as row
		rollback Tran
	end
end try
begin catch
	set @rows2=1--错误
	select @rows2 as row
	select  ERROR_MESSAGE() as message
end catch
end

else if(@strType='AddRole')--添加角色  --exec proc_Role 'AddRole',0,'测试角色',1,0,'4,6',1,2
begin
SET XACT_ABORT ON 
begin try
	Begin Tran
	declare @RoleIds varchar(200)
	set @RoleIds=newid()

    insert into Role values(@RoleIds,@RoleName,@IsActive,@Roles)--插入角色
    --select top(1) RoleID=@RoleId from Role where RoleName=@RoleName--通过角色名，得到刚才插入的角色id
    declare @code2 int
    declare @b int
    set @b=1
    while(@b<=@rows)
    begin
		print @b
		
		print @ModulCode
		
		print @b
		--select * from Authorities
		set @code2= dbo.fn_splitStrs(@ModulCode,',',@b)
		--insert into Authorities values(@RoleName,(
		--select top(1) ModulCode 
		--from (select top(@b) * from dbo.fn_splitStr(@ModulCode,',')) as t order by ModulCode desc
		--),@AuthType)
		print @code2
		insert into Authorities values(@RoleIds,@code2,@AuthType)
		set @b=@b+1
    end
    --select MAX(RoleID) from Role where RoleName=@RoleName
    declare @Err2 int=0
    set @Err2=@Err2+ @@ERROR
    
    if(@Err2=0)
    begin
		set @rows2=0--正确
		select @rows2 as row
		Commit Tran
	end
	else 
	begin
		set @rows2=1--错误
		select @rows2 as row
		rollback Tran
	end
end try
begin catch
	set @rows2=1--错误
	select @rows2 as row
	select  ERROR_MESSAGE() as message
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[proc_Main]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_Main]
(
@strType nvarchar(50),
@EmpID varchar(50)=''
,@pid int=0
,@compid nvarchar(50)=''--公司id
)
as
if(@strType='SelectEmp')
begin
select * from Employee where EmpID=@EmpID and IsOut=0
end
else if(@strType='SelectEmpDetialWo') --我页面里显示的常用信息  exec proc_Main 'SelectEmpDetialWo','S017090106','','wx512ad5972960e004'
begin
--个人信息
SELECT e.RoleID,e.CompanyID,e.EmpEmail,e.EmpID,e.EmpPassword,e.EmpPhotos,e.EmpTel,e.IsOut,e.DepartID,e.PositionID,d.DepartName,p.PositionName,e.Name
 FROM dbo.Employee e left join Depart d on e.DepartID=d.DepartID left join Position p on e.PositionID=p.PositionID 
WHERE EmpID=@EmpID and e.CompanyID=@compid and d.CompanyID=@compid
--个人奖金
SELECT ISNULL((SELECT SUM(EarMoney) FROM dbo.BonusData2 WHERE CompanyId=@compid AND  BonusType !=0 AND EarMan=@EmpID),0)
                - ISNULL((SELECT  SUM(EarMoney) DisMoney FROM dbo.BonusData2 WHERE CompanyId=@compid AND BonusType = 2 AND DisMan = @EmpID), 0)
                - ISNULL((SELECT  SUM(GetMoney) AudMoney FROM dbo.AuditBonus WHERE IsState != 2 AND GetUserID = @EmpID AND  CompanyId = @compid),0)
end
else if(@strType='SelectAuthorFi') --exec proc_Main 'SelectAuthorFi','zy','','wx512ad5972960e003'   查父id
begin 
--通过userid得到权限id
declare @AuthorId nvarchar(200) 
select @AuthorId=r.RoleID from Employee e ,Role r 
where e.RoleID=r.RoleID and e.EmpID=@EmpID and r.CompanyID=@compid

--select * from Role

--select * from Authorities where RoleID='12FB4092-792A-4424-B406-6FB378736221'--权限详情

--得到父id信息
select * from Author where AuthorID in(
--通过所拥有的权限得到父id
select PID
 from Author 
where AuthorID in
(
--通过权限id得到所拥有的权限
select ModuleCode from Authorities where RoleID=@AuthorId
)
and IsValid=0
group by PID
)
and IsValid=0
----得到子id
--select * from Author 
--where AuthorID in(
--select a.ModuleCode from Authorities a where RoleID='12FB4092-792A-4424-B406-6FB378736221' 
--)

end

else if(@strType='SelectAuthorzi')  --exec proc_Main 'SelectAuthorzi','zy',1
begin
--得到子id
declare @AuthorId2 nvarchar(200) 
select @AuthorId2=r.RoleID from Employee e ,Role r 
where e.RoleID=r.RoleID and e.EmpID=@EmpID and r.CompanyID=@compid
select * from Author 
where AuthorID in(
select a.ModuleCode from Authorities a where RoleID=@AuthorId2 and PID=@pid
)
and IsValid=0
end
else if(@strType='SelectDepJIangjin')--部门负责人可以查看自己部门收到的奖金 exec proc_Main 'SelectDepJIangjin','lzc','','wx512ad5972960e003'
begin
declare @depId int=0
SELECT @depId=DepartID FROM dbo.Depart WHERE DepartPrincipal=@EmpID AND CompanyID=@compid
if(@depId!=0)
begin
SELECT DepartID,DepartName,ISNULL(EarMoney,'0.00') Earmoney,EarMan FROM  dbo.Depart d  LEFT JOIN 
(SELECT  SUM(EarMoney) EarMoney ,EarMan   FROM dbo.BonusData2 WHERE BonusType=0 AND IsGet=0 GROUP BY EarMan )
 b ON d.DepartID=b.EarMan  WHERE   DepartPrincipal=@EmpID  and CompanyID=@compid
end
end
GO
/****** Object:  StoredProcedure [dbo].[proc_KehuManager]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_KehuManager]
(
@strType nvarchar(50)
,@cid nvarchar(50)=''--公司id
,@endDate nvarchar(50)=''--有效期
,@IsTongguo int=1--是否通过
,@CompanyName nvarchar(50)=''--公司名
)
as
if(@strType='SelectKehu')--查询所有注册过公司的信息 exec proc_KehuManager 'SelectKehu'
begin
--得到所有注册公司信息
 select CASE WHEN endDate >=GETDATE() THEN '未过期'
WHEN endDate <GETDATE() THEN '已过期'
ELSE '其他' END as a,c.CompanyName,c.CompanyEmail,c.CompanyPhone,e.Name 
,c.beginDate,c.endDate,c.CompanyID
from Company c 
left join Employee e on c.CompanyPrincipal=e.EmpID 
order by c.CompanyID desc
--得到已过期的公司
select * from Company c 
left join Employee e on c.CompanyPrincipal=e.EmpID 
where c.endDate<GETDATE()
order by c.CompanyID desc

end
else if(@strType='SelectKehuBycid')--得到某个公司的信息 exec proc_KehuManager 'SelectKehuBycid',1
begin
select * from Company c 
left join Employee e on c.CompanyPrincipal=e.EmpID 
 where c.CompanyID in(@cid) 
end
else if(@strType='Shenghe')
begin
update Company set endDate=@endDate,IsTongguo=@IsTongguo where CompanyID=@cid
end
else if(@strType='ShousuoCompany')--模糊公司名搜索公司 exec proc_KehuManager 'ShousuoCompany','','','0','讯鹏'
begin
select 
CASE WHEN endDate >=GETDATE() THEN '未过期'
WHEN endDate <GETDATE() THEN '已过期'
ELSE '其他' END as a,c.CompanyName,c.CompanyEmail,c.CompanyPhone,e.Name 
,c.beginDate,c.endDate,c.CompanyID
 from Company c 
left join Employee e on c.CompanyPrincipal=e.EmpID 
 where c.CompanyName like '%'+@CompanyName+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[proc_ImpowerData]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[proc_ImpowerData]
@EmpID varchar(50) --被授权人id
AS
DECLARE @LJ DECIMAL(18,2) --累计金额
DECLARE @MonthLJ DECIMAL(18,2)--当月授权金额
DECLARE @JY DECIMAL(18,2)   --结余
BEGIN
SET @LJ=(SELECT SUM(AddMoney) AllMoney FROM dbo.BonusImpower WHERE EmpID=@EmpID AND Convert(varchar(30),getdate(),102)<ActiveDate);
SET @MonthLJ=(SELECT SUM(Addmoney) AllMoney FROM dbo.BonusImpower WHERE EmpID=@EmpID  AND ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0))
SET @JY=(SELECT SUM(RemainMoney) AllMoney FROM dbo.BonusImpower WHERE EmpID=@EmpID  AND Convert(varchar(30),getdate(),102)<ActiveDate);

SELECT @LJ AllMoney,@MonthLJ MonthMoney,@JY RemainMoney
END
GO
/****** Object:  StoredProcedure [dbo].[proc_DelRule]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[proc_DelRule]
    (
      @BIRuleID INT ,
      @BonusItemID NVARCHAR(50) ,
      @rows INT = 0 OUTPUT
    )
AS
    DECLARE @Err INT= 0;
    DECLARE @count INT= 0;
    BEGIN	
        SET XACT_ABORT ON 
        BEGIN TRY
            BEGIN TRAN
   
            UPDATE  dbo.BonusItem
            SET     BIRuleID = NULL
            WHERE   BonusItemID = @BonusItemID
                    AND BIRuleID = @BIRuleID
            SET @count = @@rowcount
            IF @count > 0
                BEGIN
                    DELETE  FROM dbo.BonusItemRule
                    WHERE   BIRuleID = @BIRuleID
                    SET @Err = @Err + @@ERROR
                END
            ELSE
                BEGIN
                    SET @Err = 1
                END 
            IF ( @Err = 0 )
                BEGIN    
                    SET @rows = 0
                    SELECT  @rows AS row
                    COMMIT TRAN
                END		
            ELSE
                BEGIN	
                    SET @rows = 1
                    SELECT  @rows AS row
                    ROLLBACK TRAN
                END
        END TRY
        BEGIN CATCH
            SET @rows = 1--错误
            SELECT  @rows AS row
            SELECT  ERROR_MESSAGE() AS message
        END CATCH
    END
GO
/****** Object:  StoredProcedure [dbo].[proc_ClearDate]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_ClearDate]
as
truncate table BonusData2
truncate table BonusImpower
truncate table BonusItem
truncate table BonusItemRule
truncate table BonusOutput
truncate table BonusParameter
truncate table BonusReson
truncate table Company
truncate table Depart
truncate table Employee
truncate table Position
truncate table ResonDetial
truncate table Role
truncate table RuleData
truncate table SuggectionComm
truncate table SugReson
truncate table AuditBonus
truncate table dbo.Authorities
--设置超级管理员的权限
insert into Role values('8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0','超级管理员',0,'wx512ad5972960e003')
declare @code2 int
    declare @b int
    set @b=1
    while(@b<=17)
    begin
		--select * from Authorities
		
		set @code2= dbo.fn_splitStrs('4,5,6,7,8,9,10,12,13,14,15,16,17,18,21,22,23',',',@b)
		--insert into Authorities values(@RoleName,(
		--select top(1) ModulCode 
		--from (select top(@b) * from dbo.fn_splitStr(@ModulCode,',')) as t order by ModulCode desc
		--),@AuthType)
		print @code2
		insert into Authorities values('8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0',@code2,1)
		set @b=@b+1
    end
GO
/****** Object:  StoredProcedure [dbo].[proc_BonusOutput]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_BonusOutput]
@strType nvarchar(50)
,@UserId nvarchar(20)=''--登陆进来的id
,@companyId nvarchar(50)=''--公司id
,@GetUser nvarchar(20)=''--比较的userid
,@zdStr nvarchar(50)=''--自动补全进来的
,@BonusItemID nvarchar(200)=''
as
if(@strType='SelectItem')--奖金项  exec proc_BonusOutput 'SelectItem','S017090106','wx512ad5972960e003'
begin

select * into #BonusItem2 from BonusItem with(nolock)
where BIPrincipal=@UserId and CompanyID=@companyId and BIState=0 and InDate!=0
--select * from BonusItem
select isnull(SUM(rd.RemainMoney),0) sumRemainMoney ,rd.BonusItemID,bi.BIName
from RuleData rd right join #BonusItem2 bi on rd.BonusItemID=bi.BonusItemID
where bi.BIState=0 and bi.InDate!=0 and rd.EndDate>GETDATE()
group by rd.BonusItemID,bi.BIName
--select isnull(SUM(rd.RemainMoney),0) sumRemainMoney ,rd.BonusItemID,bi.BIName
--from RuleData rd right join #BonusItem2 bi on rd.BonusItemID=bi.BonusItemID
--group by rd.BonusItemID,bi.BIName
--select * from BonusItemRule

--select * from RuleData

--看登陆进来的人是否被授权
select isnull(SUM(bi.RemainMoney),0) sumRemainMoney ,b.BonusItemID,b.BIName
from BonusImpower bi right join BonusItem b on bi.BonusItemID=b.BonusItemID
where bi.EmpID=@UserId and b.CompanyID=@companyId and MONTH(bi.ImpowerDate)=MONTH(GETDATE()) 
and b.InDate!=0 and b.BIState=0 and IsValid=0
group by b.BonusItemID,b.BIName
--看被授权的奖金项是否有效
--select b.BonusItemID,b.BIName,b.BIType,b.BIRuleID,bi.AddMoney,bi.ImpowerDate,bi.RemainMoney,bi.Model,bi.BIPID
--  from BonusImpower bi with(nolock) 
--right join BonusItem b on bi.BonusItemID=b.BonusItemID 
print '奖金参数'
print @companyId
select * from BonusParameter where CompanyId=@companyId and IsOrNo=1 --奖金参数

select * from BonusReson where BonusResonUserId=@UserId and companyId=@companyId
end
else if(@strType='SelectItems2')  --exec proc_BonusOutput 'SelectItems2','cq',1
begin
--看登陆进来的是否是某个奖金项的负责人
select * into #BonusItem3 from BonusItem with(nolock)
where BIPrincipal=@UserId and BIState=0 and InDate!=0
--select * from BonusItem
--得到奖金项的钱
select SUM(rd.RemainMoney) sumRemainMoney ,bi.BonusItemID,bi.BIName
from RuleData rd right join #BonusItem3 bi on rd.BonusItemID=bi.BonusItemID
group by bi.BonusItemID,bi.BIName
--得到被授权的钱
select SUM(bi.RemainMoney) sumRemainMoney ,b.BonusItemID,b.BIName
from BonusImpower bi right join BonusItem b on bi.BonusItemID=b.BonusItemID
where bi.EmpID=@UserId and MONTH(bi.ImpowerDate)=MONTH(GETDATE()) 
and b.InDate!=0 and b.BIState=0
group by b.BonusItemID,b.BIName
select * from BonusParameter where CompanyId=@companyId --奖金参数

select * from BonusReson where BonusResonUserId=@UserId and companyId=@companyId

end
else if(@strType='IsPeops')--看是否是同一家公司  exec proc_BonusOutput 'IsPeops','lzc','wx512ad5972960e004','ll','','DA959917-D589-44F3-8756-B6F0C05F16C4'
begin
--查看是公司级奖金还是部门级奖金
declare @depId3 int
set @depId3=0
select @depId3=BIDepID from BonusItem where BonusItemID=@BonusItemID
declare @UserName nvarchar(50)
declare @isEmps int
declare @IsOk int
set @IsOk=0
if(@depId3=0)--公司级
begin

select @IsOk=COUNT(*),@UserName=Name,@isEmps=1  from Employee where EmpID !=@UserId and EmpID in
(
select EmpID from Employee where EmpID=@GetUser and CompanyID=@companyId
)
group by Name
if(@IsOk<1)
begin
select @IsOk=COUNT(*),@UserName=DepartName,@isEmps=2 from Depart where CompanyID=@companyId and DepartID=@GetUser
group by DepartName
end
end
else--部门级
begin
--select @IsOk=COUNT(*),@UserName=Name from Employee where EmpID=@GetUser and CompanyID=@companyId and DepartID=@depId3
--group by Name
 with T as(
    select DepartID from Depart  where DepartID=@depId3
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID=@companyId
)
SELECT @IsOk=COUNT(*),@UserName=Name,@isEmps=1 FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID=@companyId  and EmpID=@GetUser
group by Name; 

end
select @IsOk as isok,@UserName UserName,@isEmps isEmp
----查登陆进来人的所属公司
--select @companyId=CompanyID from Employee where EmpID=@UserId
----根据公司来看是否有得到这个奖金的人
--declare @IsOk int
--declare @UserName nvarchar(50)
--set @IsOk=0
--select @IsOk=COUNT(*),@UserName=Name from Employee where EmpID=@GetUser and CompanyID=@companyId
--group by Name
--if(@IsOk<1)--不是人员
--begin
--select @IsOk=COUNT(*),@UserName=DepartName from Depart where CompanyID=@companyId and DepartID=@GetUser
--group by DepartName
--end
--select @IsOk as isok,@UserName UserName
end

else if(@strType='SelectUsers')--奖金发放页面时选中人员  exec proc_BonusOutput 'SelectUsers','zy',1
begin
select * from Depart 
where PID=0 and CompanyID=@companyId--查通一家公司的部门
select * from Employee 
where 
CompanyID=@companyId and IsOut!=1
and EmpID!=@UserId
--CompanyID=@companyId--查同一家公司的员工
end
else if(@strType='zidong') --exec proc_BonusOutput 'zidong','cq','wx512ad5972960e003','','x'
begin

select Name label,EmpID value from Employee 
where SpellJX like '%'+@zdStr+'%' and IsOut=0 and EmpID!=@UserId and CompanyID=@companyId
		union
		select DepartName label,CONVERT(varchar(50),DepartID)value from Depart 
		where CompanyID=@companyId and SpellJX like '%'+@zdStr+'%'
end
else if(@strType='gongshiOrbumen')--根据这个奖金项是公司或者部门来看到底可以发给那些人员
begin  --exec proc_BonusOutput 'gongshiOrbumen','lzc','wx512ad5972960e004','','','DA959917-D589-44F3-8756-B6F0C05F16C4'
declare @depId int
set @depId=0
select @depId=BIDepID from BonusItem where BonusItemID=@BonusItemID
print @depId
if(@depId=0)--公司级奖金
begin
select LEFT(SpellJX,1) py from Employee 
where CompanyID=@companyId group by LEFT(SpellJX,1)
union (
select LEFT(SpellJX,1) py from Depart where CompanyID=@companyId group by LEFT(SpellJX,1))
order by py
end
else
begin
print '部门级';
--select LEFT(SpellJX,1) py from Employee where CompanyID=@companyId and DepartID=@depId and EmpID!=@UserId  group by LEFT(SpellJX,1) order by py 
    with T as(
    select DepartID from Depart  where DepartID=@depId
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID=@companyId
)
SELECT LEFT(SpellJX,1) py FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID=@companyId  and EmpID!=@UserId
group by LEFT(SpellJX,1) order by py 
end
end
else if(@strType='IsShouyi') --自动补全哪里判断公司与部门级
begin--exec proc_BonusOutput 'IsShouyi','2','wx512ad5972960e003','','','8F0F6B97-084C-459D-AA2A-86044A1E01A2'
declare @depId2 int
set @depId2=0
select @depId2=BIDepID from BonusItem where BonusItemID=@BonusItemID
if(@depId2=0)
begin--公司级
declare @IsOk2 int
declare @UserName2 nvarchar(50)
declare @isEmp int
set @IsOk2=0
select @IsOk2=COUNT(*),@UserName2=Name,@isEmp=1 from Employee where EmpID=@UserId and CompanyID=@companyId
group by Name
print '人员'
if(@IsOk2<1)--不是人员
begin
select @IsOk2=COUNT(*),@UserName2=DepartName,@isEmp=2 from Depart where CompanyID=@companyId and DepartID=@UserId
group by DepartName
end
print @IsOk2


--select Name from Employee where EmpID=@UserId and CompanyID=@companyId
--union
--select DepartName from Depart where DepartID=@UserId and CompanyID=@companyId
end
else
begin--部门级
--select * from Employee where EmpID=@UserId and CompanyID=@companyId and DepartID=@depId2
    with T as(
    select DepartID from Depart  where DepartID=@depId2
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID=@companyId
)
SELECT @IsOk2=COUNT(*),@UserName2=Name,@isEmp=1 FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID=@companyId  and EmpID=@UserId
group by Name ; 
end
select @IsOk2 as isok,@UserName2 UserName,@isEmp isEmp
end
else if(@strType='IsShouyis')
begin
declare @depId5 int
set @depId5=0
select @depId5=BIDepID from BonusItem where BonusItemID=@BonusItemID
if(@depId5=0)
begin--公司级
select EmpID,Name,EmpPhotos,CompanyID, 1 isEmp
from Employee where CompanyID=@companyId and EmpID!=@UserId and SpellJX like @zdStr+'%'
union
select CONVERT(nvarchar(50),DepartID) EmpID,DepartName Name ,'' EmpPhoto,CompanyID,2 isEmp from Depart 
where CompanyID=@companyId and SpellJX like @zdStr+'%'
--select * from Employee where CompanyID=@companyId and EmpID!=@UserId and SpellJX like @zdStr+'%'
--select * from Depart where CompanyID='wx512ad5972960e003' and SpellJX like @zdStr+'%'
--declare @IsOk5 int
--declare @UserName5 nvarchar(50)
--set @IsOk5=0
--select EmpID,Name,EmpPhotos 
--from Employee where EmpID=@GetUser and CompanyID=@companyId and SpellJX like @zdStr+'%'
--if(@IsOk5<1)--不是人员
--begin
--select DepartID EmpID,DepartName Name,'' EmpPhotos 
--from Depart where CompanyID=@companyId and DepartID=@GetUser and SpellJX like @zdStr+'%'
--end
--print @IsOk5
--select @IsOk5 as isok,@UserName5 UserName

--select Name from Employee where EmpID=@UserId and CompanyID=@companyId
--union
--select DepartName from Depart where DepartID=@UserId and CompanyID=@companyId
end
else
begin--部门级
--select * from Employee where EmpID=@UserId and CompanyID=@companyId and DepartID=@depId2
    with T as(
    select DepartID from Depart  where DepartID=@depId5
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID=@companyId
)
SELECT 
EmpID,Name,EmpPhotos,CompanyID, 1 isEmp
 FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID=@companyId  and EmpID!=@UserId and LEFT(SpellJX,1) like @zdStr+'%'; 
end
end
GO
/****** Object:  StoredProcedure [dbo].[proc_BonusData]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[proc_BonusData]
    @BonusItemID VARCHAR(50) , --奖金项id
    @CompanyID VARCHAR(50)
AS
    DECLARE @BIName VARCHAR(50)--奖金项名称
    DECLARE @LJ DECIMAL(18, 2) --累计金额
    DECLARE @MonthLJ DECIMAL(18, 2)--当月获得的金额
    DECLARE @JY DECIMAL(18, 2)   --结余(要抛去你授权出去的金额)
    DECLARE @ImpMoney DECIMAL(18, 2)--当月授权出去的钱
    BEGIN
        SET @BIName = ( SELECT  BIName
                        FROM    dbo.BonusItem
                        WHERE   BonusItemID = @BonusItemID
                                AND CompanyID = @CompanyID
                      );
        IF @BIName IS NOT NULL
            BEGIN 
                SET @LJ = ( SELECT  SUM(Addmoney) AllMoney
                            FROM    dbo.RuleData
                            WHERE   BonusItemID = @BonusItemID
                                    AND CONVERT(VARCHAR(30), GETDATE(), 102) < EndDate
                          );
                SET @MonthLJ = ( SELECT SUM(Addmoney) AllMoney
                                 FROM   dbo.RuleData
                                 WHERE  BonusItemID = @BonusItemID
                                        AND JoinDate >= CONVERT(VARCHAR(30), DATEADD(MM,
                                                              DATEDIFF(MM, 0,
                                                              GETDATE()), 0), 102)
                               );

--判断当月有没有给别人授权过金额
                --IF EXISTS ( SELECT  Addmoney
                --            FROM    BonusImpower
                --            WHERE   BonusItemID = @BonusItemID
                --                    AND ImpowerDate >= CONVERT(VARCHAR(30), DATEADD(MM,
                --                                              DATEDIFF(MM, 0,
                --                                              GETDATE()), 0), 102) )
                --    BEGIN
                --        SET @JY = ( SELECT  SUM(RemainMoney) AllMoney
                --                    FROM    dbo.RuleData
                --                    WHERE   BonusItemID = @BonusItemID
                --                            AND CONVERT(VARCHAR(30), GETDATE(), 102) < EndDate
                --                  )
                --            - ( SELECT  SUM(Addmoney)
                --                FROM    BonusImpower
                --                WHERE   BonusItemID = @BonusItemID
                --                        AND ImpowerDate >= CONVERT(VARCHAR(30), DATEADD(MM,
                --                                              DATEDIFF(MM, 0,
                --                                              GETDATE()), 0), 102)
                --              );
                --    END
                --ELSE
                --    BEGIN 
                        SET @JY = ( SELECT  SUM(RemainMoney) AllMoney
                                    FROM    dbo.RuleData
                                    WHERE   BonusItemID = @BonusItemID
                                            AND CONVERT(VARCHAR(30), GETDATE(), 102) < EndDate
                                  );
                    --END 
            END
   
        SELECT  @BonusItemID BonusItemID,
                @BIName BIName ,
                @LJ AllMoney ,
                @MonthLJ MonthMoney ,
                @JY RemainMoney
    END
GO
/****** Object:  StoredProcedure [dbo].[proc_AddRuleData]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_AddRuleData]
as
begin
   --SELECT *FROM dbo.BonusItemRule
   --SELECT *FROM ruleData
   
    INSERT INTO dbo.RuleData
           ( BonusItemID ,
             AddMoney ,
             Remainmoney,
             JoinDate ,
             EndDate ,
             JoinType
           )
           SELECT BonusItem.BonusItemID,AvgMoney*GainNum,AvgMoney*GainNum,GETDATE(),CONVERT(varchar(7), dateadd(mm,Indate,getdate()) , 120) + '-01',1 FROM BonusItemRule LEFT JOIN  BonusItem ON BonusItem.BonusItemID=BonusItemRule.BonusItemID WHERE Indate IS NOT NULL AND Indate>0
   --SELECT BonusItemID,AvgMoney*GainNum,AvgMoney*GainNum,GETDATE(),DATEADD(month,ActiveDate,GETDATE()),1 FROM dbo.BonusItemRule JOIN dbo.BonusItem ON BonusItem.BIRuleID = BonusItemRule.BIRuleID
   
end
GO
/****** Object:  StoredProcedure [dbo].[proc_AddImpowerData]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[proc_AddImpowerData]
AS
    BEGIN
   --SELECT *FROM dbo.BonusItemRule
   --SELECT *FROM ruleData
   
        --INSERT  INTO dbo.BonusImpower
        --        ( BonusItemID ,
        --          BIPID ,
        --          EmpID ,
        --          AddMoney ,
        --          RemainMoney ,
        --          ImpowerDate ,
        --          EndDate ,
        --          Model ,
        --          IsValid,
        --          Remark
        --        )
                
        --        SELECT BonusItemID,BIPID,EmpID,AddMoney,RemainMoney,GETDATE(),
        --        (SELECT CONVERT(varchar(7), dateadd(mm,Indate,getdate()) , 120) + '-01' FROM BonusItem WHERE BonusItem.BonusItemID=A.BonusItemID AND InDate>0),
        --        0,0,'自动注入长期有效数据' FROM (
        --        SELECT  BonusItemID ,
        --                BIPID ,
        --                EmpID ,
        --                SUM(AddMoney) AddMoney,
        --                SUM(AddMoney) RemainMoney
        --        FROM    dbo.BonusImpower
        --        WHERE   Model = 0 AND IsValid = 0 AND EndDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)
        --        GROUP BY BonusItemID ,
        --                BIPID ,
        --                EmpID ,
        --                Model ) AS A
        INSERT  INTO dbo.BonusImpower
                ( BonusItemID ,
                  BIPID ,
                  EmpID ,
                  AddMoney ,
                  RemainMoney ,
                  ImpowerDate ,
                  Model ,
                  IsValid ,
                  Remark
                )
                SELECT  BonusImpower.BonusItemID ,
                        BIPID ,
                        EmpID ,
                        SUM(AddMoney) AddMoney ,
                        SUM(AddMoney) RemainMoney ,
                        GETDATE() ,
                        Model ,
                        IsValid ,
                        '自动注入长期有效数据'
                FROM    dbo.BonusImpower
                        LEFT JOIN BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID
                WHERE   Indate IS NOT NULL
                        AND Indate > 0--奖金项有效月份
                        AND IsValid = 0--被授权的金额是否有效
                        AND Model = 0--长期有效还是当月有效
                        AND ImpowerDate>=(SELECT CONVERT(varchar(7), dateadd(mm,-1,getdate()) , 120) + '-01')--统计上个月一共授权了多少钱
                GROUP BY BonusImpower.BonusItemID ,
                        BIPID ,
                        EmpID ,
                        Model ,
                        IsValid
    END
GO
/****** Object:  StoredProcedure [dbo].[aboutTIJIAO]    Script Date: 07/09/2018 16:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[aboutTIJIAO] 
	-- Add the parameters for the stored procedure here
	@userID VARCHAR(20),
	@allUserID VARCHAR(MAX),
	@sumMoney   INT,
	--@nowTime DATETIME,
	@comPanyID NVARCHAR(50)
AS
 BEGIN TRAN
DECLARE @erroSum INT
SET @erroSum=0
INSERT INTO dbo.AuditBonus
        ( GetUserID ,
         GetBonusID,
          GetMoney ,
           GetTime,
          IsState ,
          CompanyID,
          IsDepOrEmp
        )
VALUES  ( @userID , -- GetUserID - varchar(20)
          @allUserID,
          @sumMoney , -- GetMoney - decimal
          GETDATE() , 
          0,-- IsState - int
          @comPanyID,
          1
        )
SET @erroSum=@erroSum+@@ERROR
DECLARE @sql NVARCHAR(500)='UPDATE dbo.BonusData2 SET  IsGet=2 WHERE BonusDataID IN('+@allUserID+')' 
--PRINT @sql
EXEC(@sql)
SET @erroSum=@erroSum+@@ERROR
IF @erroSum<>0
BEGIN 
PRINT '失败，回滚事务'
ROLLBACK TRAN
end
ELSE
BEGIN
PRINT '提现成功'
COMMIT TRAN
END
GO
/****** Object:  Default [DF_Author_IsValid]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_IsValid]  DEFAULT ((0)) FOR [IsValid]
GO
/****** Object:  Default [DF_Author_Fanwei]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_Fanwei]  DEFAULT ((0)) FOR [Fanwei]
GO
/****** Object:  Default [DF_BonusItem_Indate]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[BonusItem] ADD  CONSTRAINT [DF_BonusItem_Indate]  DEFAULT ((1)) FOR [InDate]
GO
/****** Object:  Default [DF_Company_IsTongguo]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_IsTongguo]  DEFAULT ((1)) FOR [IsTongguo]
GO
/****** Object:  Default [DF_Company_beginDate]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_beginDate]  DEFAULT (getdate()) FOR [beginDate]
GO
/****** Object:  Default [DF_KeyCode_State]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[KeyCode] ADD  CONSTRAINT [DF_KeyCode_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_SuggectionComm_CommitTime]    Script Date: 07/09/2018 16:24:36 ******/
ALTER TABLE [dbo].[SuggectionComm] ADD  CONSTRAINT [DF_SuggectionComm_CommitTime]  DEFAULT (getdate()) FOR [CommitTime]
GO
