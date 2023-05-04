# Könyvtár

Egy könyvtár nyilvántartja a könyvtárba beiratkozott személyeket, és a kikölcsönözhető könyveit. 
Könyvtári tag az a személy lehet (ismerjük a nevét), aki beiratkozik a könyvtárba. 

Egy könyvtári tag egy alkalommal legfeljebb öt, a könyvtárban meglévő könyvet kölcsönözhet ki. Egy 
könyvnek ismert a címe, szerzője, kiadója, ISBN száma, az oldalszáma, és van egy könyvtári azonosítója, 
miután a könyvtárba kerül. Az egyszerre kikölcsönzött könyveket több részletben is vissza lehet hozni, 
így egy kölcsönzés eseményhez tartozó könyvek listája folyamatosan csökkenhet.

## Alkalmazott tervezési mintá
- Abstract Factory
- Template Method
- Strategy pattern