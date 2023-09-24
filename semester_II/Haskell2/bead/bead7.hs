module Homework7 where

import Data.List ( group )

mapping :: [(Char, Char)]
mapping = [('0', '3'), ('1', '4'), ('2', '5'), ('3', '6'),
           ('4', '7'), ('5', '8'), ('6', '9'), ('7', 'A'),
           ('8', 'B'), ('9', 'C'), ('A', 'D'), ('B', 'E'),
           ('C', 'F'), ('D', 'G'), ('E', 'H'), ('F', 'I'),
           ('G', 'J'), ('H', 'K'), ('I', 'L'), ('J', 'M'),
           ('K', 'N'), ('L', 'O'), ('M', 'P'), ('N', 'Q'),
           ('O', 'R'), ('P', 'S'), ('Q', 'T'), ('R', 'U'),
           ('S', 'V'), ('T', 'W'), ('U', 'X'), ('V', 'Y'),
           ('W', 'Z'), ('X', 'A'), ('Y', 'B'), ('Z', 'c'),
           ('a', 'd'), ('b', 'e'), ('c', 'f'), ('d', 'g'),
           ('e', 'h'), ('f', 'i'), ('g', 'j'), ('h', 'k'),
           ('i', 'l'), ('j', 'm'), ('k', 'n'), ('l', 'o'),
           ('m', 'p'), ('n', 'q'), ('o', 'r'), ('p', 's'),
           ('q', 't'), ('r', 'u'), ('s', 'v'), ('t', 'w'),
           ('u', 'x'), ('v', 'y'), ('w', 'z'), ('x', 'a'),
           ('y', 'b'), ('z', 'c')]

encodeCaesar :: String -> String
encodeCaesar l = map (\(x:xs) -> getNewChar x) (group l)
    where
        getNewChar :: Char -> Char
        getNewChar c = snd (head (filter (\(x,y) -> x == c) mapping))

decodeCaesar :: String -> String
decodeCaesar l = map (\(x:xs) -> getNewChar x) (group l)
    where
        getNewChar :: Char -> Char
        getNewChar c = fst (head (filter (\(x,y) -> y == c) mapping))


-- Orai:

isPrime :: Int -> Bool
isPrime n = length (filter (\x -> x `mod` n == 0) [1..n]) == 2

iterate'' :: (a -> a) -> a -> [a]
iterate'' f x = x : iterate'' f (f x)

zipWith' :: (a -> b -> c) -> [a] -> [b] -> [c]
-- zipWith' _ [] _ = []
-- zipWith' _ _ [] = []
-- zipWith' f (x:xs) (y:ys) = f x y : zipWith' f xs ys
zipWith' f l1 l2 = map (\(x,y) -> f x y ) (zip l1 l2) 

lucasSeries :: [Integer]
lucasSeries = map fst (iterate (\(x,y) -> (y, x + y)) (1,1))

compress :: Eq a => [a] -> [(a, Int)]
compress l = map (\(x:xs) -> (x, length (x:xs))) (group l)

decompress :: Eq a => [(a, Int)] -> [a]
decompress l = concat (map (\(a, b) -> replicate b a) l) 

apsOnList :: [(a -> b)] -> [[a]] -> [[b]]
apsOnList fs ls = map (\(f,l) -> map f l) (zip fs ls)