import Data.List

quartition :: (a -> Bool) -> (a -> Bool) -> [a] -> ([a],[a],[a],[a])
first f g a = filter (f) (filter (\x -> not(g x)) a)
second f g a = filter (g) (filter (\x -> not(f x)) a)
both f g a = filter (f) (filter (g) a)
none f g a = (filter (\x -> not(f x))) (filter (\x -> not(g x)) a)
quartition f g a = (first f g a, second f g a, both f g a, none f g a) 

atLeastTwo :: (a -> Bool) -> [[a]] -> [Int]
atLeastTwo f [] = []
atLeastTwo f (x:xs) = filter (>1) ((length(filter f x)) : (atLeastTwo f (xs)))

pairwiseMap :: [a -> b] -> [a] -> [b]
pairwiseMap _ [] = []
pairwiseMap [] _ = []
pairwiseMap (x:xs) (y:ys) = x y : pairwiseMap xs ys

crossMap' f [] = []
crossMap' f (x:xs) = f x : crossMap' f xs

crossMap :: [a -> b] -> [a] -> [[b]]
crossMap f x = map (`map` x) f

sameVal :: Eq b => [a -> b] -> a -> Bool
sameVal' [] f = []
sameVal' (x:xs) f = x f : sameVal' xs f
sameVal a f = (all - notSame) > 0
    where 
        all = length(sameVal' a f)
        notSame = length(nub (sameVal' a f))

maxBy :: Ord b => (a -> b) -> [a] -> a
maxBy f a = maximumBy (\x y -> compare (f x) (f y)) (reverse a)


maxFun :: Ord b => [a -> b] -> a -> (a -> b)
maxFun f a = maximumBy (\x y -> compare (x a) (y a))  (reverse f)
