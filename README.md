## Programowanie ASP.Net sklep sportowy

### Uwaga!

Aby poprawnie uruchomić aplikację wymagane jest wpisanie poprawnego connection stringa do servera sql w pliku appsettings.Development.json lub appsettings.json (w zależności od uruchamianej wersji)

### Pokrycie testami:

---

Dominik Wantuch:
- ProductApiController- Moq + xUnit - pokrycie 100%
- ProductRepository- InMemoryDatabase + Moq + xUnit - pokrycie 100%
- CreateProductModel- xUnit - pokrycie 100%
- ProductModel - xUnit - pokrycie 100%

(wszystkie testy przechodzą z wynikiem pozytywnym)

---

Dawid Burnat:
- ManufacturerApiController - Moq + xUnit - pokrycie 100%
- ManufacturerController - Moq + xUnit - pokrycie 100%
- ManufacturerRepository - Moq + xUnit + InMemoryDatabase - pokrycie 100%

(wszystkie testy przechodzą poprawnie)

---


Szymon Domalik:
- AdminController - Moq + xUnit - pokrycie 100%
- ManufacturerModel - xUnit - pokrycie 100%
- UpdateManufacturerModel - xUnit - pokrycie 100%
---
