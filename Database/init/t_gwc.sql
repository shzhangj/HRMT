if exists (select 1
            from  sysindexes
           where  id    = object_id('t_gwc')
            and   name  = 'Relationship_8_FK'
            and   indid > 0
            and   indid < 255)
   drop index t_gwc.Relationship_8_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('t_gwc')
            and   name  = 'Relationship_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index t_gwc.Relationship_7_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('t_gwc')
            and   name  = 'Relationship_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index t_gwc.Relationship_6_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_gwc')
            and   type = 'U')
   drop table t_gwc
go

/*==============================================================*/
/* Table: t_gwc                                                 */
/*==============================================================*/
create table t_gwc (
   gwcid                int  identity(1,1)                not null,
   cpdnid               int                  not null,
   wldwid               int                  not null,
   cpzlid               int                  not null,
   FQtry                int                  not null,
   FPrice               decimal(13,2)        not null,
   FAmount              decimal(13,2)        not null,
   FData                datetime             null,
   Remark               varchar(300)         null,
   constraint PK_T_GWC primary key nonclustered (gwcid)
)
go

/*==============================================================*/
/* Index: Relationship_6_FK                                     */
/*==============================================================*/
create index Relationship_6_FK on t_gwc (
wldwid ASC
)
go

/*==============================================================*/
/* Index: Relationship_7_FK                                     */
/*==============================================================*/
create index Relationship_7_FK on t_gwc (
cpzlid ASC
)
go

/*==============================================================*/
/* Index: Relationship_8_FK                                     */
/*==============================================================*/
create index Relationship_8_FK on t_gwc (
cpdnid ASC
)
go

/*
   gwcid 主键ID
   cpdnid 产品ID
   wldwid 客户ID
   cpzlid 产品中类ID
   FQtry  数量
   FPrice 单价
   FAmount 金额
   FData  日期
   Remark 备注
 */