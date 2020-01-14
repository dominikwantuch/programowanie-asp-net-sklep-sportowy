## Programowanie ASP.NET sklep sportowy

### Uwaga!

**Projekt utworzony został przy użyciu frameworka _.NET Core 2.2_**

**Aby poprawnie uruchomić aplikację wymagane jest wpisanie poprawnego connection stringa do servera sql w pliku appsettings.Development.json lub appsettings.json (w zależności od uruchamianej wersji).**

**Uruchomienie testów selenium wymaga włączonej aplikacj!**

**Opisy przypadków testowych znajdują się w folderze _UserTestCases_ na gałęzi _master_.**

**UnitTesty znajdują się w projektach o nazwie w formacie [Imie]UnitTests**

**Testy Selenium znajdują się w projektak o nazwie w formacie [Imie]SeleniumTests**

### Pokrycie testami:

---

**Dominik Wantuch** (numer indeksu- 11190):

**Unit testy** (38 przypadków testowych):
- **ProductApiController**- Moq + xUnit - pokrycie 100%
- **ProductRepository**- InMemoryDatabase + Moq + xUnit - pokrycie 100%
- **CreateProductModel**- xUnit - pokrycie 100%
- **ProductModel** - xUnit - pokrycie 100%

**Testy Selenium** (5 przypadków testowych):
- testy panelu logowania- xUnit + Selenium

(wszystkie testy przechodzą z wynikiem pozytywnym)

---

Dawid Burnat:

**Unit testy** (43 przypadki testowe)
- **ManufacturerApiController** - Moq + xUnit - pokrycie 100%
- **ManufacturerController** - Moq + xUnit - pokrycie 100%
- **ManufacturerRepository** - Moq + xUnit + InMemoryDatabase - pokrycie 100%

(wszystkie testy przechodzą z wynikiem pozytywnym)

---


Szymon Domalik:

**UnitTesty** (14 przypdaków testowych):
- **AdminController** - Moq + xUnit - pokrycie 100%
- **ManufacturerModel** - xUnit - pokrycie 100%
- **UpdateManufacturerModel** - xUnit - pokrycie 100%

(wszystkie testy przechodzą z wynikiem pozytywnym)

---
