IF EXISTS (SELECT name FROM sysobjects WHERE name = N'p_Order_Query' AND type = 'P')
    DROP PROCEDURE dbo.p_Order_Query
GO

CREATE PROCEDURE dbo.p_Order_Query
	@CustomerID	int,				--客户ID
	@QueryKey varchar(50)
	
/*
	EXEC p_Order_Query
			@CustomerID = 'admin.1',
			@QueryKey = NULL
					
*/	
AS

SELECT DISTINCT TOP 100 t_khddmx.khddid INTO #ret
	FROM t_khdd
		JOIN t_khddmx ON t_khdd.khddid = t_khddmx.khddid
		JOIN c_cpdn on t_khddmx.cpdnid = c_cpdn.cpdnid
WHERE t_khdd.wldwid = @CustomerID
	AND (@QueryKey IS NULL 
		OR (t_khdd.ecode = @QueryKey
		OR c_cpdn.ecode = @QueryKey
		OR c_cpdn.ename = @QueryKey) )

SELECT 
	t_khdd.ecode, --订单号
	trantime,--日期
	t_khddmx.khddid,--订单ID 
	t_khddmx.khddmxID,--订单IDID 
	c_cpdn.ecode AS xinghao, --型号
	c_cpdn.ename as pinming,--品名
	t_khddmx.FQty,--数量
	t_khddmx.FPrice,--单价
	t_khddmx.zk,--折扣
	t_khddmx.FAmount,--金额
	CASE t_khddmx.xstatus WHEN 0 THEN '草稿'  WHEN 1 THEN '已受理'  WHEN 2 THEN '已排产'  WHEN 3 THEN '已开单'  WHEN 4 THEN '已出库'
  		WHEN 5 THEN '物流方式'   WHEN 6 THEN '已完成'  WHEN 7 THEN '已作废'  ELSE '' END  as  Status,
  	t_khddmx.yf AS DeliveryCharge--运费
FROM t_khdd
	JOIN t_khddmx ON t_khdd.khddid = t_khddmx.khddid
	JOIN c_cpdn on t_khddmx.cpdnid = c_cpdn.cpdnid
	JOIN #ret ON t_khdd.khddid = #ret.khddid
ORDER BY t_khdd.khddid DESC

GO
