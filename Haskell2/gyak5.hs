module Gyak5 where

isPrefixOf' :: Eq a => [a] -> [a] -> Bool
isPrefixOf' [] _ = True
isPrefixOf' _ [] = False
isPrefixOf' (x:xs) (y:ys)
  | x == y = isPrefixOf' xs ys
  | otherwise = False
  

isPrefixOf'' :: Eq a => [a] -> [a] -> Bool
isPrefixOf'' [] _ = True
isPrefixOf'' _ [] = False
isPrefixOf'' (x:xs) (y:ys) = x == y && isPrefixOf' xs ys


elem' :: Eq a => [a] -> a -> Bool
elem' [] _ = False
elem' (x:xs) a = x == a || elem' xs a


take' :: [a] -> Int -> [a]
take' _ 0 = []
take' [] _ = []
take' (x:xs) a = x : take' xs (a - 1)


qsort :: Ord a => [a] -> [a]
qsort [] = []
qsort (p:xs) = qsort [x | x <- xs, x < p] ++ p : qsort [x | x <- xs, x >= p]

tails' :: [a] -> [[a]]
tails' [] = []
tails' (x:xs) = (x:xs) : tails' xs

inits' :: [a] -> [[a]]
inits' [] = [[]]
-- inits' (x:xs) = [] : [x:a | a <- inits' xs]
inits' l = inits' (init l) ++ [l]