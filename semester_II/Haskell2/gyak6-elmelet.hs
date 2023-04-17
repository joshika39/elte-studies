import Data.List
-- funkcionális


-- tiszta/mellékhatásmentes


-- statikus típusrendszer


-- hivatkozási helyfüggetlenség

g :: a -> a
g a = f a

f :: a -> a
f a = a

-- minta

h :: Int -> String
h 1 = "A"
h 2 = "Haskell"
h 3 = "nagyon"
h _ = "jó"

-- parciális/totális


-- parciális függvényalkalmazás

head' :: [a] -> a
head' (x:xs) = x

-- curryzés
inc :: Int -> Int
inc x = x + 1

inc' :: Int -> Int
inc' = (+1)

add :: Int -> Int -> Int
add = (+)

-- lustaság


-- operátor


-- konstruktor


-- polimorfizmus


-- paraméter polimorfizmus

tail' :: [a] -> [a]
tail' (x:xs) = xs

-- ad-hoc polimorfizmus

qsort :: Ord a => [a] -> [a]
qsort [] = []
qsort (x:xs) = qsort kisebb ++ [x] ++ qsort nagyobb where
            kisebb = [k | k <- xs, k <= x]
            nagyobb = [n | n <- xs, n > x]

-- generátor



-- típusszinoníma