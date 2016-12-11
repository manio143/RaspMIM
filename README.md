# RaspMIM

Zbudowaliśmy zestaw aplikacji wspólnie działających. Na RaspberryPi z GrovePi+ działa Windows IoT Core, a nim Background App, która mierzy dane z sensorów dźwięku, światła, temperatury i wilgotności, a następnie wysyła te dane przez HTTP do aplikacji webowej postawionej na Azurze. Aplikacja webowa WebAPI zbiera i akumuluje dane, oraz udostępnia informację czy znajdujemy się w przyjaznym środowisku, co urządzenie RPi pobiera i wyświetla odpowiedni komunikat na ekranie LCD. Do tego mamy dwie aplikacji mobilne służące jako interfejs użytkownika. Aplikacja UWP przedstawia najnowszy wpis oraz pozwala na tworzenie wykresów aktywności poszczególnych sensorów z ostatniego dnia. Aplikacja Android (Xamarin) również przedstawia najnowszy wpis oraz zawiera wykres agregujący dane względem częstości wystąpień w danym zakresie wartości.

# Instalacja
Potrzebujesz Visual Studio >=2015 wraz z Windows 10 SDK i narzędziami do tworzenia aplikacji uniwersalnych.

Na RaspberryPi należy najpierw zainstalować Windows 10 IoT Core oraz skonfigurować Visual Studio, aby móc zdeployować na RPi aplikację IoTBackgroundApp.

Należy w niej zmienić URL do serwera na którym uruchomione jest WebAPI (standardowa kompilacja ASP.NET).

RPi będzie serwować dane z sensorów, które trzeba podpiąć do odpowiednich pinów (patrz kod).

Następnie za pomocą aplikacji UWP i Android możemy połączyć się z serwerem WebAPI, aby monitorować nasze urządzenie.
