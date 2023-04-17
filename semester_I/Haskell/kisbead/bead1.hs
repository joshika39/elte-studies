-- Joshua Hegedus
-- NeptunCode: YQMHWO

module Hazi1 where

{-
1. Feladat

Definiálj egy familyName nevű konstanst, melynek értéke szöveg típusú, nagy betűvel kezdődik, és a
vezetéknevedet tartalmazza ékezetek nélkül.

Definiálj egy givenName nevű konstanst, melynek értéke szöveg típusú, nagy betűvel kezdődik, és a
keresztnevedet tartalmazza ékezetek nélkül.

-}

familyName :: String
familyName = "Hegedus"

givenName :: String
givenName = "Joshua"


{-
2. feladat

Definiálj egy fullName nevű konstanst, melynek értéke szöveg típusú és a teljes nevedet tartalmazza
ékezetek nélkül, szóközzel elválasztva. A definícióban kötelező felhasználni a korábbiakban definiált
familyName és givenName konstansokat is

-}

fullName :: String
fullName = familyName ++ " " ++ givenName

