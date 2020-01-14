# Przypadki testowe formularza logowania

## Dominik Wantuch

**ID 1**

**Tytuł:** Poprawne logowanie do aplikacji

**Warunek wstępny:** Niezalogowany użytkownik, posiadający konto w aplikacji.

**Kroki:**

-Otwarcie formularza logowania.

-Wprowadzenie loginu.

-Wprowadzenie hasła.

-Kliknięcie przycisku “submit”.

**Oczekiwany wynik:** Zalogowanie się do aplikacji jako użytkownik.

**Warunki końcowe:** Zalogowany użytkownik. Widoczny ekran panelu administratora.

**Wartości wejściowe**: Prawidłowy login, prawidłowe hasło.

**ID 2**

**Tytuł:** Brak podanego loginu

**Warunek wstępny:** Niezalogowany użytkownik.

**Kroki:**

-Otwarcie formularza logowania.

-Wprowadzenie hasła.

-Kliknięcie przycisku “submit”.

**Oczekiwany wynik:** Wyświetlenie informacji proszącej o podanie loginu.

**Warunki końcowe:** Niezalogowany użytkownik. Widoczny ekran logowania wraz z prośbą o podanie loginu.

**Wartości wejściowe**: Dowolny ciąg znaków podany jako hasło.

**ID 3**

**Tytuł:** Brak podanego hasła

**Warunek wstępny:** Niezalogowany użytkownik.

**Kroki:**

-Otwarcie formularza logowania.

-Wprowadzenie loginu.

-Kliknięcie przycisku “submit”.

**Oczekiwany wynik:** Wyświetlenie informacji proszącej o podanie hasła.

**Warunki końcowe:** Niezalogowany użytkownik. Widoczny ekran logowania wraz z prośbą o podanie hasła.

**Wartości wejściowe**: Dowolny ciąg znaków podany jako login.

**ID 4**

**Tytuł:** Brak podanego loginu i hasła

**Warunek wstępny:** Niezalogowany użytkownik.

**Kroki:**

-Otwarcie formularza logowania.

-Kliknięcie przycisku “submit”.

**Oczekiwany wynik:** Wyświetlenie informacji proszącej o podanie loginu i hasła.

**Warunki końcowe:** Niezalogowany użytkownik. Widoczny ekran logowania wraz z prośbą o podanie loginu i hasła.

**Wartości wejściowe**: Brak

**ID 5**

**Tytuł:** Podanie błędnego loginu lub hasła

**Warunek wstępny:** Niezalogowany użytkownik.

**Kroki:**

-Otwarcie formularza logowania.

-Wprowadzenie loginu.

-Wprowadzenie hasła.

-Kliknięcie przycisku “submit”.

**Oczekiwany wynik:** Wyświetlenie informacji o błednie podanym loginie lub haśle.

**Warunki końcowe:** Niezalogowany użytkownik. Widoczny ekran logowania wraz z informacją o podaniu błędnego loginu lub hasła.

**Wartości wejściowe**: Błędny login i/lub hasło.

