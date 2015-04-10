# Vyvoj GuidApp pro Windows Phone


### Vyvojove prostredi
Minimalni pozadavky
* Windows 8 Pro
* Visual Studio 2012 Express
* Windows Phone SDK 8.0
* [Vice informaci](https://msdn.microsoft.com/en-us/library/windows/apps/ff402529%28v=vs.105%29.aspx)

### Struktura aplikace
* veskere zdrojove soubory jsou ulozeny v Datasys SVN ```guidapp/trunk/windows/core ```
* v adresari ```GuidApp``` se nachazi zdrojove soubory vsech modulu, ktere jsou oddeleny v jednotlivych slozkach podle modulu
* vsechny moduly jsou zavisle na ```Core``` modulu
* nektere moduly napr. ```LodgingComponent``` jsou zavisle na jinych modulech, jelikoz se jedna pouze o jejich rozsireni,  v tomto pripade na ```PlacesComponent```
* databaze pro vetsinu modulu je definovana v ```Core```
* moduly ```WeatherWidget``` a ```NewsComponent``` maji databazi definovanou zvlast 

### Build aplikace
* build s pouzitim Visual Studio
    - vyberte kofiguraci v menu "Solution Configurations"
    - v build menu vyberte "Build Solution" nebo "Rebuild Solution"
* build s pouzitim CMD
    - pokud nemate MSBuild.exe v PATH, prejdete do adresare C:\Windows\Microsoft.NET\Framework
    - spustte program MSBuild s plnou cestou k souboru .csproj / .sln vasi aplikace

```
MSBuild C:\projekty\guidapp\guidapp.sln
```
    
* [Vice informaci](https://msdn.microsoft.com/en-us/library/windows/apps/ff928362%28v=vs.105%29.aspx)

### Beta testovani aplikace
Pro testovani Windows Phone aplikaci existuji 2 zpusoby

1. Distribuce aplikace pomoci SD karty, ze ktere se aplikace na telefon nainstaluje pomoci vychozi aplikace store 
    - lze samozrejme vyuzit pouze na zarizenich, ktere podporuji SD karty
    - [Vice informaci](https://www.windowsphone.com/en-us/how-to/wp8/apps/how-do-i-install-apps-from-an-sd-card)
2. Distribuce skrze Windows Phone Store
    - v tomto pripade se aplikace nahraje do Store a oznaci se jako Beta
    - je treba pridat seznam emailu uzivatelu, kteri maji opravneni si aplikaci nainstalovat
    - [Vice informaci](https://msdn.microsoft.com/en-us/library/windows/apps/jj215598%28v=vs.105%29.aspx)

### Vydani aplikace
K publikovani aplikace se pouziva dashboard na webu [dev.windowsphone.com]()

* Informace o aplikaci
    - nazev aplikace
    - kategorie (podkategorie) aplikace
    - cena
    - seznam zemi ve kterych bude aplikace dostupna
    - [Vice informaci](https://msdn.microsoft.com/en-us/library/windows/apps/jj206733.aspx)

* Nahrani a popis baliku
    - nahrajeme .XAP balik z Release adresare
    - doplnime informace o verzi
    - pokud se jedna o aktualizaci doplnime senam novinek v posledni verzi (automaticky se doplni do popisu)
    - nahrajeme potrebne ikony
        - Squre icon (358x358px) povinne
        - Wide icon (358x173ox) nepovinne
        - Background image (1000x800px) nepovinne
    - nahrajeme screenshoty aplikace
        - bud pro kazde rozliseni zvlast
        - nebo lze nahrat screenshoty pouze z verze WXGA, ktere se automaticky prizpusobi ostatnim
    - [Vice informaci](https://msdn.microsoft.com/en-us/library/windows/apps/jj206723.aspx)
