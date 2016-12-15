IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_new_Customer_BillQuery]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_new_Customer_BillQuery]
GO

CREATE Proc [dbo].[p_new_Customer_BillQuery] --EXEC p_new_Customer_BillQuery 14279,'2016-10-26','2016-11-25'
@CustomerID int,@Kssj DateTime,@jssj DateTime
AS
  DECLARE @qcje decimal(13,2),@bqfsje decimal(13,2),@bqskje decimal(13,2),@qmje decimal(13,2)
 SELECT @qcje =SUM(ISNULL(je,0)+ISNULL(yf,0)-ISNULL(tyf,0))-SUM(ISNULL(skje,0)) --�ڳ�
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime < @Kssj
  SELECT @bqfsje =SUM(ISNULL(je,0)+ISNULL(yf,0)-ISNULL(tyf,0)) --���ڷ�����
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime >= @Kssj AND trantime <= @jssj AND entityid in(9462,9316,9520,9340,9443)
  SELECT @bqskje =SUM(ISNULL(skje,0)) --�����տ�
 FROM cw_BillDetail WHERE CustomerID =@CustomerID AND trantime >= @Kssj AND trantime <= @jssj AND entityid in(9416,9429)
 select @qmje = @qcje + @bqfsje -@bqskje 
 
SELECT	@qcje AS InitialBalance,  --�ڳ�
		@bqfsje AS CurrentPeriodAmount,   --���ڷ�����
  		@bqskje CurrentPeriodOtherAmount,  --�����տ�
		@qmje AS CurrentPeriodBalance     --���� 
		
/*���ڷ�����ϸ*/
SELECT  cw_BillDetail.trantime AS TradingDay,--������
s_entity.ename AS BillType,--��������
cw_BillDetail.dzNumber AS BillNo,--���ݺ�
cw_BillDetail.xinghao AS ModelNo,--�ͺ�
cw_BillDetail.ph AS BatchNo,--����
c_jldw.ename AS Unit,--��λ
cw_BillDetail.sl AS Qty,--����
cw_BillDetail.dj AS Price,--����
cw_BillDetail.zk AS Discount,--�ۿ�
ISNULL(cw_BillDetail.yf,0)+ ISNULl(cw_BillDetail.je,0) - ISNULL(cw_BillDetail.tyf,0) AS Amount--���
FROM cw_BillDetail
	LEFT JOIN c_jldw on cw_BillDetail.jldwid = c_jldw.jldwid
	LEFT JOIN s_entity ON cw_BillDetail.entityid = s_entity.entityid
WHERE cw_BillDetail.CustomerID =@CustomerID AND  s_entity.entityid in(9462,9316,9520,9340,9443)
	AND trantime >= @Kssj AND trantime <= @jssj 
ORDER BY cw_BillDetail.trantime
/*�տ���ϸ*/
SELECT  cw_BillDetail.trantime AS TradingDay,--������
s_entity.ename AS BillType,--��������
cw_BillDetail.dzNumber AS BillNo,--���ݺ�
cw_BillDetail.skje AS Amount--���
FROM cw_BillDetail
 JOIN s_entity ON cw_BillDetail.entityid = s_entity.entityid
where  s_entity.entityid in(9416,9429) AND cw_BillDetail.CustomerID =@CustomerID 
AND trantime >= @Kssj AND trantime <= @jssj 
order by cw_BillDetail.trantime
GO


