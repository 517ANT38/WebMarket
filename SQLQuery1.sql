create proc insert_safe
(
	@f int,
	@n nvarchar(50),
	@p nvarchar(50),
	@t int,
	@e money,
	@h nvarchar(50)

)
as
begin tran
if exists (select * from Корзина where email like @h and id=@f)
begin
UPDATE Корзина SET Количество=@t where email like @h and id=@f
end
else
begin
insert into Корзина (Id,Название,Описание,Количество,Стоимость_в_рублях_за_ед_товара,Email) values(@f,@n,@p,@t,@e,@h)
end
commit tran

