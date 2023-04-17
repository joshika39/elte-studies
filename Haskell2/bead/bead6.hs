module Homework6 where
import Data.Char (chr)
import Data.List (isPrefixOf)

filters :: (a -> Bool) -> [[a]] -> [a]
filters = concatMap . filter

mapMap :: (a -> a) -> [[a]] -> [[a]]
mapMap = map . map

dropSpaces :: [Char] -> [Char]
dropSpaces = dropWhile (== ' ')

uniq :: Eq a => [a] -> [a]
uniq [] = []
uniq (x:xs) = x : uniq (filter (/=x) xs)

-- 6. GYAKORLAT --

-- a -> a -> a -> a == a -> (a -> (a -> a))
inc'' :: Int -> Int
inc'' = (\x -> x + 1)

($$) :: (a -> b) -> a -> b
($$) f a = f a

(...) :: (b -> c) -> (a -> b) -> a -> c
(...) f g x= f (g x)

map' :: (a -> b) -> [a] -> [b]
map' _ [] = []
map' f (x:xs) = (f x) : map' f xs

foo :: Int -> Bool
foo = (> 10)

filter' :: (a -> Bool) -> [a] -> [a]
filter' _ [] = []
filter' f (x:xs)
  | f x = x : filter' f xs
  | otherwise = filter' f xs 

count' :: (a -> Bool) -> [a] -> Int
count' _ [] = 0
count' f (x:xs)
  | f x = 1 + count' f xs
  | otherwise = count' f xs

all' :: (a -> Bool) -> [a] -> Bool
all' _ [] = False
all' f (x:xs) = (f x) && all' f xs

any' :: (a -> Bool) -> [a] -> Bool
any' _ [] = False
any' f (x:xs) = (f x) || any' f xs

-- extra (oran nem lett megirva, en keszitettem el hazinak)
takeWhile' :: (a -> Bool) -> [a] -> [a]
takeWhile' _ [] = []
takeWhile' f (x:xs)
  | f x = x : takeWhile' f xs
  | otherwise = takeWhile' f xs

dropWhile' :: (a -> Bool) -> [a] -> [a]
dropWhile' _ [] = []
dropWhile' f (x:xs)
  | f x = dropWhile' f xs
  | otherwise = (x:xs)

-- HEGEDUS JOSHUA Dictionary HF 1 --

dictionary :: [String]
dictionary = [ [chr x] | x <- [0..127]]

prefixes :: String -> [String] -> [(Int,String)]
prefixes y xs = [(i, xs !! i) | i <- [0 .. length xs - 1], (xs !! i) `isPrefixOf` y]
-- prefixes y xs = [(i, xs !! i) | i <- [0 .. length xs - 1], isPrefixOf (xs !! i) y]

longest :: [(Int,String)] -> (Int,String)
longest xs = helper xs (0,"")
  where
    helper [] acc = acc
    helper (x:xs) acc
      | length (snd acc) < length (snd x) = helper xs x
      | otherwise = helper xs acc


munch :: [String] -> String -> (Int,String,String)
munch dict str = (fst longst, snd longst, [str !! x | x <- [length (snd longst)..length str - 1]])
  where
    longst = longest (prefixes str dict)

append :: [String] -> String -> String -> [String]
append dict _ "" = dict
append dict w1 w2
  | word `notElem` dict = dict ++ [word]
  | otherwise = dict
  where
    word = w1 ++ take 1 w2