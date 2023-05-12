scale :: Double -> [Double] -> [Double]
scale x a = map (x*) a

elem' :: Eq a => a -> [a] -> Bool
elem' a = any (a==)

isVowel :: Char -> Bool
isVowel x = x `elem` "aeiouyAEIOUY"

vowel :: String -> String
vowel = filter isVowel

genVowels :: [[Char]] -> [[Char]]
genVowels a = map (vowel) a

translate :: [Double] -> [Double] -> [Double]
translate x y 
  | length x == length y = zipWith (+) x y
  | otherwise = error "Nem ugyan annyi elemu vektor!"


oddPairs :: [Int] -> [Int] -> [(Int, Int)]
oddPair (a, b) = odd (a + b)
oddPairs a b = filter (oddPair) (zip a b)

divisors :: Int -> [Int]
divisors n 
  | n < 1 = error "Az ertek nem lehet negativ"
  | otherwise = filter ((==0) . rem n) [1 .. n]

factorok :: Int -> [Int]
factorok n = [x | x <- [1..n], mod n x == 0]

isPrime :: Int -> Bool
isPrime n = factorok n == [1,n]

primesUntil :: Int -> [Int]
primesUntil n 
    | n > 1 = filter (isPrime) [2 .. n]
    | otherwise = error "Nem lehet 2-nel kisebb szam"
