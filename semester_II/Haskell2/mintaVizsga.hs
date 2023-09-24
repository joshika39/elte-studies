splitQuadruple :: (a,b,c,d) -> ((a,b),(c,d))
splitQuadruple (a, b, c, d) = ((a, b), (c, d))

dist1 :: Num a => a -> a -> a
dist1 a b = abs (a - b)

kroeneckerDelta :: Eq a => a -> a -> Int
kroeneckerDelta a b
  | a == b = 1
  | otherwise = 0

freq :: Eq a => a -> [a] -> Int
freq _ [] = 0
freq elem (l:ls)
  | elem == l = 1 + freq elem ls
  | otherwise = freq elem ls


hasUpperCase :: String -> Bool
hasUpperCase [] = False
hasUpperCase (l:ls) = l >= 'A' && l <= 'Z' || hasUpperCase ls

-- identifier :: String -> Bool
-- identifier (s:str)

replace :: Int -> a -> [a] -> [a]
replace _ newVal [] = [newVal]
replace n newVal (x:xs)
  | n < 0 = newVal : (x:xs)
  | n == 0 = newVal:xs
  | otherwise = x:replace (n-1) newVal xs

safeDiv :: Int -> Int -> Maybe Int
safeDiv a b
  | b == 0 = Nothing
  | otherwise = Just (div a b)

paripos :: [Int] -> Bool
paripos l = all (\(x,y) -> even x == even y) (zip l [0..])

parseCSV :: String -> [String]
parseCSV [] = [""]
parseCSV xs = takeWhile f xs : parseCSV (tail (dropWhile f xs))
  where
    f = (\x -> x /= ';')

c :: (a -> b -> c) -> (b -> a -> c)
c f x y = f y x


-- w :: (a -> a -> a) -> a -> a
-- w f a = f a

data Binary = On | Off deriving (Eq, Show)

switch :: Binary -> Binary
switch On = Off
switch Off = On

bxor :: [Binary] -> [Binary] -> [Binary]
bxor [] _ = []
bxor _ [] = []
bxor (x:xs) (y:ys)
  | x == y = On : bxor xs ys
  | otherwise = Off : bxor xs ys

data Temperature = Daytime Int | Night Int deriving (Eq, Show)

isDaytime :: Temperature -> Bool
isDaytime (Daytime x) = True
isDaytime _ = False

extremes :: [Temperature] -> (Int, Int)
extremes x = (maximum [n | (Daytime n) <- x], minimum [n | (Night n) <- x])