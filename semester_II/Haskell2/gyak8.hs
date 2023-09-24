mapToSmaller :: Ord a => (a -> a) -> [a] -> [a]
mapToSmaller _ [] = []
mapToSmaller f (x:xs)
  | f x < x = f x : mapToSmaller f xs
  | otherwise = x : mapToSmaller f xs

c :: ((a, b) -> Int -> c) -> a -> Int -> b -> c
c f a n b = f (a, b) n

d:: (a -> b -> c) -> (a -> b -> c)
-- d f b a = f a b
-- d f = f
d = id

applyNTimes :: Int -> (a -> a) -> [a] -> [a]
applyNTimes 0 _ l = l
-- applyNTimes n f l = map f (applyNTimes (n-1) f l)
applyNTimes n f l = applyNTimes (n-1) f (map f l)

functionComposition :: [(a -> a)] -> a -> a
functionComposition [] a = a
functionComposition (f:fs) a = f (functionComposition fs a)

weightedSum :: Num a => [(a, a)] -> a
weightedSum [] = 0
weightedSum ((x, y) : ls) = (x * y) + weightedSum ls


until' :: (a -> Bool) -> (a -> a) -> a -> a
until' p f a
  | not (p a) = until' p f (f a)
  | otherwise = a

foldr' :: (a -> b -> b) -> b -> [a] -> b
foldr' f b [] = b
foldr' f b (x:xs) = f x (foldr' f b xs)