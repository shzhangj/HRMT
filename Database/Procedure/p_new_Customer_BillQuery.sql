IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_new_Customer_BillQuery]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_new_Customer_BillQuery]
GO

CREATE Proc [dbo].[p_new_Customer_BillQuery] --EXEC p_new_Customer_BillQuery 14279,'2016-10-26','2016-11-25'
@CustomerID int,@Kssj DateTime,@jssj DateTime
AS
  DECLARE @qcje decimal(13,2),@bqfsje decimal(13,2),@bqskje decimal(13,2),@qmje decimal(13,2)
 SELECT @qcje =SUM(ISNULL(je,0)+ISNULL(yf,0)-ISNULL(tyf,0))-SUM(ISNULL(skje,0)) --期初
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime < @Kssj
  SELECT @bqfsje =SUM(ISNULL(je,0)+ISNULL(yf,0)-ISNULL(tyf,0)) --本期发生额
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime >= @Kssj AND trantime <= @jssj AND entityid in(9462,9316,9520,9340,9443)
  SELECT @bqskje =SUM(ISNULL(skje,0)) --本期收款
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime >= @Kssj AND trantime <= @jssj AND entityid in(9416,9429)
 select @qmje = @qcje + @bqfsje -@bqskje 
 
SELECT	@qcje AS InitialBalance,  --期初
		@bqfsje AS CurrentPeriodAmount,   --本期发生额
  		@bqskje CurrentPeriodOtherAmount,  --本期收款
		@qmje AS CurrentPeriodBalance     --结余 
		
/*本期发生明细*/
SELECT  cw_BillDetail.trantime AS TradingDay,--交易日
s_entity.ename AS BillType,--单据类型
cw_BillDetail.dzNumber AS BillNo,--单据号
cw_BillDetail.xinghao AS ModelNo,--型号
cw_BillDetail.ph AS BatchNo,--批号
c_jldw.ename AS Unit,--单位
cw_BillDetail.sl AS Qty,--数量
cw_BillDetail.dj AS Price,--单价
cw_BillDetail.zk AS Discount,--折扣
ISNULL(cw_BillDetail.yf,0)+ ISNULl(cw_BillDetail.je,0) - ISNULL(cw_BillDetail.tyf,0) AS Amount--金额
FROM cw_BillDetail
	LEFT JOIN c_jldw on cw_BillDetail.jldwid = c_jldw.jldwid
	LEFT JOIN s_entity ON cw_BillDetail.entityid = s_entity.entityid
WHERE cw_BillDetail.CustomerID =@CustomerID AND  s_entity.entityid in(9462,9316,9520,9340,9443)
	AND trantime >= @Kssj AND trantime <= @jssj 
ORDER BY cw_BillDetail.trantime
/*收款明细*/
SELECT  cw_BillDetail.trantime AS TradingDay,--交易日
s_entity.ename AS BillType,--单据类型
cw_BillDetail.dzNumber AS BillNo,--单据号
cw_BillDetail.skje AS Amount--金额
FROM cw_BillDetail
 JOIN s_entity ON cw_BillDetail.entityid = s_entity.entityid
where  s_entity.entityid in(9416,9429) AND cw_BillDetail.CustomerID =@CustomerID 
AND trantime >= @Kssj AND trantime <= @jssj 
order by cw_BillDetail.trantime
GO


