module Homework3 where
import Data.List

getSum :: Int -> Int -> Int
getSum a b 
  | a < b = sum [a..b]
  | otherwise = sum [b..a]

factors n = [x | x <- [1..n], mod n x == 0]
isPrime n = factors n == [1, n]

primeList :: Int -> Int -> [Int]
primeList a b = filter isPrime [ x | x <- [a..b] ]

encode' :: [String] -> [(Char,Int)]
encode' [] = []
encode' (s:xs) = (head s, length s) : encode' xs

encode :: String -> [(Char,Int)]
encode s = encode' $ group s 

decode' :: [(Char,Int)] -> [String]
decode' [] = [""]
decode' ((a, c) : xs) = (replicate c a) : decode' xs

decode :: [(Char,Int)] -> String
decode list = concat (decode' list)