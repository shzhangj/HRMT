

/****** Object:  StoredProcedure [dbo].[p_new_CpKcQuery]    Script Date: 12/11/2016 11:51:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_new_CpKcQuery]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_new_CpKcQuery]
GO

/*
EXEC p_new_CpKcQuery 10092,'GT840304'
传入参数
@wldwid 客户ID
@cpCode 型号

返回结果	
cpdnid	        产品ID
c_cpdn_ecode	型号
c_cpdn_ename	品名
cpzlid	        版本ID
c_cpzl_ecode	版本号
c_cpzl_ename	版本名称
dj	            单价
bbRemark	    特殊说明
kcsl	        库存数
ydsl	        已预订数量
kcxssm	        查询显示

*/
CREATE PROC [dbo].[p_new_CpKcQuery]
@wldwid int,
@cpCode varchar(30)
AS
	CREATE TABLE #cpdn(cpdnid int,dj decimal(13,2),kcsl varchar(100),bbRemark varchar(30))
	INSERT INTO #cpdn(cpdnid,dj,bbRemark)
	SELECT c_wldwCpdnDj.cpdnid,c_wldwCpdnDj.dj,(CASE WHEN ISNULL(c_cpdn.wldwid,0)<> @wldwid THEN '该产品已报备' ELSE ''END)
	FROM c_wldwCpdnDj
	JOIN  c_cpdn ON c_cpdn.cpdnid = c_wldwCpdnDj.cpdnid
	WHERE c_wldwCpdnDj.wldwid =@wldwid AND c_cpdn.ecode = @cpCode  and c_cpdn.entitytype=1 and c_cpdn.closestatus ='0'
	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO #cpdn(cpdnid,dj,bbRemark)
		SELECT c_cpdn.cpdnid,isnull(c_wldwmx.dj,0),(CASE WHEN ISNULL(c_cpdn.wldwid,0)<> @wldwid THEN '该产品已报备' ELSE ''END)
		FROM c_cpdn
		JOIN c_wldwmx ON c_cpdn.cpzlid = c_wldwmx.cpzlid
		WHERE c_wldwmx.wldwid =@wldwid and c_cpdn.entitytype=1  AND c_cpdn.ecode = @cpCode  and c_cpdn.closestatus ='0'
		AND NOT EXISTS (SELECT 1 FROM #cpdn d WHERE c_cpdn.cpdnid = d.cpdnid)
	END

	DECLARE @kcStr varchar(200),@ydsl int,@kcxssm varchar(200)
	SELECT @kcStr = ISNULL(@kcStr,'') + Convert(varchar(20),Convert(float,i_kcye.sl-i_kcye.sddsl))+'+'
	from #cpdn cpdn
	JOIN i_kcye ON cpdn.cpdnid = i_kcye.cpdnid
	where i_kcye.sl > 0 AND EXISTS(SELECT 1 FROM t_WebSet_Ckdn WHERE i_kcye.ckdnid = i_kcye.ckdnid)
	
	IF ISNULL(@kcStr,'') <> ''
		SET @kcStr = LEFT(@kcStr,LEN(@kcStr)-1)
		
	SELECT @ydsl = SUM(t_khddmx.FQty) FROM #cpdn cpdn
	JOIN t_khddmx ON cpdn.cpdnid = t_khddmx.cpdnid 
	WHERE t_khddmx.xstatus= 0
	SELECT @kcxssm = ckxssm FROM t_WebSet
	 
	 
	SELECT c_cpdn.cpdnid,c_cpdn.ecode AS c_cpdn_ecode,c_cpdn.ename as c_cpdn_ename,
	c_cpzl.cpzlid,c_cpzl.ecode AS c_cpzl_ecode,c_cpzl.ename as c_cpzl_ename,cpdn.dj,cpdn.bbRemark,@kcStr AS kcsl,@ydsl as ydsl,@kcxssm as kcxssm
	FROM #cpdn cpdn
	JOIN c_cpdn on cpdn.cpdnid = c_cpdn.cpdnid
    JOIN c_cpzl on c_cpdn.cpzlid = c_cpzl.cpzlid
    
    
   
GO


