prod :: Num a => [a] -> a
prod [] = 1
prod [a] = a
prod (a:as) = a * prod as

every :: [Bool] -> Bool
every [] = True
every (a:as) = a && every as

some :: [Bool] -> Bool
some [] = False
some (a:as) = a || some as

password :: [Char] -> [Char]
password [] = []
password (a:as) = '*':password as

countA :: [Char] -> Int
countA [] = 0
countA (a:as)
    | a == 'a' || a == 'A' = (countA as) + 1
    | otherwise = countA as

isLengthEven :: [a] -> Bool
isLengthEven [] = True
isLengthEven (_:as) = not (isLengthEven as)

dotProduct :: [Double] -> [Double] -> Double
dotProduct [] [] = 0
dotProduct (x:xs) (y:ys) = x * y + (dotProduct xs ys)

hasSameLength :: [a] -> [b] -> Bool
hasSameLength [] [] = True
hasSameLength (a:as) [] = False
hasSameLength [] (a:as) = False
hasSameLength (x:xs) (y:ys) = hasSameLength xs ys

filterA2' :: [Char] -> [Char]
filterA2' [] = []
filterA2' (x:xs)
    | x == 'a' || x == 'A' = filterA2' xs
    | otherwise = x : filterA2' xs

filterA2 :: [Char] -> [Char] -> ([Char],[Char])
filterA2 a b = (filterA2' a, filterA2' b)


