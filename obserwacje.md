prompt: Zbuduj minutnik w tym projekcie Avalonia. Ma odmierzać 25 minut pracy,
potem 5 minut przerwy, i liczyć skończone cykle. Obie długości mają być do
zmiany w oknie. Jeden przycisk start/pauza, drugi reset. Czas widoczny jako mm:ss, licznik cykli widoczny obok. Po skończonej pracy przerwa ma ruszyć
sama. Interfejs po polsku, nazwy w kodzie po angielsku, bez dodatkowych
bibliotek. Cała logika wprost w pliku MainWindow.axaml.cs, bez osobnych
klas pośredniczących. Użyj DispatcherTimer

odpowiedz: Sprawdzę obecny układ okna i konfigurację projektu, a następnie dodam minutnik bezpośrednio do MainWindow.axaml.cs i zweryfikuję kompilację.

Obecne okno jest tylko szablonem Avalonia. Dodaję polski interfejs z edytowalnymi długościami faz oraz powtarzającym się cyklem; licznik zwiększy się po zakończeniu przerwy.
Podczas edycji ścieżka z migawki okazała się nieaktualna: w katalogu roboczym widzę teraz projekt minutnik, a nie okno. Sprawdzę aktualne pliki i dopasuję zmiany do bieżącego projektu, nie nadpisując niczego w ciemno.


Logika minutnika jest już w pliku okna: pauza zachowuje pozostały czas, zmiana długości aktualnej fazy działa przy zatrzymanym timerze, a cykl nalicza się po przerwie. Teraz sprawdzam kompilację Avalonia i dopasowanie XAML do kodu.
Zbudowałem minutnik Pomodoro w aktualnym projekcie minutnik.

MainWindow.axaml zawiera polski interfejs, edytowalne długości pracy i przerwy, wyświetlanie czasu oraz licznik cykli.
MainWindow.axaml.cs zawiera całą logikę z użyciem DispatcherTimer. Przerwa rozpoczyna się automatycznie po pracy, cykl jest liczony po zakończeniu przerwy, a przyciski obsługują start/pauzę i reset.
Weryfikacja: dotnet build --no-restore zakończył się powodzeniem, bez ostrzeżeń i błędów.