IF EXISTS (SELECT name FROM sysobjects WHERE name = N'p_new_Create_Order' AND type = 'P')
    DROP PROCEDURE dbo.p_new_Create_Order
GO

CREATE PROCEDURE dbo.p_new_Create_Order
	@gwcid	int,				--购物车明细
	@OrderNo varchar(50),		--订单号
	@Remark varchar(255),		--订单备注
	@khshrlbID int				--收货地址
	
/*
	EXEC bp_SelectValueCardByConditions
			@ic_CardNo = 'admin.1',
			@ii_CardAmountFrom = NULL,
			@ii_CardAmountTo = NULL,
			@ii_BalanceAmountFrom = NULL,
			@ii_BalanceAmountTo = NULL,
			@id_CreateTimeFrom = NULL,
			@id_CreateTimeTo = NULL,
			@ib_IsActive = NULL,
			@ic_Creator = NULL,
			@ic_MemberName = NULL,
			@ic_MemberIDCardNo = NULL
					
*/	
WITH ENCRYPTION 
AS

DECLARE @khddid int

INSERT INTO t_khdd(wldwid,wlxxdnid,shAddr,shr,FMobile,FTel,ecode,tranmemo,createby,createtime,
closestatus,estatus,xstatus,trantime)
SELECT a.wldwid,a.wlxxdnid,a.shAddr,a.shr,a.FMobile,a.FTel,@OrderNo,@Remark,@UserCode,Getdate(),
0,1,1,Getdate(),
FROM t_khshrlb a WHERE khshrlbID = @khshrlbID
SELECT @khddid = SCOPE_IDENTITY()

INSERT INTO t_khddmx(khddid,cpzlid,cpdnid,xh,Remark,FQty,FPrice,FAmount,xstatus)
SELECT @khddid,p.cpdnid,c.cpdnid,@SeqID,@Remark,@Qty,@Price,@Amount
FROM t_gwc c
	JOIN c_cpdn p ON c.cpdnid = p.cpdnid
	JOIN c_cpzl pt ON p.cpzlid = pt.cpzlid
WHERE c.gwcid = @gwcid


GO
