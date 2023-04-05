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
  | a < 0 = fromTo (a + 1) b l
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
-- ([x,_]) -> minium 2 elemu: test ([x,_]) = [x,0]
-- [(x,_)] -> 1 elemu ami egy tuple-t tartalmaz: test [(x,_)] = []
-- ((x:y):xs) -> listaba agyazott lista: test ((x:y):xs) = [[a1]]
-- (xs:ys:zs) -> minium 2 elemu:
-- [(,) x y,z] pontosan 2 elemu tuple lista: [(,) 4 5, (,) 5 6] || [(4,5),(5,6)]
-- ([d]:[ds]) pontosan 2 elemu listaba agyazott lista: [[4], [5]]
-- ((:) x y:ys) legalabb 1 nem ures allista: [[4],[4]] || [[4]]
-- ((a:b):(c:d:e)) ez egy tradicionalis listaba listak, de legalabb 3 eleme kell legyen es az elso al elemnek legalabb 1 ([[3],[],[]]): [[1,3,4,55,6,7,6],[4,5,6,4,4,5,6,6],[],[],[],[],[],[4],[]] || ez is jo: [[[[]]],[],[]]
-- ((,) x y:ys : _) legalabb 2 elemu tuple lista:  [(4,5),(4,5)] || [(4,5),(4,5), (4,5), (6,7)]
-- [(x,xs):[y,ys]] -> pontosan 3 elemet (tuple) tartalmazo dupla lista: [[(4,5), (3,4), (5,6)]]
-- ([_]:[(x,[xs])]:[y,ys]:[]) -> pontosan 3 list elem, amelyek tuple-eket tartalamznak es a 3ik lista 2 tuple-t kell tartalmazzon: [[(4,[5])], [(4,[5])], [(4,[5]), (4,[6])]]
-- ([(x,y:_:[])]:[]) -> pontosan 1 listaba lista aminben egy tuple es ennek 1 eleme es egy 2 elemu listaja van: [[(4,[5,6])]]  

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