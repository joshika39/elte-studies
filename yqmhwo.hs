-- 1. A haskell futas idoben is tudja jelezni a tipushibakat,
-- de ha elore definialjuk egy valtozo, illetve fuggveny tipusat, 
-- akkor forditas alatt is jelelzni fogja, es ez a 
-- legmegfelelobb haskell kod iras, es egyben a leghatekonyabb

-- 2. Le tudjuk kerdezni a :i paraccsal az Int informacioit
-- itt latni tudjuk, hogy ez egy Num tipus, és ha megnézzük a Num
-- típust az :i paranccsal, akkor láthatjuk, hogy a 
-- + függvény sosem fog kiutatni ebbol a típusból, így tudunk 
-- biztosra menni

andImpl :: Bool -> Bool -> Bool -> Bool
andImpl a b c = (a && b) ==> c