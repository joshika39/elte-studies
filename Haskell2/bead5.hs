module Homework5 where

drop' :: Int -> [a] -> [a]
drop' n xs
  | n <= 0 = xs
drop' n [] = []
drop' n (x:xs) = drop' (n - 1) xs

reverse' :: [a] -> [a]
reverse' [] = [] 
reverse' (x:xs) = reverse' xs +++ [x]
  where
    (+++) :: [a] -> [a] -> [a]
    (+++) [] ys = ys
    (+++) (x:xs) ys = x : (+++) xs ys

howMany :: Char -> String -> Int
howMany _ [] = 0
howMany c (s:xs)
  | c == s = 1 + howMany c xs
  | otherwise = howMany c xs


splitAt' :: Int -> [a] -> ([a], [a])
splitAt' n l
  | n <= 0 = ([], l)
splitAt' _ [] = ([], [])
splitAt' n (l:xl) = (l:l', xl')
  where (l', xl') = splitAt' (n - 1) xl

insertAt :: Int -> a -> [a] -> [a]
insertAt n e l
  | n < 0 = e : l

insertAt n e l = insertAt' n l
  where
    insertAt' 0 l' = e : l'
    insertAt' n [] = [e]
    insertAt' n' (x:xs) = x : insertAt' (n' - 1) xs