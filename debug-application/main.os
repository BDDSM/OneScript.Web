#Использовать "model"
#Использовать logos

Процедура ПриНачалеРаботыСистемы()

	Сообщить("Старт отладочного приложения через DOTNET WATCH - То есть с перекомпиляцией при изменении");
	Сообщить("Фактически - медленно но верно позволяет кодить как на ОднОскрипте, так и на C#");

	ИспользоватьСтатическиеФайлы();
	ИспользоватьСессии();
	ИспользоватьМаршруты("ОпределениеМаршрутов");

	ИспользоватьФоновыеЗадания();
	НастроитьФоновыеЗадания();

КонецПроцедуры

Процедура ОпределениеМаршрутов(КоллекцияМаршрутов)

	КоллекцияМаршрутов.Добавить("ГлавнаяСтраница", "{controller=home}/{action=index}");

КонецПроцедуры

Процедура НастроитьФоновыеЗадания()

	Раписание = Новый РасписаниеФоновыхЗаданий();
	Раписание.КаждуюМинуту();

	РегламентныеЗадания.СоздатьПериодическоеЗаданиеПоРасписанию(
		"СерверныеОповещения", "ОповеститьКлиентовОСтранном", Раписание
	);

КонецПроцедуры
