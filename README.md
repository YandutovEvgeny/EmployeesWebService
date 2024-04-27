#Тестовое задание для компании SmartWay на позицию .Net backend разработчик
*Сервис умеет:
1. Добавлять сотрудников, в ответ должен приходить Id добавленного сотрудника.
2. Удалять сотрудников по Id.
3. Выводить список сотрудников для указанной компании. Все доступные поля.
4. Выводить список сотрудников для указанного отдела компании. Все доступные
поля.
5. Изменять сотрудника по его Id. Изменения должно быть только тех полей,
которые указаны в запросе.
Модель сотрудника:*
```json
{
  "Employee": {
    "Id": "1"
    "Name": "Евгений"
    "Surname": "Яндутов"
    "Phone": "+89763459021"
    "CompanyId": "111"
    "Passport":{
      "Id": "1"
      "Type": "Гражданский"
      "Number": "123-987"
    }
    "Department":{
      "Id": "1"
      "Name": "IT Департамент"
      "Phone": "+89045678932"
    }
  }
```
# C# 11 - .Net 8 - ASP.Net Core - MS SQL Server - Dapper