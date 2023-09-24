import Data.List

pairsWithPred :: (a -> a -> Bool) -> [a] -> [(a,a)]
pairsWithPred f l = [(x,y) | x <- l, y <- l, f x y]

appLeftOrRight :: [a -> b] -> [a -> b] -> (a -> Bool) -> [a] -> [b]
appLeftOrRight _ [] _ _ = []
appLeftOrRight [] _ _ _ = []
appLeftOrRight (f:fs) (g:gs) p [] = []
appLeftOrRight (f:fs) (g:gs) p (x:xs)
  | p x == True = f x : appLeftOrRight fs gs p xs
  | p x == False = g x : appLeftOrRight fs gs p xs

secondParam :: Integral a => [a -> a -> Bool] -> (a,a) -> a -> [a]
secondParam fs (start, end) value = filter (\x -> all (\f -> f value x) fs) [start..end]

deletions :: [a] -> [[a]]
deletions xs = unfoldr f (xs, [])
  where
    f ([], _) = Nothing
    f (y:ys, zs) = Just (zs ++ ys, (ys, zs ++ [y]))

fixPoint :: Eq a => (a -> a) -> a -> a
fixPoint f x = until (\y -> y == f y) f x