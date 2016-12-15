if exists (select 1
            from  sysindexes
           where  id    = object_id('t_khshrlb')
            and   name  = 'Relationship_11_FK'
            and   indid > 0
            and   indid < 255)
   drop index t_khshrlb.Relationship_11_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('t_khshrlb')
            and   name  = 'Relationship_10_FK'
            and   indid > 0
            and   indid < 255)
   drop index t_khshrlb.Relationship_10_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('t_khshrlb')
            and   type = 'U')
   drop table t_khshrlb
go

/*==============================================================*/
/* Table: t_khshrlb                                             */
/*==============================================================*/
create table t_khshrlb (
   khshrlbID            int         identity(1,1)         not null,
   wldwid               int                  not null,
   wlxxdnid             int                  null,
   shAddr               varchar(255)                  null,
   shr                  varchar(30)          null,
   FTel                 varchar(30)          null,
   FMobile              varchar(30)          null,
   constraint PK_T_KHSHRLB primary key nonclustered (khshrlbID)
)
go

/*==============================================================*/
/* Index: Relationship_10_FK                                    */
/*==============================================================*/
create index Relationship_10_FK on t_khshrlb (
wldwid ASC
)
go

/*==============================================================*/
/* Index: Relationship_11_FK                                    */
/*==============================================================*/
create index Relationship_11_FK on t_khshrlb (
wlxxdnid ASC
)
go
