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

fromTo :: Int -> Int -> [a] -> [a]
fromTo _ _ [] = []
fromTo a b l
  | b < a = []
  | otherwise = fromTo' (b - a) (strip' a l)
    where
      strip' 0 a = a
      strip' _ [] = []
      strip' n (a:as)
        | n <= 0 = a:as
        | otherwise = strip' (n - 1) as

      fromTo' :: Int -> [a] -> [a]
      fromTo' 0 l' = []
      fromTo' _ [] = []
      fromTo' n' (l':ls') = l' : fromTo' (n' - 1) ls'


-- ([]:[]) -> ures lista: test ([]:[]) = [[]] (egymasba agyazott) | :: [[a1]] -> [a2]
-- ([x,_]) -> 2 elemu: test ([x,_]) = [x,0]
-- [(x,_)] -> 1 elemu ami egy tuple-t tartalmaz: test [(x,_)] = []
-- ((x:y):xs) -> listaba agyazott lista: test ((x:y):xs) = [[a1]]
-- (xs:ys:zs)
-- [(,) x y,z]
-- ([d]:[ds])
-- ((:) x y:ys)
-- ((a:b):(c:d:e))
-- ((,) x y:ys : _)
-- [(x,xs):[y,ys]]
-- ([_]:[(x,[xs])]:[y,ys]:[])
-- ([(x,y:_:[])]:[])



-- t1 :: [[a1]] -> [a2]
-- t1 ([]:[]) = []

-- t2 :: [a1] -> [a2]
-- t2 ([x,_]) = []

-- t3 :: [(a1, b)] -> [a2]
-- t3 [(x,_)] = []

-- t4 :: [[a1]] -> [a2]
-- t4 ((x:y):xs) = []

-- t5 :: [a1] -> [a2]
-- t5 (xs:ys:zs) = []

-- t6 :: [(a1, b)] -> [a2]
-- t6 [(,) x y,z] = []

-- t7 :: [[a1]] -> [a2]
-- t7 ([d]:[ds]) = []

-- t8 :: [[a1]] -> [a2]
-- t8 ((:) x y:ys) = []

-- t9 :: [[a1]] -> [a2]
-- t9 ((a:b):(c:d:e)) = []

-- t10 :: [(a1, b)] -> [a2]
-- t10 ((,) x y:ys : _) = []

-- t11 :: [[(a1, b)]] -> [a2]
-- t11 [(x,xs):[y,ys]] = []

-- t12 :: [[(a1, [a2])]] -> [a3]
-- t12 ([_]:[(x,[xs])]:[y,ys]:[]) = []

-- t13 :: [[(a1, [a2])]] -> [a3]
-- t13 ([(x,y:_:[])]:[]) = []