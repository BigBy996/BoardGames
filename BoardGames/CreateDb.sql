CREATE TABLE [dbo].[Flower]
(
    [Id] INT IDENTITY (1, 1) NOT NULL, 
    [Kingdom] VARCHAR(MAX) NULL,
    [Clade] VARCHAR(MAX) NULL,
    [OtherClade] VARCHAR(MAX) NULL,
    [Order] VARCHAR(MAX) NULL,
    [Family] VARCHAR(MAX) NULL,
    [Tribe] VARCHAR(MAX) NULL,
    [Genus] VARCHAR(MAX) NULL,
    [CountReview] INT DEFAULT ((0)) NULL,
    [AverageMark] INT NULL,
    PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [dbo].[Review] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Author] VARCHAR (MAX) NULL,
    [FlowerId] INT NULL,
    [Text] VARCHAR (MAX) NULL,
    [LikeCount] INT DEFAULT ((0)) NULL,
    [ReportReason] VARCHAR (MAX) DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

create index i1 on Flower (Id);

create table t1000 (num int not null)

go
declare @i int
set @i = 1
while @i < 1001
  begin
    insert t1000 values(@i)
    set @i = @i + 1
  end
go

insert Flower (Kingdom, Clade, OtherClade, [Order], Family, Tribe, Genus)
select 'Kingdom ' + cast(id as varchar(10)), 'Clade ' + cast(id as varchar(10)),
       'OtherClade ' + cast(id as varchar(10)), 'Order ' + cast(id as varchar(10)),
       'Family ' + cast(id as varchar(10)), 'Tribe ' + cast(id as varchar(10)),
       'Genus ' + cast(id as varchar(10))
  from (
    select t1.num * t2.num as id, t1.num from 
      t1000 t1 cross join t1000 t2
  ) t
go