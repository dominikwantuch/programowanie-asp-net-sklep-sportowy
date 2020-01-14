# Przypadki testowe formularza dodawania nowego produktu

## Szymon Domalik


**ID 1**

**Tytuł:** Poprawne dodanie nowego produktu

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku w menu "Create"

-Kliknięcie przycisku "New Product"

-Wprowadzenie id producenta

-Wprowadzenie nazwy produktu

-Wprowadzenie opisu

-Wprowadzenie ceny

-Wprowadzenie kategorii

-Kliknięcie przycisku "submit"

**Oczekiwany wynik:** dodanie nowego produktu do bazy

**Warunki końcowe:** Przejście na stronę z listą wszystkich produktów wraz z wcześniej utworzonym

**Wartości wejściowe:** poprawne id producenta, poprawna nazwa (min. 3 znaki, max. 60 znaków), 

poprawny opis, poprawna cena (liczba większa od 0), poprawna kategoria

---

**ID 2**

**Tytuł:** Brak podanej nazwy produktu

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku w menu "Create"

-Kliknięcie przycisku "New Product"

-Wprowadzenie id producenta

-Wprowadzenie opisu

-Wprowadzenie ceny

-Wprowadzenie kategorii

-Kliknięcie przycisku "submit"

**Oczekiwany wynik:** wyświtlenie komunikatu nad polem nazwy

**Warunki końcowe:** Pozostanie na tej samej stronie. Widoczny komunikat "The Name field is required.".

**Wartości wejściowe:** poprawne id producenta, poprawny opis, poprawna cena (liczba większa od 0), poprawna kategoria

---

**ID 3**

**Tytuł:** Podanie ceny równej 0

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku w menu "Create"

-Kliknięcie przycisku "New Product"

-Wprowadzenie id producenta

-Wprowadzenie nazwy produktu

-Wprowadzenie opisu

-Wprowadzenie ceny 

-Wprowadzenie kategorii

-Kliknięcie przycisku "submit"

**Oczekiwany wynik:** wyświtlenie komunikatu nad polem ceny

**Warunki końcowe:** Pozostanie na tej samej stronie. Widoczny komunikat "Price must be grater tham 0".

**Wartości wejściowe:** poprawne id producenta,niepoprawna cena (liczba 0), poprawny opis, poprawna kategoria

---

**ID 4**

**Tytuł:** Brak podanych danych formularza

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku w menu "Create"

-Kliknięcie przycisku "New Product"

-Kliknięcie przycisku "submit"

**czekiwany wynik:** wyświtlenie komunikatu nad polem id producenta, nazwy, opisu, ceny i kategorii

**Warunki końcowe:** Pozostanie na tej samej stronie. Widoczne komunikaty nad każdym polem.

**Wartości wejściowe:** brak

