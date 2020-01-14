# Przypadki testowe formularza edytowania produktu

## Dawid Burnat


**ID 1**

**Tytuł:** Poprawne zmiana nazwy produktu

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku "Edit" przycisku przy dowolnym produkcie

-Wprowadzenie nazwy produktu

-Kliknięcie przycisku "Submit"

**Oczekiwany wynik:** Zmieniona nazwa produktu 

**Warunki końcowe:** Przejście na stronę z listą wszystkich produktów wraz z wcześniej edytowanym

**Wartości wejściowe:** Poprawna nazwa (min. 3 znaki, max. 60 znaków), 

---

**ID 2**


**Tytuł:** Poprawna zmiana opisu produktu

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku "Edit" przycisku przy dowolnym produkcie

-Wprowadzenie opisu produktu

-Kliknięcie przycisku "Submit"

**Oczekiwany wynik:** Zmieniona nazwa opisu produktu 

**Warunki końcowe:** Przejście na stronę z listą wszystkich produktów wraz z wcześniej edytowanym

**Wartości wejściowe:** Poprawny opis(wymagany chociaż jeden znak), 

---

**ID 3**

**Tytuł:** Usunięcie opisu produktu

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku "Edit" przycisku przy dowolnym produkcie

-Usunięcie produktu

-Kliknięcie przycisku "Submit"

**Oczekiwany wynik:** Niezaakceptowana zmiana opisu 

**Warunki końcowe:** Wyświetlenie komunikatu z błędem

**Wartości wejściowe:** Brak
---

**ID 4**

**Tytuł:** Podanie ceny mniejszej od 0

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku "Edit" przycisku przy dowolnym produkcie

-Wprowadzenie ujemnej ceny

-Kliknięcie przycisku "Submit"

**Oczekiwany wynik:** Niezaakceptowana zmiana ceny

**Warunki końcowe:** Wyświetlenie komunikatu z błędem

**Wartości wejściowe:** Poprawna cena(liczba większa od 0)

---

**ID 5**

**Tytuł:** Wprowadzenie ceny jako tekst

**Warunek wstępny:** Zalogowany użytkownik, posiadający uprawnienia administratora

**Kroki:**

-Kliknięcie przycisku "Edit" przycisku przy dowolnym produkcie

-Wprowadzenie tekstu do pola ceny

-Kliknięcie przycisku "Submit"

**Oczekiwany wynik:** Niezaakceptowana zmiana ceny

**Warunki końcowe:** Wyświetlenie komunikatu z błędem

**Wartości wejściowe:** Poprawna cena(liczba większa od 0)
