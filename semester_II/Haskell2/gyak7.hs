import Data.List
import Data.Char

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
