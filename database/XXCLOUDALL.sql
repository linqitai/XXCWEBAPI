--分页获取法院表`存储过程
drop proc sp_getCourtInfByPage
create proc sp_getCourtInfByPage
@CName VARCHAR(50),
@pageSize int=3,
@pageIndex int=1,
@count int output
as
begin
select * from T_CourtInf
where CName like '%'+@CName+'%'
order by CId
offset ((@pageIndex-1)*@pageSize) rows
fetch next @pageSize rows only
set @count=(select COUNT(*) from T_CourtInf where CName like '%'+@CName+'%')
end

declare @ct int
exec sp_getCourtInfByPage @CName='',@pageSize=5,@pageIndex=1,@count=@ct output
print @ct

CId, CNumber, CName, CLinkman, CWorkTelephone, CAddress, CLongitude, CLatitude

select count(*) from T_VisitorAccessInf
select v.VFromCourtId,c.CName from T_VisitorAccessInf v left join T_CourtInf c on v.VFromCourtId = c.CNumber where v.VFromCourtId='10003'

--访客表分页获取 存储过程
drop proc sp_getVisitorAccessInfByPage1
create proc sp_getVisitorAccessInfByPage1
@VName VARCHAR(50),
@VCertificateNumber VARCHAR(50),
@VFromCourtId VARCHAR(50),
@pageSize int=3,
@pageIndex int=1,
@count int output
as
begin
select *,c.CName from T_VisitorAccessInf v 
left join T_CourtInf c 
on v.VFromCourtId = c.CNumber
where v.VName like '%'+@VName+'%' and v.VCertificateNumber like '%'+@VCertificateNumber+'%' and v.VFromCourtId like '%'+@VFromCourtId+'%'
order by v.VId
offset ((@pageIndex-1)*@pageSize) rows
fetch next @pageSize rows only
set @count=(select COUNT(*) from T_VisitorAccessInf where VName like '%'+@VName+'%' and VCertificateNumber like '%'+@VCertificateNumber+'%' and VFromCourtId like '%'+@VFromCourtId+'%')
end

declare @ct int
exec sp_getVisitorAccessInfByPage1 @VName='',@VCertificateNumber='',@VFromCourtId='',@pagesize=12,@pageindex=1,@count=@ct output
print @ct

--获取黑名单列表存储过程
drop proc sp_getBalcklistInfByPage1
create proc sp_getBalcklistInfByPage1
@BName VARCHAR(50),
@BCertificateNumber VARCHAR(50),
@BFromCourtId VARCHAR(50),
@pageSize int=3,
@pageIndex int=1,
@count int output
as
begin
select *,c.CName from T_BlacklistInf b
left join T_CourtInf c 
on b.BFromCourtId = c.CNumber
where b.BName like '%'+@BName+'%' and b.BCertificateNumber like '%'+@BCertificateNumber+'%' and b.BFromCourtId like '%'+@BFromCourtId+'%'
order by b.BId
offset ((@pageIndex-1)*@pageSize) rows
fetch next @pageSize rows only
set @count=(select COUNT(*) from T_BlacklistInf where BName like '%'+@BName+'%' and BCertificateNumber like '%'+@BCertificateNumber+'%' and BFromCourtId like '%'+@BFromCourtId+'%')
end

declare @ct int
exec sp_getBalcklistInfByPage1 @BName='',@BCertificateNumber='',@BFromCourtId='1',@pagesize=5,@pageindex=1,@count=@ct output
print @ct

--律师分页查询·存储过程
drop proc sp_getLawyerInfByPage1
create proc sp_getLawyerInfByPage1
@LName VARCHAR(50),
@LIdentityNumber VARCHAR(50),
@LFromCourtId VARCHAR(50),
@pageSize int=3,
@pageIndex int=1,
@count int output
as
begin
select *,c.CName from T_LawyerInf l
left join T_CourtInf c 
on l.LFromCourtId = c.CNumber
where l.LName like '%'+@LName+'%' and l.LIdentityNumber like '%'+@LIdentityNumber+'%' and l.LFromCourtId like '%'+@LFromCourtId+'%'
order by l.LId
offset ((@pageIndex-1)*@pageSize) rows
fetch next @pageSize rows only
set @count=(select COUNT(*) from T_LawyerInf where LName like '%'+@LName+'%' and LIdentityNumber like '%'+@LIdentityNumber+'%' and LFromCourtId like '%'+@LFromCourtId+'%')
end

declare @ct int
exec sp_getLawyerInfByPage1 @LName='',@LIdentityNumber='',@LFromCourtId='',@pagesize=5,@pageindex=1,@count=@ct output
print @ct

select
l.*,
c.CName,
Sex=case
	when l.LSex='1' then '男'
	when l.LSex='0' then '女'
	else '保密'
end
from T_LawyerInf l
left join T_CourtInf c 
on l.LFromCourtId = c.CNumber

--用户表
select * from T_UserInf

alter table T_UserInf
add constraint UQ_UserName unique (UserName)

create proc sp_getModel4Login
@UserName VARCHAR(50),
@UPassword VARCHAR(50),
@count int output
as
begin
	select * from T_UserInf
	where UserName=@UserName and UPassword=@UPassword
	set @count=(select COUNT(*) from T_UserInf where UserName=@UserName and UPassword=@UPassword)
end

declare @ct int
exec sp_getModel4Login @UserName='admin',@UPassword='qwer1234',@count=@ct output
print @ct


update T_BlacklistInf set BRemark='这里是一段备注文字'
update T_BlacklistInf set BCreateTime='2019-01-12 08:12:12'
update T_VisitorAccessInf set VInTime='2019-01-12 08:12:12'
update T_VisitorAccessInf set VOutTime='2019-01-12 08:12:12'
update T_VisitorAccessInf set VCertificateType='身份证'



update T_VisitorAccessInf set VCertificateNumber = '229292929292920' where VName='张三'

select v.VId,v.VName,v.VSex,v.VNation,v.VBirthDate,v.VAddress,v.VIssuingAuthority,v.VExpiryDate,v.VCertificateType,v.VCertificateNumber,v.VType,v.VFromCourtId,v.VInTime,v.VOutTime,v.VInPost,v.VOutPost,v.VInDoorkeeper,v.VOutDoorkeeper,v.VVisitingReason,v.VIntervieweeDept,v.VInterviewee,v.VOffice,v.VOfficePhone,v.VExtensionPhone,v.VMobilePhone,v.VRemark,c.CName from T_VisitorAccessInf v 
left join T_CourtInf c 
on v.VFromCourtId = c.CNumber
where v.VInTime between '2019-01-01 00:00:00' and '2019-06-01 23:59:59'
and v.VCertificateNumber='229292929292920'
order by v.VId desc

select top 2 v.VId,v.VName,v.VSex,v.VNation,v.VBirthDate,v.VAddress,v.VIssuingAuthority,v.VExpiryDate,v.VCertificateType,v.VCertificateNumber,v.VType,v.VFromCourtId,v.VInTime,v.VOutTime,v.VInPost,v.VOutPost,v.VInDoorkeeper,v.VOutDoorkeeper,v.VVisitingReason,v.VIntervieweeDept,v.VInterviewee,v.VOffice,v.VOfficePhone,v.VExtensionPhone,v.VMobilePhone,v.VRemark,c.CName from T_VisitorAccessInf v 
left join T_CourtInf c 
on v.VFromCourtId = c.CNumber
where v.VCertificateNumber='229292929292920'
order by v.VId desc


--根据时间和身份证号查询
select * from T_VisitorAccessInf
where VInTime between '2019-01-01 00:00:00' and '2019-06-01 23:59:59'
and VCertificateNumber='229292929292920'

--根据时间和身份证号查询（用户不选择时间怎么办？）
select * from T_VisitorAccessInf
where VInTime between '' and '' --<===这样？查不出任何东西的吧
and VCertificateNumber='229292929292920'

--是不是要做个判断来切换查询语句？比如:
--if(用户选择了时间和身份证号)
select * from T_VisitorAccessInf
where VInTime between '2019-01-01 00:00:00' and '2019-06-01 23:59:59'
and VCertificateNumber='229292929292920'
--else if(只选择了身份证号)
select * from T_VisitorAccessInf
where VCertificateNumber='229292929292920'


select v.VId,v.VInTime,c.CName
from T_VisitorAccessInf v
left join T_CourtInf c 
on v.VFromCourtId = c.CNumber
where v.VCertificateNumber = '229292929292920'
