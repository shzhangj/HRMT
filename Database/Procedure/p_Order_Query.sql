IF EXISTS (SELECT name FROM sysobjects WHERE name = N'p_Order_Query' AND type = 'P')
    DROP PROCEDURE dbo.p_Order_Query
GO

CREATE PROCEDURE dbo.p_Order_Query
	@CustomerID	int,				--�ͻ�ID
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
	t_khdd.ecode, --������
	trantime,--����
	t_khddmx.khddid,--����ID 
	t_khddmx.khddmxID,--����IDID 
	c_cpdn.ecode AS xinghao, --�ͺ�
	c_cpdn.ename as pinming,--Ʒ��
	t_khddmx.FQty,--����
	t_khddmx.FPrice,--����
	t_khddmx.zk,--�ۿ�
	t_khddmx.FAmount,--���
	CASE t_khddmx.xstatus WHEN 0 THEN '�ݸ�'  WHEN 1 THEN '������'  WHEN 2 THEN '���Ų�'  WHEN 3 THEN '�ѿ���'  WHEN 4 THEN '�ѳ���'
  		WHEN 5 THEN '������ʽ'   WHEN 6 THEN '�����'  WHEN 7 THEN '������'  ELSE '' END  as  Status,
  	t_khddmx.yf AS DeliveryCharge--�˷�
FROM t_khdd
	JOIN t_khddmx ON t_khdd.khddid = t_khddmx.khddid
	JOIN c_cpdn on t_khddmx.cpdnid = c_cpdn.cpdnid
	JOIN #ret ON t_khdd.khddid = #ret.khddid
ORDER BY t_khdd.khddid DESC

GO
