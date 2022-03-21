# WypozyczalniaSamochodow

Aplikacja do zarzązania wypożyczalnią samochodów. Aplikacja została stworzona przeze mnie w ramach realizacji przedmiotu Aplikacje Internetowe II, w ramach studiów
inżynierskich na kieruku Informatyka.

Tworząc aplikacje zdecydowałem skorzystać z ASP.NET MVC – czyli platformy aplikacyjnej do budowy 
aplikacji internetowych opartych na wzorcu Model-View-Controller, opartej na technologii ASP.NET. Celem zadania zaliczeniowego było stworzenie aplikacji która ma łączyć się
z bazą MS SQL Server oraz pozwalać na wyświetlanie danych z wcześniej stworzonej bazy danych, a także edycję tych danych oraz dodawanie danych i usuwanie danych.
Stworzona aplikacja zawiera również elementy walidacji wprowadzonych danych oraz posiada menu.

## Kategoria użytkowników i funkcjonalności

Użytkownikami aplikacji w zamyśle mają być pracownicy wypożyczalni który będą zarządzać wypożyczalnią 
samochodów. Funkcje systemu dla pracownika:
- rejestracja do systemu- inaczej brak dostępu do aplikacji (hasło jest szyfrowane, a link 
aktywujący konto jest wysyłany na e-mail, możliwość zrestartowania hasła);
- logowanie do systemu (po zalogowaniu dostęp do poniższych funkcjonalości)
- zarządzanie pracownikiem (dodanie, edycja, szczegóły);
- zarządzanie wypożyczeniami (dodanie, edycja, szczegóły);
- zarządzanie klientem (dodanie, edycja, szczegóły);
- zarządzanie szczegółami wypożyczeń (dodanie, edycja, szczegóły); 
- zarządzanie samochodem (dodanie, edycja, szczegóły);
- zarządzanie kategoriami samochodów (dodanie, edycja, szczegóły);
- zarządzanie filami wypożyczalń w całym kraju (dodanie, edycja, szczegóły);
- możliwość filtracji rekordów poprzez wpisanie frazy;
- możliwość sortowanie rekordów w zakładkach.

## Opis modelu fizycznego

Model fizyczny oparty jest o diagram ERD, przedstawiający graficzne związki między encjami w bazie danych, o którą oparta jest stworzona aplikacja. W diagramie przedstawione następujące encje:
- klient - encja ma przedstawiać osobę która chce wypożyczyć samochód;
- wypozyczenie - encja przedstawiająca realizacje wypożyczenia, zawiera klucz główny zarówno 
klienta wypożyczającego jaki i samochód;
- kategoria- encja będąca kategorią samochodu- wiele samochodów może znajdować się w tej 
encji;
- samochod - encja przedstawia samochód do wypożyczenia;
- pracownik - encjaprzedstawiająca osobę pracującą w wypożyczalni która będzie mogła 
zarządzać systemem i realizować wypożyczenia;
- wypozyczalnia - encja ma utożsamiać fizyczną wypożyczalnie;
- User - tabela utożsamia konto użtkownika, jest powiązana z pracownikiem.

<p align="center">
![model](https://user-images.githubusercontent.com/56295726/159260647-66ed3acd-d094-44f4-99ae-aa133c8b17aa.PNG)
</p>
  
## Korzystanie z apliakcji

Projekt przedstawia aplikację stworzoną do zarządzania bazą danych. Aby przejść do aplikacji trzeba się 
zarejestrować, a następnie zalogować.
Aby dokonać rejestracji trzeba wypełnić formularz który jest walidowany (np. hasło i powtórzone hasło 
muszą być identyczne). Przy rejestracji trzeba również podać id pracownika wypożyczalni samochodów 
gdyż w założeniu aplikacja jest przeznaczona tylko dla pracowników. Po wypełnieniu formularza na 
podany adres e-mail zostanie wysłany link aktywujący konto. Aby aktywować konto należy wejść na 
pocztę za pomocą wysłanego linka przejść na stronę i zalogować się do już aktywnego konta. Warto 
zaznaczyć, że hasło podane przez użytkownika jest szyfrowane, tak aby nie było widoczne z poziomu 
bazy danych.
Po zalogowaniu ujrzymy widok z infomacjami na temat naszego konta, a także możliwość 
wylogowania. Główną funkcjonalością jest możliwość zarządzania bazą danych którą stworzyłem na 
potrzeby tej aplikacji- możliwość dodawania, edycji i wglądu w szczegóły jest dostępna tylko dla 
zalogowanych użytkowników. Dane do których ma dostęp użytkownik można filtrować za pomocą 
wpisania określonej frazy, a także sortować. W przyszłości baza będzie zawierała dużo więcej rekordów 
więc wyżej wymienione funkcji będą niezwykle przydatne. W aplikacji mamy również dostęp to mini 
galerii zdjęć w formie karuzeli.
