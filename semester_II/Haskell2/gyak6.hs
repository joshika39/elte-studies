import Data.List
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
all' f (x:xs) = f x && all' f xs

any' :: (a -> Bool) -> [a] -> Bool
any' _ [] = False
any' f (x:xs) = f x || any' f xs

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